namespace Karvis.Domain.JobExtract
{
    public interface IExtractorHelper
    {
        void GetReady(AdSource siteSource);
        string[] GetSiteSourceUrl(AdSource siteSource);
        bool JobUrlExists(string jobUrl);
        string ProcessTitle(string source);
        string ProcessDescription(string plainDescription);
        void ProcessJob(Job job);
        string GetAbsoluteUrl(string relativeUrl, string rootUrl);
        string ExtractTags(string text);
        string ExtractRootUrl(string url);
    }
}
