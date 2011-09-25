using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Web;

namespace Karvis.Core
{
    public class ExtractJobsModel : IExtractJobsModel
    {
        public ExtractJobsModel()
        {
        }

        public Stream GetWebTextStream(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

            request.UserAgent = "Karvis Web Crawler";
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            return stream;
        }

        public string GetWebText(string url)
        {
            StreamReader reader = new StreamReader(GetWebTextStream(url));
            string htmlText = reader.ReadToEnd();

            return htmlText;
        }

        public string ExtractEmails(string url)
        {
            string content = GetWebText(url);
            return ExtractEmailsByText(content);
        }

        public string ExtractEmailsByText(string content)
        {
            string retval = string.Empty;
            string MatchEmailPattern = @"([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})";

            MatchCollection matches = Regex.Matches(content, MatchEmailPattern);
            foreach (Match match in matches)
                retval += match.Value + ", ";

            return retval;
        }

        public List<Job> ExtractJobs(AdSource siteSource)
        {
            string url = ExtractSiteSourceUrl(siteSource);

            List<Job> retval = new List<Job>();
            HtmlNodeCollection textJobs;
            HtmlNodeCollection imageJobs;
            HtmlNodeCollection agahiContacts;
            ExtractHtmlJobs(siteSource, url, out textJobs, out imageJobs, out agahiContacts);

            string rootUrl = ExtractRootUrl(url);

            retval.AddRange(ExtractTextJobs(siteSource, textJobs, agahiContacts, rootUrl));

            if (siteSource == AdSource.rahnama_com)
                retval.AddRange(ExtractImageJobs(imageJobs, rootUrl));

            return retval;
        }

