using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Syndication;
using System.Xml;

namespace Karvis.Core
{
    public class FeedExtractor : IFeedExtractor
    {
        IExtractorHelper _extractorHelper;

        public FeedExtractor(IExtractorHelper extractorHelper)
        {
            _extractorHelper = extractorHelper;
        }

        public List<Job> ExtractFeed(AdSource siteSource)
        {
            List<Job> result = new List<Job>();

            string[] urls = _extractorHelper.GetSiteSourceUrl(siteSource);
            _extractorHelper.GetReady(siteSource);

            foreach (var url in urls)
            {
                var xml = new XmlTextReader(url);
                SyndicationFeed feed = SyndicationFeed.Load(xml);

                foreach (var item in feed.Items)
                {
                    Job job = new Job
                    {
                        Description = item.Summary != null ? item.Summary.Text : string.Empty,
                        Url = item.Links.Count > 0 ? item.Links[0].Uri.ToString() : string.Empty,
                        OriginalDate = item.PublishDate.UtcDateTime,
                        Title = item.Title.Text,
                        AdSource = siteSource,
                        IsActive = true
                    };

                    if (!_extractorHelper.JobUrlExists(job.Url))
                    {
                        _extractorHelper.ProcessJob(job);
                        result.Add(job);
                    }
                }
            }

            return result;
        }
    }
}
