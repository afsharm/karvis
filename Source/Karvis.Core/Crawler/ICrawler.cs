using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.IO;

namespace Karvis.Core
{
    interface ICrawler
    {
        IList<string> ExtractEmails(string url);
        HtmlNodeCollection ExtractHtmlJobs(string pageContent);
        string ExtractJobDescription(global::HtmlAgilityPack.HtmlNode item, string rootUrl);
        IList<string> ExtractJobs(string url);
        string ExtractRootUrl(string url);
        string GetWebText(string url);
        Stream GetWebTextStream(string url);
        string ProcessDescription(string plainDescription);
        string ProcessLink(string plainLink, string rootUrl);
    }
}
