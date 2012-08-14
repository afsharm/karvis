using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Web;
using Fardis;

namespace Karvis.Core
{
    public class ExtractJobsModel : IExtractJobsModel
    {
        //todo:
        //save per item
        //track ignore jobs to not appear again
        //extract tag must be extended
        //extract tag must consider title too
        //ProcessJob() to be accessible in other places
        //remove fixed words like "mohem nist" from Nofa.ir
        //delete button in job list must show a confirmation
        //each tag in job list or job detail must convert to a link to search page
        //more items must be showed in first page of job list
        //add barnamehnevis.org to adsources

        const string RahnamaTextJobXPath = "//div[@id='listing']";
        const string RahnamaImageJobXPath = "//a[@onclick='return hs.expand(this)']";

        /// <summary>
        /// Date/Time convertion
        /// </summary>
        IDateTimeHelper _dateTimeHelper;

        /// <summary>
        /// crawling web addresses
        /// </summary>
        IKarvisCrawler _crawler;

        IExtractorHelper _extractorHelper;

        /// <summary>
        /// Constructor
        /// </summary>
        public ExtractJobsModel(IKarvisCrawler karvisCrawler, IDateTimeHelper dateTimeHelper,
            IExtractorHelper extractorHelper)
        {
            _dateTimeHelper = dateTimeHelper;
            _crawler = karvisCrawler;
            _extractorHelper = extractorHelper;
        }

        /// <summary>
        /// Extract jobs from the given Advertise source regarding given limitations. 
        /// Extractor retrives records until reachs one of three stoppers
        /// </summary>
        /// <param name="siteSource">Advertising sources</param>
        /// <param name="stopperDate">Extractor does not retrive records with original date older than stopperDate</param>
        /// <param name="stopperRecordCount">Extractor extract just as specified stopper record count</param>
        public List<Job> ExtractJobs(AdSource siteSource, int? limitDays, int? stopperRecordCount)
        {
            //url(s) of the specified advertise source
            string[] urls = _extractorHelper.GetSiteSourceUrl(siteSource);

            List<Job> retval = new List<Job>();

            _extractorHelper.GetReady(siteSource);

            foreach (string url in urls)
            {
                string rootUrl = _extractorHelper.ExtractRootUrl(url);
                var jobs = ExtractSingleUrlJobs(siteSource, url, rootUrl, limitDays, stopperRecordCount);

                retval.AddRange(jobs);
            }

            return retval;
        }

        /// <summary>
        /// Extract jobs from specified single url
        /// </summary>
        public List<Job> ExtractSingleUrlJobs(AdSource siteSource, string url, string rootUrl,
            int? limitDays, int? stopperRecordCount)
        {
            List<Job> retval = new List<Job>();

            //url changes over pagings
            string currentUrl = url;

            //if this url contains another page
            bool hasPaging = false;

            //does Stoppers allow the process to be continued
            bool canContinue = false;

            //looping through pages of information
            do
            {
                //loading content of url
                string pageContent = _crawler.GetWebText(currentUrl);
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(pageContent);

                switch (siteSource)
                {
                    case AdSource.rahnama_com:
                        //rahnama_com has 2 type of advertises: text and image

                        //text jobs
                        canContinue = ExtractRahnamaJobs(retval, doc, rootUrl,
                            limitDays, stopperRecordCount, false);

                        //image jobs
                        canContinue = ExtractRahnamaJobs(retval, doc, rootUrl,
                            limitDays, stopperRecordCount, true);

                        //repeat the process if another pages exists
                        hasPaging = HasPagingRahnama(doc, url, ref currentUrl, rootUrl);
                        break;
                    case AdSource.agahi_ir:
                        //agahi_ir jobs
                        canContinue = ExtractAgahiJobs(retval, doc, rootUrl, limitDays, stopperRecordCount);

                        //paging
                        hasPaging = HasPagingAgahi(doc, ref currentUrl);
                        break;
                    case AdSource.nofa_ir:
                        canContinue = ExtractNofaJobs(retval, doc, limitDays, stopperRecordCount, rootUrl);
                        hasPaging = HasPagingNofa(doc, ref currentUrl);
                        break;
                }
            }
            while (hasPaging && canContinue);

            return retval;
        }