        private string ExtractSiteSourceUrl(AdSource siteSource)
        {
            switch (siteSource)
            {
                case AdSource.rahnama_com:
                    return "http://www.rahnama.com/component/mtree/%DA%AF%D8%B1%D9%88%D9%87/35179/%D8%A8%D8%B1%D9%86%D8%A7%D9%85%D9%87-%D9%86%D9%88%D9%8A%D8%B3.html";
                case AdSource.agahi_ir:
                    return "http://www.agahi.ir/category/14";

                case AdSource.irantalent_com:
                case AdSource.developercenter_ir:
                case AdSource.itjobs_ir:
                case AdSource.istgah_com:
                case AdSource.nofaـir:
                case AdSource.unp_ir:
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

        public List<Job> ExtractImageJobs(HtmlNodeCollection imageJobs, string rootUrl)
        {
            List<Job> retval = new List<Job>();

            foreach (var item in imageJobs)
            {
                string link = item.ChildNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[1].Attributes["href"].Value;
                Job job = new Job()
                {
                    Url = link,
                    Title = "آگهی عکسی"
                };

                retval.Add(job);

            }

            return retval;
        }

        public List<Job> ExtractTextJobs(AdSource siteSource, HtmlNodeCollection textJobs, HtmlNodeCollection agahiContacts, string rootUrl)
        {
            List<Job> retval = new List<Job>();

            for (int i = 0; i < textJobs.Count; i++)
            {
                var item = textJobs[i];
                Job job = null;
                switch (siteSource)
                {
                    case AdSource.rahnama_com:
                        job = CreateTextJobRahnama(rootUrl, item);
                        break;
                    case AdSource.agahi_ir:
                        job = ExtractJobAgahi(item, agahiContacts[i]);
                        break;
                }
                retval.Add(job);
            }

            return retval;
        }

        public Job CreateTextJobRahnama(string rootUrl, HtmlNode item)
        {
            string plainLink;
            string processedLink;
            string title;
            string description;
            string emails;
            string tag;

            //to ignore possible error
            try
            {
                plainLink = item.ChildNodes[3].ChildNodes[0].Attributes["href"].Value;
                processedLink = ProcessLink(plainLink, rootUrl);
                title = item.ChildNodes[3].ChildNodes[0].InnerText;
                description = ExtractJobDescription(item);
                emails = ExtractEmailsByText(description);
                tag = ExtractTags(description);
            }
            catch (Exception ex)
            {
                plainLink = "N/A";
                processedLink = "N/A";
                title = "N/A";
                description = ex.Message;
                emails = "N/A";
                tag = "N/A";
            }

            Job job = new Job()
            {
                Description = description,
                Emails = emails,
                Tag = tag,
                Title = title,
                Url = processedLink,
                AdSource = AdSource.rahnama_com
            };

            return job;
        }


        public Job ExtractJobAgahi(HtmlNode item, HtmlNode contact)
        {
            string processedLink;
            string title;
            string description;
            string emails;
            string tag;

            //to ignore possible error
            try
            {
                processedLink = item.ChildNodes[1].Attributes["href"].Value;
                title = item.ChildNodes[1].ChildNodes[3].InnerText;
                string originalDate = item.ChildNodes[3].InnerText;
                description = string.Format("{0}, original date: {1}, contact: {2}", 
                    item.ChildNodes[6].InnerText, originalDate, contact.InnerText);
                emails = ExtractEmailsByText(description);
                tag = ExtractTags(description);
            }
            catch (Exception ex)
            {
                processedLink = "N/A";
                title = "N/A";
                description = ex.Message;
                emails = "N/A";
                tag = "N/A";
            }

            Job job = new Job()
            {
                Description = description,
                Emails = emails,
                Tag = tag,
                Title = title,
                Url = processedLink,
                AdSource = AdSource.agahi_ir
            };

            return job;
        }

        public string ExtractTags(string description)
        {
            string retval = string.Empty;
            string pattern = @"[a-zA-Z.#+_@]+";
            string source = description;

            foreach (string email in ExtractEmailsByText(source).Trim().Split(','))
                if (!string.IsNullOrEmpty(email))
                    source = source.Replace(email.Trim(), " ");

            if (string.IsNullOrEmpty(source.Trim()))
                return retval;

            List<string> tags = new List<string>();
            MatchCollection matches = Regex.Matches(source, pattern);
            foreach (Match match in matches)
                if (!tags.Contains(match.Value))
                    tags.Add(match.Value);

            foreach (var item in tags)
                retval += item + ", ";

            return retval;
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

        public string ExtractJobDescription(HtmlNode item)
        {
            string plainDescription = item.ChildNodes[4].InnerHtml;
            string processedDescription = ProcessDescription(plainDescription);

            return processedDescription;
        }

        public string ProcessDescription(string plainDescription)
        {
            return plainDescription
                .Replace("<br>", " ")
                .Replace("</br>", " ")
                .Replace("<span>", " ")
                .Replace("</span>", " ");
        }

        public string ProcessLink(string plainLink, string rootUrl)
        {
            return string.Format("{0}{1}", rootUrl, plainLink);
        }

        public void ExtractHtmlJobs(AdSource siteSource, string url, out HtmlNodeCollection textJobs,
            out HtmlNodeCollection imageJobs, out HtmlNodeCollection agahiContactJobs)
        {
            textJobs = new HtmlNodeCollection(null);
            imageJobs = new HtmlNodeCollection(null);
            agahiContactJobs = new HtmlNodeCollection(null);

            string thisUrl = url;

            bool hasPaging = false;
            do
            {
                string pageContent = GetWebText(thisUrl);
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(pageContent);

                switch (siteSource)
                {
                    case AdSource.rahnama_com:
                        ExtractRahnamaRawTextJobs(textJobs, doc);
                        ExtractRawImageJobs(imageJobs, doc);
                        hasPaging = HasPagingRahnama(doc, url, ref thisUrl);
                        break;
                    case AdSource.agahi_ir:
                        ExtractAgahiRawTextJobs(textJobs, agahiContactJobs, doc);
                        hasPaging = HasPagingAgahi(doc, ref thisUrl);
                        break;
                }
            }
            while (hasPaging);
        }

        private bool HasPagingRahnama(HtmlDocument doc, string originalUrl, ref string nextPageUrl)
        {
            HtmlNodeCollection paging = null;

            paging = doc.DocumentNode.SelectNodes("//a[@title='بعدی']");
            bool hasPaging = paging != null && paging.Count > 0;

            if (hasPaging)
                nextPageUrl = ExtractRootUrl(originalUrl) + paging[0].Attributes["href"].Value;

            return hasPaging;
        }

        private bool HasPagingAgahi(HtmlDocument doc, ref string nextPageUrl)
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

        public void ExtractRawImageJobs(HtmlNodeCollection imageJobs, HtmlDocument doc)
        {
            var imageRes = doc.DocumentNode.SelectNodes("//div[@class='image-container']");
            if (imageRes != null) //if any image advertise exists at all
                foreach (var item in imageRes)
                    imageJobs.Add(item);
        }

        public void ExtractRahnamaRawTextJobs(HtmlNodeCollection textJobs, HtmlDocument doc)
        {
            var res = doc.DocumentNode.SelectNodes("//div[@id='listing']");
            foreach (var item in res)
                textJobs.Add(item);
        }

        public void ExtractRawTextJobs(HtmlNodeCollection textJobs, HtmlDocument doc)
        {
            var res = doc.DocumentNode.SelectNodes("//div[@id='listing']");
            foreach (var item in res)
                textJobs.Add(item);
        }

        public void ExtractAgahiRawTextJobs(HtmlNodeCollection body, HtmlNodeCollection contacts, HtmlDocument doc)
        {
            string description = doc.DocumentNode.SelectNodes("//div[@class='slr_box_contents']")[5].ChildNodes[4].InnerHtml;
            string title = doc.DocumentNode.SelectNodes("//div[@class='slr_box_contents']")[5].ChildNodes[6].InnerHtml;

            var jobs = doc.DocumentNode.SelectNodes("//div[@class='slr_box_contents']")[5].ChildNodes;
            for (int i = 4; i < jobs.Count - 2; i = i + 8)
            {
                body.Add(jobs[i]);
                contacts.Add(jobs[i + 2]);
            }
        }
    }
}
