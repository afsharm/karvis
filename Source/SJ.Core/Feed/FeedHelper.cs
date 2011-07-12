using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Syndication;

namespace SJ.Core
{
    public class FeedHelper
    {
        public static SyndicationFeedFormatter All(string format)
        {
            return
                PrepareJobFeed(
                "کارویس - همه", "نمایش فهرست همه کارهای ثبت شده در سیستم",
                new Uri(GeneralHelper.GetAppUrl()),
                GeneralHelper.GetAppEmail(),
                "همه",
                "نمایش فهرست همه کارهای ثبت شده در سیستم",
                new JobModel().GetAllJobs(true),
                format
                );
        }

        public static SyndicationFeedFormatter ByTag(string tag, string format)
        {
            return
                PrepareJobFeed(
                string.Format("کارویس - با تگ {0}", tag),
                string.Format("فهرست مشاغل دارای تگ {0}", tag),
                new Uri(GeneralHelper.GetAppUrl()),
                GeneralHelper.GetAppEmail(),
                string.Format("تگ {0}", tag),
                string.Format("فهرست مشاغل ثبت شده با تگ {0}", tag),
                new JobModel().GetJobsByTag(tag, true),
                format
                );
        }

        private static SyndicationFeedFormatter PrepareJobFeed(
        string title, string description, Uri feedAlternateLink,
        string email, string name, string text,
        IEnumerable<Job> jobs, string format)
        {
            SyndicationFeed feed = new SyndicationFeed(
                title, description,
                feedAlternateLink);

            feed.Authors.Add(new SyndicationPerson(email));
            feed.Categories.Add(new SyndicationCategory(name));
            feed.Description = new TextSyndicationContent(text);

            List<SyndicationItem> items = new List<SyndicationItem>();

            var jobModel = new JobModel();

            foreach (Job job in jobs)
            {
                SyndicationItem item = new SyndicationItem(
                    job.Title,
                    jobModel.GetFeedDescription(job),
                    new Uri(jobModel.GetJobUrl(job)),
                    job.Id.ToString(),
                    job.DateAdded.Value);

                items.Add(item);
            }

            feed.Items = items;

            switch (format)
            {
                case "rss":
                    return new Rss20FeedFormatter(feed);
                case "atom":
                    return new Atom10FeedFormatter(feed);
                default:
                    throw new ApplicationException("Unkown feed format: " + format);
            }
        }
    }
}
