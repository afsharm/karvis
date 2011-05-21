using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Syndication;
using SJ.Core;

namespace SJ.Web.Feed
{
    
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
                IncludeExceptionDetailInFaults = true)]
    public class Feed : IFeed
    {
        public string AllJobs()
        {
            return "WTF";

            SyndicationFeed feed = new SyndicationFeed("کارویس - فهرست همه آگهی‌ها", "نمایش فهرست همه کارهای ثبت شده در سیستم", new Uri(GeneralHelper.GetSiteUrl()));
            feed.Authors.Add(new SyndicationPerson(GeneralHelper.GetAppEmail()));
            feed.Categories.Add(new SyndicationCategory("همه آگهی‌ها"));
            feed.Description = new TextSyndicationContent("نمایش فهرست همه کارهای ثبت شده در سیستم");

            List<SyndicationItem> items = new List<SyndicationItem>();

            foreach (Job job in JobDao.GetAllJobs())
            {
                SyndicationItem item = new SyndicationItem(
                    job.Title,
                    job.Description,
                    new Uri(Job.GetJobUrl(job.ID.ToString(), job.Title)),
                    job.ID.ToString(),
                    job.DateAdded.Value);

                items.Add(item);
            }

            feed.Items = items;

            //return new Rss20FeedFormatter(feed);
        }
    }
}
