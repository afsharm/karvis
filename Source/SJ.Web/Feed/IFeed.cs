using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Syndication;
using System.ServiceModel.Web;

namespace SJ.Web.Feed
{
    [ServiceContract]
    public interface IFeed
    {
        [OperationContract]
        [WebGet(UriTemplate = "/AllJobs/")]
        string AllJobs();
    }
}
