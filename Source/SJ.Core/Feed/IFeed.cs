using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using System.ServiceModel.Syndication;

namespace SJ.Core
{
    [ServiceContract]
    [ServiceKnownType(typeof(Atom10FeedFormatter))]
    [ServiceKnownType(typeof(Rss20FeedFormatter))]
    public interface IFeed
    {
        [OperationContract]
        [WebGet(UriTemplate = "All?format={format}")]
        SyndicationFeedFormatter All(string format);
    }
}
