using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.ServiceModel.Web;

namespace SJ.Core
{
    [ServiceContract]
    public interface IFeed
    {
        [OperationContract]
        [WebGet]
        Rss20FeedFormatter AllJobs();
    }
}