        /// <summary>
        /// Extract rahnama_com text jobs
        /// </summary>
        public bool ExtractRahnamaJobs(List<Job> jobs, HtmlDocument doc, string rootUrl,
            int? limitDays, int? stopperRecordCount, bool isImageJob)
        {
            string xPath = isImageJob ? RahnamaImageJobXPath : RahnamaTextJobXPath;

            //extracting raw information
            var rawJobs = doc.DocumentNode.SelectNodes(xPath);

            //don't continue if no jobs exist
            if (rawJobs == null)
                return false;

            for (int i = 0; i < rawJobs.Count; i++)
            {
                //stop extracting if stopperRecordCount has been reached
                if (stopperRecordCount != null && i > stopperRecordCount.Value)
                    return false;

                var rawJob = rawJobs[i];

                //extract job from raw html
                Job job = null;
                if (isImageJob)
                    job = ExtractRahnamaImageJob(rawJob);
                else
                    job = ExtractRahnamaTextJob(rootUrl, rawJob);

                //stop extracting if stopperDate has reached
                if (job.OriginalDate != null && limitDays != null &&
                    (DateTime.Now - job.OriginalDate.Value).Days > limitDays)
                    return false;

                if (!_extractorHelper.JobUrlExists(job.Url))
                    jobs.Add(job);
            }

            //not stopper condition has been encountered
            return true;
        }

        /// <summary>
        /// extract rahnam_com text job from raw html
        /// </summary>
        public Job ExtractRahnamaTextJob(string rootUrl, HtmlNode rawHtmlJob)
        {
            string absoluteUrl;
            string title;
            string description;

            try
            {
                string relativeUrl = rawHtmlJob.ChildNodes[3].ChildNodes[0].Attributes["href"].Value;
                absoluteUrl = _extractorHelper.GetAbsoluteUrl(relativeUrl, rootUrl);
                title = rawHtmlJob.ChildNodes[3].ChildNodes[0].InnerText;
                description = rawHtmlJob.ChildNodes[4].InnerHtml;
            }
            catch (Exception ex)
            {
                //to ignore possible error
                absoluteUrl = "N/A";
                title = "N/A";
                description = ex.Message;
            }

            Job job = new Job()
            {
                Description = description,
                Title = title,
                Url = absoluteUrl,
                AdSource = AdSource.rahnama_com
            };

            _extractorHelper.ProcessJob(job);
            return job;
        }

        /// <summary>
        /// extract rahnam_com text job from raw html
        /// </summary>
        public Job ExtractRahnamaImageJob(HtmlNode rawHtmlJob)
        {
            string absoluteUrl;
            string description;
            string title;

            try
            {
                absoluteUrl = rawHtmlJob.Attributes["href"].Value;
                title = "آگهی عکسی";
                description = "<img src=\"" + absoluteUrl + "\" />";
            }
            catch (Exception ex)
            {
                //to ignore possible error
                absoluteUrl = "N/A";
                title = "N/A";
                description = ex.Message;
            }

            Job job = new Job()
            {
                Description = description,
                Title = title,
                Url = absoluteUrl,
                AdSource = AdSource.rahnama_com
            };

            _extractorHelper.ProcessJob(job);
            return job;
        }

        /// <summary>
        /// if requested url has another pages of data
        /// </summary>
        public bool HasPagingRahnama(HtmlDocument doc, string originalUrl, ref string nextPageUrl, string rootUrl)
        {
            HtmlNodeCollection paging = null;

            paging = doc.DocumentNode.SelectNodes("//a[@title='بعدی']");
            bool hasPaging = paging != null && paging.Count > 0;

            //extracting url of next page
            if (hasPaging)
                nextPageUrl = _extractorHelper.GetAbsoluteUrl(paging[0].Attributes["href"].Value, rootUrl);

            return hasPaging;
        }

