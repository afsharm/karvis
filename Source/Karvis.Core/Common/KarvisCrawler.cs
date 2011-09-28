using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;

namespace Karvis.Core
{
    public class KarvisCrawler : IKarvisCrawler
    {
        const string MatchEmailPattern = @"([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})";
        /// <summary>
        /// retrieving web content of the given url
        /// </summary>
        public Stream GetWebTextStream(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

            request.UserAgent = "Karvis Web Crawler";
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            return stream;
        }

        /// <summary>
        /// retrieving web content of the given url in string format. This solves unicode problems.
        /// </summary>
        public string GetWebText(string url)
        {
            StreamReader reader = new StreamReader(GetWebTextStream(url));
            string htmlText = reader.ReadToEnd();

            return htmlText;
        }

        /// <summary>
        /// Extracts emails from the given text using regular expressions
        /// </summary>
        public string ExtractEmailsByText(string content)
        {
            string retval = string.Empty;

            MatchCollection matches = Regex.Matches(content, MatchEmailPattern);
            foreach (Match match in matches)
                retval += match.Value + ", ";

            return retval;
        }
    }
}
