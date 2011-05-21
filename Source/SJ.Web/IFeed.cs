using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using System.ServiceModel.Syndication;

namespace SJ.Web
{
    [ServiceContract]
    public interface IFeed
    {
        [OperationContract]
        [WebGet(UriTemplate = "/AllJobs")]
        Rss20FeedFormatter AllJobs();
    }
}
