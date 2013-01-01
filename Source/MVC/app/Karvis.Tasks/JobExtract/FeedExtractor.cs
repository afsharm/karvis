using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
using Karvis.Domain;
using Karvis.Domain.JobExtract;

namespace Karvis.Tasks.JobExtract
{
    public class FeedExtractor : IFeedExtractor
    {
        readonly IExtractorHelper _extractorHelper;

        public FeedExtractor(IExtractorHelper extractorHelper)
        {
            _extractorHelper = extractorHelper;
        }

        public List<Job> ExtractFeed(AdSource siteSource)
        {
            var result = new List<Job>();

            var urls = _extractorHelper.GetSiteSourceUrl(siteSource);
            _extractorHelper.GetReady(siteSource);

            foreach (var url in urls)
            {
                var xml = new XmlTextReader(url);
                SyndicationFeed feed = SyndicationFeed.Load(xml);

                foreach (var job in feed.Items.Select(item => new Job
                                                                  {
                                                                      Description = item.Summary != null ? item.Summary.Text : string.Empty,
                                                                      Url = item.Links.Count > 0 ? item.Links[0].Uri.ToString() : string.Empty,
                                                                      OriginalDate = item.PublishDate.UtcDateTime,
                                                                      Title = item.Title.Text,
                                                                      AdSource = siteSource,
                                                                      IsActive = true
                                                                  }).Where(job => !_extractorHelper.JobUrlExists(job.Url)))
                {
                    _extractorHelper.ProcessJob(job);
                    result.Add(job);
                }
            }

            return result;
        }
    }
}
