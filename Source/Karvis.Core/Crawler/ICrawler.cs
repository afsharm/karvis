using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.IO;

namespace Karvis.Core
{
    public interface ICrawler
    {
        List<string> ExtractEmails(string url);
        HtmlNodeCollection ExtractHtmlJobs(string pageContent);
        string ExtractJobDescription(HtmlAgilityPack.HtmlNode item);
        List<JobDto> ExtractJobs(string url);
        string ExtractJobUrl(HtmlAgilityPack.HtmlNode item, string rootUrl);
        string ExtractRootUrl(string url);
        string GetWebText(string url);
        Stream GetWebTextStream(string url);
        JobDto PrepareJobDto(string description, string url);
        string ProcessDescription(string plainDescription);
        string ProcessLink(string plainLink, string rootUrl);
    }
}
