using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Syndication;

namespace SJ.Core
{
    public class FeedHelper
    {
        public static Rss20FeedFormatter AllJobs()
        {
            SyndicationFeed feed = new SyndicationFeed("کارویس - فهرست همه آگهی‌ها", "نمایش فهرست همه کارهای ثبت شده در سیستم", new Uri("http://afsharm.com/"));
            feed.Authors.Add(new SyndicationPerson(GeneralHelper.GetAppEmail()));
            feed.Categories.Add(new SyndicationCategory("همه آگهی‌ها"));
            feed.Description = new TextSyndicationContent("نمایش فهرست همه کارهای ثبت شده در سیستم");

            List<SyndicationItem> items = new List<SyndicationItem>();

            foreach (Job job in JobDao.GetAllJobs())
            {
                SyndicationItem item = new SyndicationItem(
                    job.Title,
                    job.Description,
                    new Uri(job.GetJobUrl()),
                    job.ID.ToString(),
                    job.DateAdded.Value);

                items.Add(item);
            }

            feed.Items = items;

            return new Rss20FeedFormatter(feed);
        }
    }
}
