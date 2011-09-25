using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.IO;

namespace Karvis.Core
{
    public interface IExtractJobsModel
    {
        Job CreateTextJob(string rootUrl, HtmlNode item);
        string ExtractEmails(string url);
        string ExtractEmailsByText(string content);
        void ExtractHtmlJobs(string url, out HtmlNodeCollection textJobs, out HtmlNodeCollection imageJobs);
        List<Job> ExtractImageJobs(HtmlNodeCollection imageJobs, string rootUrl);
        string ExtractJobDescription(HtmlNode item);
        List<Job> ExtractJobs(AdSource siteSource);
        string ExtractRootUrl(string url);
        string ExtractTags(string description);
        List<Job> ExtractTextJobs(HtmlNodeCollection textJobs, string rootUrl);
        string GetWebText(string url);
        Stream GetWebTextStream(string url);
        string ProcessDescription(string plainDescription);
        string ProcessLink(string plainLink, string rootUrl);
        void ExtractRawImageJobs(HtmlNodeCollection imageJobs, HtmlDocument doc);
        void ExtractRawTextJobs(HtmlNodeCollection textJobs, HtmlDocument doc);
    }
}
