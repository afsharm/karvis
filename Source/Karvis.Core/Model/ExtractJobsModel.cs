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
        const string RahnamaTextJobXPath = "//div[@id='listing']";
        const string RahnamaImageJobXPath = "//div[@class='image-container']";

        /// <summary>
        /// Date/Time convertion
        /// </summary>
        IDateTimeHelper dateTimeHelper;

        /// <summary>
        /// crawling web addresses
        /// </summary>
        IKarvisCrawler crawler;

        /// <summary>
        /// Constructor
        /// </summary>
        public ExtractJobsModel()
        {
            dateTimeHelper = new DateTimeHelper();
            crawler = new KarvisCrawler();
        }

        /// <summary>
        /// Extract jobs from the given Advertise source regarding given limitations. 
        /// Extractor retrives records until reachs one of three stoppers
        /// </summary>
        /// <param name="siteSource">Advertising sources</param>
        /// <param name="stopperUrl">a job URL that stops Extractor from retrieving more job records</param>
        /// <param name="stopperDate">Extractor does not retrive records with original date older than stopperDate</param>
        /// <param name="stopperRecordCount">Extractor extract just as specified stopper record count</param>
        public List<Job> ExtractJobs(AdSource siteSource, string stopperUrl, DateTime? stopperDate, int? stopperRecordCount)
        {
            //url(s) of the specified advertise source
            string[] urls = GetSiteSourceUrl(siteSource);

            List<Job> retval = new List<Job>();

            foreach (string url in urls)
            {
                string rootUrl = ExtractRootUrl(url);
                var jobs = ExtractSingleUrlJobs(siteSource, url, rootUrl, stopperUrl, stopperDate, stopperRecordCount);

                retval.AddRange(jobs);

                //retval.AddRange(ExtractTextJobs(siteSource, textJobs, agahiContacts, agahiComplementary, rootUrl));

                //if (siteSource == AdSource.rahnama_com)
                //    retval.AddRange(ExtractImageJobs(imageJobs, rootUrl));
            }

            return retval;
        }

        /// <summary>
        /// Gets urls that are used for given advertise source, a typical ad source may have more than one url.
        /// </summary>
        public string[] GetSiteSourceUrl(AdSource siteSource)
        {
            switch (siteSource)
            {
                case AdSource.rahnama_com:
                    return new string[] 
                        {
                            "http://www.rahnama.com/component/mtree/%DA%AF%D8%B1%D9%88%D9%87/35179/%D8%A8%D8%B1%D9%86%D8%A7%D9%85%D9%87-%D9%86%D9%88%D9%8A%D8%B3.html",
                            "http://www.rahnama.com/component/mtree/%DA%AF%D8%B1%D9%88%D9%87/35310/%D9%85%D9%87%D9%86%D8%AF%D8%B3-%D9%83%D8%A7%D9%85%D9%BE%D9%8A%D9%88%D8%AA%D8%B1.html"
                        };
                case AdSource.agahi_ir:
                    return new string[] 
                        { 
                            "http://www.agahi.ir/category/14" 
                        };
                case AdSource.nofa_ir:
                    return new string[]
                    {
                        "http://www.nofa.ir/JobSR-t13.aspx"
                    };

                case AdSource.irantalent_com:
                case AdSource.developercenter_ir:
                case AdSource.itjobs_ir:
                case AdSource.istgah_com:
                case AdSource.unp_ir:
                case AdSource.banki_ir:
                    throw new ApplicationException("This site source has not been implemented yet");

                case AdSource.Misc:
                case AdSource.All:
                case AdSource.karvis_ir:
                case AdSource.Email:
                    throw new ApplicationException("This site source can not be extrcted");

                default:
                    throw new ArgumentException("Unknown site source");
            }
        }

        /// <summary>
        /// Extract jobs from specified single url
        /// </summary>
        public List<Job> ExtractSingleUrlJobs(AdSource siteSource, string url, string rootUrl,
            string stopperUrl, DateTime? stopperDate, int? stopperRecordCount)
        {
            //textJobs = new HtmlNodeCollection(null);
            //imageJobs = new HtmlNodeCollection(null);
            //agahiContactJobs = new HtmlNodeCollection(null);
            //agahiComplementary = new HtmlNodeCollection(null);

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
                string pageContent = crawler.GetWebText(currentUrl);
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(pageContent);

                switch (siteSource)
                {
                    case AdSource.rahnama_com:
                        //rahnama_com has 2 type of advertises: text and image

                        //text jobs
                        canContinue = ExtractRahnamaJobs(retval, doc, rootUrl, stopperUrl,
                            stopperDate, stopperRecordCount, false);

                        //image jobs
                        canContinue = ExtractRahnamaJobs(retval, doc, rootUrl, stopperUrl,
                            stopperDate, stopperRecordCount, true);

                        //repeat the process if another pages exists
                        hasPaging = HasPagingRahnama(doc, url, ref currentUrl, rootUrl);
                        break;
                    case AdSource.agahi_ir:
                        //agahi_ir jobs
                        canContinue = ExtractAgahiJobs(retval, doc, rootUrl, stopperUrl, stopperDate, stopperRecordCount);

                        //paging
                        hasPaging = HasPagingAgahi(doc, ref currentUrl);
                        break;
                    case AdSource.nofa_ir:
                        canContinue = ExtractNofaJobs(retval, doc, stopperUrl, stopperDate, stopperRecordCount, rootUrl);
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
            string stopperUrl, DateTime? stopperDate, int? stopperRecordCount, bool isImageJob)
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
                if (job.OriginalDate != null && stopperDate != null && job.OriginalDate.Value < stopperDate.Value)
                    return false;

                //stop extracting if stopperUrl has been found
                if (stopperUrl != null && job.Url == stopperUrl)
                    return false;

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
                absoluteUrl = GetAbsoluteUrl(relativeUrl, rootUrl);
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

            ProcessJob(job);
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
                absoluteUrl = rawHtmlJob.ChildNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[1].Attributes["href"].Value;
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

            ProcessJob(job);
            return job;
        }

        /// <summary>
        /// Process title of a job 
        /// </summary>
        public string ProcessTitle(string source)
        {
            return FConvert.ToPersianTotal(FConvert.ReplaceControlCharacters(source, " "));
        }

        /// <summary>
        /// get absolute url from given relative url and given root url
        /// </summary>
        public string GetAbsoluteUrl(string relativeUrl, string rootUrl)
        {
            return string.Format("{0}{1}", rootUrl, relativeUrl);
        }

        /// <summary>
        /// process description text
        /// </summary>
        public string ProcessDescription(string plainDescription)
        {
            return
                FConvert.ToPersianYehKeh(FConvert.ReplaceControlCharacters(
                plainDescription
                .Replace("<br>", " ")
                .Replace("</br>", " ")
                .Replace("<span>", " ")
                .Replace("</span>", " "), " "));
        }

        /// <summary>
        /// extract tags from given text
        /// </summary>
        public string ExtractTags(string text)
        {
            string retval = string.Empty;
            string tagPattern = @"[a-zA-Z.#+_@]+";
            string source = text;

            source = FConvert.ToPersianYehKeh(source);
            source = source.ToLower();
            source = source.Replace(",", " ");

            //todo: remove spaces more than one

            //correcting mispelling
            source = source
                .Replace("#c", "C#")
                .Replace("net.", ".Net")
                .Replace("++c", "C++")
                .Replace("++vc", "VC++");

            //persian translation
            source = source
                .Replace("وب", "Web")
                .Replace("و ب", "Web")
                .Replace("آقا", "Male")
                .Replace("خانم", "Female")
                .Replace("طراح قالب", "Template Designer")
                .Replace("دلفی", "Delphi")
                .Replace("تحلیل گر", "Analyst")
                .Replace("تحلیل‌گر", "Analyst")
                .Replace("طراح", "Software Designer")
                .Replace("جاوا", "Java")
                .Replace("بانک اطلاعاتی", "database")
                .Replace("اوراکل", "Oracle")
                .Replace("فلش", "Flash")
                .Replace("جوملا", "Joomla")
                .Replace("وردپرس", "Wordpress")
                .Replace("ورد پرس", "WordPress")
                .Replace("اتوران", "Autoran")
                .Replace("اتو ران", "Autoran")
                .Replace("مدیر پروژه", "Project Manager")
                .Replace("پاره وقت", "Part Time")
                .Replace("شبکه", "Network")
                .Replace("پروژه‌ای", "Project Based")
                .Replace("پروژه ای", "Project Based")
                .Replace("موبایل", "mobile")
                .Replace("آژاکس", "Ajax")
                .Replace("ویندوز", "Windows")
                .Replace("لینوکس", "Linux")
                .Replace("تست", "Test")
                .Replace("آزمون", "Test")
                .Replace("آزمونگر", "Test")
                .Replace("آزمون گر", "Test")
                .Replace("میکرو کنترلر", "Microcontroller")
                .Replace("میکروکنترلر", "Microcontroller")
                .Replace("دات نت", ".Net")
                .Replace("دات‌نت", ".Net");

            //remove email from tags
            foreach (string email in crawler.ExtractEmailsByText(source).Trim().Split(','))
                if (!string.IsNullOrEmpty(email))
                    source = source.Replace(email.Trim(), " ");

            if (string.IsNullOrEmpty(source.Trim()))
                return retval;

            //find tags with regular expressions
            List<string> tags = new List<string>();
            MatchCollection matches = Regex.Matches(source, tagPattern);
            foreach (Match match in matches)
            {
                string rawTag = match.Value;

                //ignore empty results
                if (string.IsNullOrEmpty(rawTag))
                    continue;

                //ignore 1 length tags
                if (rawTag.Length < 2)
                    continue;

                //ignore duplicate tags
                if (tags.Contains(rawTag))
                    continue;

                tags.Add(rawTag);
            }

            //concatening
            foreach (var item in tags)
                retval += item + ", ";

            return retval;
        }

        //public List<Job> ExtractTextJobs(AdSource siteSource, HtmlNodeCollection textJobs,
        //    HtmlNodeCollection agahiContacts, HtmlNodeCollection agahiComplementary, string rootUrl)
        //{
        //    List<Job> retval = new List<Job>();

        //    for (int i = 0; i < textJobs.Count; i++)
        //    {
        //        var item = textJobs[i];
        //        Job job = null;
        //        switch (siteSource)
        //        {
        //            case AdSource.rahnama_com:
        //                job = ExtractRahnamaJob(rootUrl, item);
        //                break;
        //            case AdSource.agahi_ir:
        //                job = ExtractAgahiJob(item, agahiContacts[i], agahiComplementary[i]);
        //                break;
        //        }

        //        //ignoring old ads (Agahi.ir)
        //        if (job == null)
        //            break;

        //        retval.Add(job);
        //    }

        //    return retval;
        //}

        public void ProcessJob(Job job)
        {
            job.Description = ProcessDescription(job.Description);
            job.Emails = crawler.ExtractEmailsByText(job.Description);
            job.Tag = ExtractTags(job.Description);
            job.Title = ProcessTitle(job.Title);
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
                originalDate = dateTimeHelper.ConvertPersianToGregorianDate(originalDateString);

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

            ProcessJob(job);
            return job;
        }

        public string ExtractRootUrl(string url)
        {
            Regex regex = new Regex(@"http([s]*)://[\w.]*");
            MatchCollection matches = regex.Matches(url);

            if (matches.Count > 0)
                return matches[0].Value;
            else
                return null;
        }

        public bool HasPagingNofa(HtmlDocument doc, ref string url)
        {
            //nofa.ir use ASP.NET GridView for paging, paging in GridView is hard
            //so we does not support paging for nofa yet
            return false;
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
                nextPageUrl = GetAbsoluteUrl(paging[0].Attributes["href"].Value, rootUrl);

            return hasPaging;
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
            string stopperUrl, DateTime? stopperDate, int? stopperRecordCount, string rootUrl)
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
                if (job.OriginalDate != null && stopperDate != null && job.OriginalDate.Value < stopperDate.Value)
                    return false;

                //stop extracting if stopperUrl has been found
                if (stopperUrl != null && job.Url == stopperUrl)
                    return false;

                jobs.Add(job);
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
                absoluteUrl = GetAbsoluteUrl("/" + relativeUrl, rootUrl);
                string originalDate = item.ChildNodes[1].ChildNodes[3].ChildNodes[0].ChildNodes[1].InnerText;
                DateTime originalDateGregorian = dateTimeHelper.ConvertPersianToGregorianDate(originalDate);

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

            ProcessJob(job);
            return job;
        }

        public void ExtractRawTextJobs(HtmlNodeCollection textJobs, HtmlDocument doc)
        {
            var res = doc.DocumentNode.SelectNodes("//div[@id='listing']");
            foreach (var item in res)
                textJobs.Add(item);
        }

        /// <summary>
        /// extract jobs from agahi_ir
        /// </summary>
        public bool ExtractAgahiJobs(List<Job> jobs, HtmlDocument doc, string rootUrl,
            string stopperUrl, DateTime? stopperDate, int? stopperRecordCount)
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
                if (job.OriginalDate != null && stopperDate != null && job.OriginalDate.Value < stopperDate.Value)
                    return false;

                //stop extracting if stopperUrl has been found
                if (stopperUrl != null && job.Url == stopperUrl)
                    return false;

                jobs.Add(job);
            }

            return true;
        }

        public bool SupportPaging(AdSource adSource)
        {
            throw new NotImplementedException();
        }

        public bool SupportOriginalDate(AdSource adSource)
        {
            throw new NotImplementedException();
        }
    }
}
