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

        bool ExtractAgahiJobs(List<Job> realJobs, HtmlDocument doc, string rootUrl, int? limitDays, int? stopperRecordCount);
        Job ExtractAgahiJob(HtmlNode item, HtmlNode contact);

        bool ExtractNofaJobs(List<Job> realJobs, HtmlDocument doc, int? limitDays, int? stopperRecordCount, string rootUrl);
        Job ExtractNofaJob(string rootUrl, HtmlNode item);
    }
}
