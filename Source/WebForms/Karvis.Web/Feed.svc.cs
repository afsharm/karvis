using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Syndication;
using Karvis.Core;

namespace Karvis.Web
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
                IncludeExceptionDetailInFaults = true)]
    public class Feed : IFeed
    {
        public SyndicationFeedFormatter All(string format)
        {
            return FeedHelper.All(format);
        }

        public SyndicationFeedFormatter ByTag(string tag, string format)
        {
            return FeedHelper.ByTag(tag, format);
        }
    }
}
