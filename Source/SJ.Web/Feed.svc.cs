using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Syndication;
using SJ.Core;

namespace SJ.Web
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
                IncludeExceptionDetailInFaults = true)]
    public class Feed : IFeed
    {
        public Rss20FeedFormatter AllJobs()
        {
            return FeedHelper.AllJobs();
        }
    }
}