        /// <summary>
        /// extract jobs from agahi_ir
        /// </summary>
        public bool ExtractAgahiJobs(List<Job> jobs, HtmlDocument doc, string rootUrl,
            int? limitDays, int? stopperRecordCount)
        {
            var jobsHtml = doc.DocumentNode.SelectNodes("//div[@class='slr_box_contents']")[5].ChildNodes;
            int recordCounter = 0;

            for (int i = 4; i < jobsHtml.Count - 2; i = i + 8)
            {
                recordCounter++;

                //do not continue if stopperrecord counter has been reached
                if (stopperRecordCount != null && recordCounter > stopperRecordCount.Value)
                    return false;

                HtmlNode bodyNode = jobsHtml[i];
                HtmlNode contactNode = jobsHtml[i + 2];
                Job job = ExtractAgahiJob(bodyNode, contactNode);

                //stop extracting if stopperDate has reached
                if (job.OriginalDate != null && limitDays != null &&
                    (DateTime.Now - job.OriginalDate.Value).Days > limitDays)
                    return false;

                if (!_extractorHelper.JobUrlExists(job.Url))
                    jobs.Add(job);
            }

            return true;
        }

        public Job ExtractAgahiJob(HtmlNode item, HtmlNode contact)
        {
            string absoluteUrl;
            string title;
            string description;
            DateTime? originalDate = null;

            try
            {
                absoluteUrl = item.ChildNodes[1].Attributes["href"].Value;
                title = item.ChildNodes[1].ChildNodes[3].InnerText;
                string originalDateString = item.ChildNodes[3].InnerText;
                originalDate = _dateTimeHelper.ConvertPersianToGregorianDate(originalDateString);

                description = string.Format("{0}, original date: {1}, contact: {2}",
                    item.ChildNodes[6].InnerText, originalDate, contact.InnerText);
            }
            catch (Exception ex)
            {
                //to ignore possible error
                absoluteUrl = "N/A";
                title = "N/A";
                description = ex.Message;
            }

            Job job = new Job()
            {
                Description = description,
                Title = title,
                Url = absoluteUrl,
                OriginalDate = originalDate,
                AdSource = AdSource.agahi_ir
            };

            _extractorHelper.ProcessJob(job);
            return job;
        }

        /// <summary>
        /// is there paging
        /// </summary>
        public bool HasPagingAgahi(HtmlDocument doc, ref string nextPageUrl)
        {
            var list = doc.DocumentNode.SelectNodes("//div[@class='paging_div']")[0].ChildNodes;

            bool hasPaging = false;
            bool found = false;
            int counter = 0;
            for (int i = 2; i < list.Count; i = i + 2)
            {
                HtmlAttributeCollection attributes = list[i].Attributes;

                var cssClass = attributes["class"].Value;
                if (!found && cssClass != "paging_item_selected")
                    continue;
                else
                    found = true;

                counter++;

                if (counter > 1)
                {
                    nextPageUrl = attributes["href"].Value;
                    hasPaging = true;
                    break;
                }
            }

            return hasPaging;
        }

        /// <summary>
        /// extract jobs from nofa_ir
        /// </summary>
        public bool ExtractNofaJobs(List<Job> jobs, HtmlDocument doc,
            int? limitDays, int? stopperRecordCount, string rootUrl)
        {
            var list = doc.DocumentNode.SelectNodes("//table[@id='Table22']");

            for (int i = 0; i < list.Count; i++)
            {
                //stop extracting if stopperRecordCount has been reached
                if (stopperRecordCount != null && i > stopperRecordCount.Value)
                    return false;

                var node = list[i];
                Job job = ExtractNofaJob(rootUrl, node);

                //stop extracting if stopperDate has reached
                if (job.OriginalDate != null && limitDays != null &&
                    (DateTime.Now - job.OriginalDate.Value).Days > limitDays)
                    return false;

                if (!_extractorHelper.JobUrlExists(job.Url))
                {
                    ExtractNofaJobComplementary(job);
                    jobs.Add(job);
                }
            }

            return true;
        }

        public Job ExtractNofaJob(string rootUrl, HtmlNode item)
        {
            string absoluteUrl;
            string title;
            string description;

            try
            {
                string relativeUrl = item.ChildNodes[1].ChildNodes[1].ChildNodes[0].ChildNodes[1].Attributes[2].Value;
                absoluteUrl = _extractorHelper.GetAbsoluteUrl("/" + relativeUrl, rootUrl);
                string originalDate = item.ChildNodes[1].ChildNodes[3].ChildNodes[0].ChildNodes[1].InnerText;
                DateTime originalDateGregorian = _dateTimeHelper.ConvertPersianToGregorianDate(originalDate);

                title = item.ChildNodes[1].ChildNodes[1].ChildNodes[0].ChildNodes[1].InnerText;
                description = item.ChildNodes[5].ChildNodes[1].ChildNodes[1].ChildNodes[1].InnerText;
            }
            catch (Exception ex)
            {
                //to ignore possible error

                title = "N/A";
                absoluteUrl = "N/A";
                description = ex.Message;
            }

            Job job = new Job()
            {
                Description = description,
                Title = title,
                Url = absoluteUrl,
                AdSource = AdSource.nofa_ir
            };

            _extractorHelper.ProcessJob(job);
            return job;
        }

