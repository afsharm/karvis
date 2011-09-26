using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.IO;

namespace Karvis.Core
{
    public interface IExtractJobsModel
    {
        Job CreateTextJobRahnama(string rootUrl, HtmlNode item);
        Job ExtractJobAgahi(HtmlNode item, HtmlNode contact, HtmlNode complementary);
        string ExtractEmails(string url);
        string ExtractEmailsByText(string content);
        void ExtractHtmlJobs(AdSource siteSource, string url, out HtmlNodeCollection textJobs,
            out HtmlNodeCollection imageJobs, out HtmlNodeCollection agahiContactJobs, out HtmlNodeCollection agahiComplementary);
        List<Job> ExtractImageJobs(HtmlNodeCollection imageJobs, string rootUrl);
        string ExtractJobDescription(HtmlNode item);
        List<Job> ExtractJobs(AdSource siteSource);
        string ExtractRootUrl(string url);
        string ExtractTags(string description);
        List<Job> ExtractTextJobs(AdSource siteSource, HtmlNodeCollection textJobs,
            HtmlNodeCollection agahiContacts, HtmlNodeCollection agahiComplementary, string rootUrl);
        string GetWebText(string url);
        Stream GetWebTextStream(string url);
        string ProcessDescription(string plainDescription);
        string ProcessLink(string plainLink, string rootUrl);
        void ExtractRawImageJobs(HtmlNodeCollection imageJobs, HtmlDocument doc);
        void ExtractRahnamaRawTextJobs(HtmlNodeCollection textJobs, HtmlDocument doc);
        void ExtractAgahiRawTextJobs(HtmlNodeCollection body, HtmlNodeCollection contacts,
            HtmlNodeCollection complementary, HtmlDocument doc);
    }
}
