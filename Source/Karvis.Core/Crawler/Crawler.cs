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
    public class Crawler : ICrawler
    {
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

        public List<string> ExtractEmails(string url)
        {
            string content = GetWebText(url);
            return ExtractEmailsByText(content);
        }

        public List<string> ExtractEmailsByText(string content)
        {
            List<string> retval = new List<string>();
            string MatchEmailPattern = @"(\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})";

            MatchCollection matches = Regex.Matches(content, MatchEmailPattern);
            foreach (Match match in matches)
            {
                retval.Add(match.Value);
            }

            return retval;
        }

        public List<JobDto> ExtractJobs(string url)
        {
            List<JobDto> retval = new List<JobDto>();

            string pageContent = GetWebText(url);
            HtmlNodeCollection jobNodes = ExtractHtmlJobs(pageContent);
            string rootUrl = ExtractRootUrl(url);
            foreach (var item in jobNodes)
                retval.Add(PrepareJobDto(ExtractJobDescription(item), ExtractJobUrl(item, rootUrl)));

            return retval;
        }

        public JobDto PrepareJobDto(string description, string url)
        {
            JobDto jobDto = new JobDto()
            {
                Description = description,
                PossibleEmails = ExtractEmailsByText(description),
                PossibleTags = ExtractPossibleTags(description),
                Title = ExtractTitle(description),
                Url = url
            };

            return jobDto;
        }

        private string ExtractTitle(string description)
        {
            //todo
            return string.Empty;
        }

        private List<string> ExtractPossibleTags(string description)
        {
            //todo
            return new List<string>();
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

        public string ExtractJobUrl(HtmlNode item, string rootUrl)
        {
            string plainLink = item.ChildNodes[3].ChildNodes[0].Attributes["href"].Value;
            string processedLink = ProcessLink(plainLink, rootUrl);

            return processedLink;
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

        public HtmlNodeCollection ExtractHtmlJobs(string pageContent)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageContent);

            return doc.DocumentNode.SelectNodes("//div[@id='listing']");
        }
    }
}
