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

        public IList<string> ExtractEmails(string url)
        {
            IList<string> retval = new List<string>();
            string content = GetWebText(url);


            string MatchEmailPattern = @"(\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})";

            MatchCollection matches = Regex.Matches(content, MatchEmailPattern);
            foreach (Match match in matches)
            {
                retval.Add(match.Value);
            }

            return retval;
        }

        public IList<string> ExtractJobs(string url)
        {
            IList<string> retval = new List<string>();
            string pageContent = GetWebText(url);
            HtmlNodeCollection jobNodes = ExtractHtmlJobs(pageContent);
            string rootUrl = ExtractRootUrl(url);
            foreach (var item in jobNodes)
                retval.Add(ExtractJobDescription(item, rootUrl));

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

        public string ExtractJobDescription(HtmlNode item, string rootUrl)
        {
            string plainLink = item.ChildNodes[3].ChildNodes[0].Attributes["href"].Value;
            string plainDescription = item.ChildNodes[4].InnerHtml;

            string processedLink = ProcessLink(plainLink, rootUrl);
            string processedDescription = ProcessDescription(plainDescription);

            return processedDescription;
        }

        public string ProcessDescription(string plainDescription)
        {
            throw new NotImplementedException();
        }

        public string ProcessLink(string plainLink, string rootUrl)
        {
            throw new NotImplementedException();
        }

        public HtmlNodeCollection ExtractHtmlJobs(string pageContent)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageContent);

            return doc.DocumentNode.SelectNodes("//div[@id='listing']");
        }
    }
}
