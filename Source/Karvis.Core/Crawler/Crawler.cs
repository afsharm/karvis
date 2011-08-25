using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace Karvis.Core
{
    public class Crawler
    {
        public string GetWebText(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

            request.UserAgent = "Karvis Web Crawler";
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
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
            //var pageContent = GetWebText(url);
            var pageContent = "<div class=\"listimage\"><img title=\"برنامه‌نويس #C  مسلط به\" src=\"http://www.rahnama.com/images/ricon.jpg\"></div>        ";

            //string pattern = "div"
            Regex r = new Regex(url);
            pageContent = pageContent.Replace("\r", "").Replace("\n", "").Replace("\t", "");
            MatchCollection mcl = r.Matches(pageContent);

            // loop through each minister to construct the source XML
            foreach (Match ml in mcl)
            {
                retval.Add(ml.Value);
            }

            return retval;
        }
    }
}
