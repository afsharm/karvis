using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.IO;

namespace Karvis.Core
{
    public interface IExtractJobsModel
    {
        string ExtractEmails(string url);
        void ExtractHtmlJobs(string url, out HtmlNodeCollection textJobs, out HtmlNodeCollection imageJobs);
        string ExtractJobDescription(HtmlNode item);
        List<Job> ExtractJobs(string url);
        string ExtractRootUrl(string url);
        string GetWebText(string url);
        Stream GetWebTextStream(string url);
        string ProcessDescription(string plainDescription);
        string ProcessLink(string plainLink, string rootUrl);
    }
}
