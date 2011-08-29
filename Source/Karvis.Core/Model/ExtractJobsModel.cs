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
            string MatchEmailPattern = @"(\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})";

            MatchCollection matches = Regex.Matches(content, MatchEmailPattern);
            foreach (Match match in matches)
                retval += match.Value + ", ";

            return retval;
        }

        public List<Job> ExtractJobs(string url)
        {
            List<Job> retval = new List<Job>();
            HtmlNodeCollection textJobs;
            HtmlNodeCollection imageJobs;
            ExtractHtmlJobs(url, out textJobs, out imageJobs);

            string rootUrl = ExtractRootUrl(url);
            foreach (var item in textJobs)
            {
                Job job = CreateTextJob(rootUrl, item);

                retval.Add(job);
            }

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

        private Job CreateTextJob(string rootUrl, HtmlNode item)
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
                tag = ExtractPossibleTags(description);
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
                Url = processedLink
            };

            return job;
        }

        private string ExtractTitle(string description)
        {
            //todo
            return string.Empty;
        }

        private string ExtractPossibleTags(string description)
        {
            //todo
            return string.Empty;
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

        public void ExtractHtmlJobs(string url, out HtmlNodeCollection textJobs, out HtmlNodeCollection imageJobs)
        {
            textJobs = new HtmlNodeCollection(null);
            imageJobs = new HtmlNodeCollection(null);

            string thisUrl = url;

            bool hasPaging = true;
            HtmlNodeCollection paging = null;
            do
            {
                string pageContent = GetWebText(thisUrl);
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(pageContent);

                var res = doc.DocumentNode.SelectNodes("//div[@id='listing']");
                foreach (var item in res)
                    textJobs.Add(item);

                var imageRes = doc.DocumentNode.SelectNodes("//div[@class='image-container']");
                if (imageRes != null) //if any image advertise exists at all
                    foreach (var item in imageRes)
                        imageJobs.Add(item);

                paging = doc.DocumentNode.SelectNodes("//a[@title='بعدی']");
                hasPaging = paging != null && paging.Count > 0;
                if (hasPaging)
                    thisUrl = ExtractRootUrl(url) + paging[0].Attributes["href"].Value;
            }
            while (hasPaging);
        }
    }
}
