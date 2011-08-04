using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace SJ.Core
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

            var pageContent = GetWebText(url);

            // use regular expression to find matching data portion
            Regex r = new Regex("<div class=\"grid_3 noborder center\">");// + 
            //"<a href="biography.asp\\?MPPID=[0-9]+"><imgsrc="http://" + 
            //"www.premier.gov.on.ca/photos/team/[A-Za-z]+" + 
            //".jpg"width="144" height="171" alt="[A-Za-z .]+'s " + 
            //"Biography" /></a></div><div class="grid_3"><h3>" + 
            //"<a href="biography.asp\\?MPPID=[0-9]+"title="[A-Za-z .]+'s " + 
            //"Biography">[A-Za-z .]+</a></h3><p>[A-Za-z .,¡¯'-]+" + 
            //"(<br />[A-Za-z .,¡¯'-]+)+</p><ul>(<li><a href="http://" + 
            //"[A-Za-z.]+.ca/[0-9A-Za-z./&=;\\?]+">[0-9A-Za-z .,-¡¯;" + 
            //"&#]+</a></li>)+</ul></div>");
            pageContent = pageContent.Replace("\r", "").Replace("\n", "").Replace("\t", "");
            MatchCollection mcl = r.Matches(pageContent);

            // loop through each minister to construct the source XML
            foreach (Match ml in mcl)
            {
                string xmlNode = ml.Groups[0].Value.Replace("imgsrc", "img src").Replace(
                  "width", " width").Replace("title", " title").Replace("\\\"", "\"");

                //XmlReader xmlReader = XmlReader.Create(new StringReader(
                //"<Minister>" + xmlNode + "</Minister>"));
                //xmlelemRoot.AppendChild(srcDoc.ReadNode(xmlReader));
            }

            return null;
        }
    }
}
