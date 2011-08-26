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
    public class Crawler
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
            //http://www.codeproject.com/KB/aspnet/WebScraping.aspx
            IList<string> retval = new List<string>();
            string pageContent = GetWebText(url);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageContent);

            //HtmlNodeCollection jobs = ExtractHtmlJobs(
            //doc.DocumentNode.SelectNodes("//h3")[0].ChildNodes[1].Attributes[0].Value
            foreach (var item in doc.DocumentNode.SelectNodes("//div[@id='listing']"))
            {
                //string link = item.ChildNodes[3].ChildNodes[1].Attributes["href"].Value;
                string link = item.ChildNodes[3].ChildNodes[0].Attributes["href"].Value;
                string description = item.ChildNodes[4].InnerHtml;

                retval.Add(description);
            }

            return retval;
        }
    }
}
