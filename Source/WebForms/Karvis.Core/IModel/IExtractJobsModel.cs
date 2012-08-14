using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.IO;

namespace Karvis.Core
{
    public interface IExtractJobsModel
    {
        List<Job> ExtractJobs(AdSource siteSource, int? limitDays, int? countLimit);
        List<Job> ExtractSingleUrlJobs(AdSource siteSource, string url, string rootUrl,
            int? limitDays, int? stopperRecordCount);

        bool ExtractRahnamaJobs(List<Job> realJobs, HtmlDocument doc, string rootUrl, int? limitDays, int? stopperRecordCount, bool isImageJob);
        Job ExtractRahnamaTextJob(string rootUrl, HtmlNode item);
        Job ExtractRahnamaImageJob(HtmlNode item);
        bool HasPagingRahnama(HtmlDocument doc, string originalUrl, ref string nextPageUrl, string rootUrl);

        bool ExtractAgahiJobs(List<Job> realJobs, HtmlDocument doc, string rootUrl, int? limitDays, int? stopperRecordCount);
        Job ExtractAgahiJob(HtmlNode item, HtmlNode contact);
        bool HasPagingAgahi(HtmlDocument doc, ref string nextPageUrl);

        bool ExtractNofaJobs(List<Job> realJobs, HtmlDocument doc, int? limitDays, int? stopperRecordCount, string rootUrl);
        Job ExtractNofaJob(string rootUrl, HtmlNode item);
        void ExtractNofaJobComplementary(Job job);
        bool HasPagingNofa(HtmlDocument doc, ref string url);
    }
}