        /// <summary>
        /// Nofa jobs have their information in 2 pages. This
        /// method extract complementary informtion.
        /// </summary>
        public void ExtractNofaJobComplementary(Job job)
        {
            string pageContent = _crawler.GetWebText(job.Url);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageContent);

            try
            {
                job.Title = doc.DocumentNode.SelectNodes("//span[@id='BaseMasterPage1__ctl0__ctl4_lblJobName']")[0].InnerText;

                string i1 = doc.DocumentNode.SelectNodes("//span[@id='BaseMasterPage1__ctl0__ctl4_lblDesc']")[0].InnerText;
                string i2 = doc.DocumentNode.SelectNodes("//a[@id='BaseMasterPage1__ctl0__ctl4_lblCompanyName']")[0].InnerText;
                string i3 = doc.DocumentNode.SelectNodes("//span[@id='BaseMasterPage1__ctl0__ctl4_lblRegDate']")[0].InnerText;
                string i4 = doc.DocumentNode.SelectNodes("//span[@id='BaseMasterPage1__ctl0__ctl4_lbltel']")[0].InnerText;
                string i5 = doc.DocumentNode.SelectNodes("//span[@id='BaseMasterPage1__ctl0__ctl4_lblWebSite']")[0].InnerText;
                string i6 = doc.DocumentNode.SelectNodes("//span[@id='BaseMasterPage1__ctl0__ctl4_lblJobType']")[0].InnerText;
                string i7 = doc.DocumentNode.SelectNodes("//span[@id='BaseMasterPage1__ctl0__ctl4_lbladdress']")[0].InnerText;
                string i8 = doc.DocumentNode.SelectNodes("//span[@id='BaseMasterPage1__ctl0__ctl4_lblState']")[0].InnerText;
                string i9 = doc.DocumentNode.SelectNodes("//span[@id='BaseMasterPage1__ctl0__ctl4_lblCity']")[0].InnerText;
                string i10 = doc.DocumentNode.SelectNodes("//span[@id='BaseMasterPage1__ctl0__ctl4_lbledu']")[0].InnerText;
                string i11 = doc.DocumentNode.SelectNodes("//span[@id='BaseMasterPage1__ctl0__ctl4_lbllsum']")[0].InnerText;
                string i12 = doc.DocumentNode.SelectNodes("//span[@id='BaseMasterPage1__ctl0__ctl4_lblsex']")[0].InnerText;
                string i13 = doc.DocumentNode.SelectNodes("//span[@id='BaseMasterPage1__ctl0__ctl4_lblmarr']")[0].InnerText;
                string i14 = doc.DocumentNode.SelectNodes("//span[@id='BaseMasterPage1__ctl0__ctl4_lblmili']")[0].InnerText;
                string i15 = doc.DocumentNode.SelectNodes("//span[@id='BaseMasterPage1__ctl0__ctl4_lblage']")[0].InnerText;
                string i16 = doc.DocumentNode.SelectNodes("//span[@id='BaseMasterPage1__ctl0__ctl4_lblant']")[0].InnerText;

                job.Description = string.Format("{0} - {1} - {2} - {3} - {4} - {5} - {6} - {7} - {8} - {9} - {10} - {11} - {12} - {13} - {14} - {15}",
                    i1, i2, i3, i4, i5, i6, i7, i8, i9, 10, i11, i12, i13, i14, i15, i16);

                _extractorHelper.ProcessJob(job);
            }
            catch (Exception ex)
            {
                job.Description = ex.Message;
            }
        }

        public bool HasPagingNofa(HtmlDocument doc, ref string url)
        {
            //nofa.ir use ASP.NET GridView for paging, paging in GridView is hard
            //so we does not support paging for nofa yet
            return false;
        }
    }
}
