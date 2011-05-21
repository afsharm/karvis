using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Web;
using System.Configuration;
using System.Web;

namespace SJ.Core
{
    public class FeedHelper
    {
        private static FeedHelper instance;
        WebServiceHost svcHost;

        private FeedHelper()
        {
            string feedAddress = string.Format("{0}Feed", "http://localhost/");
            Uri baseAddress = new Uri(feedAddress);
            svcHost = new WebServiceHost(typeof(FeedService), baseAddress);
        }

        public static FeedHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FeedHelper();
                }
                return instance;
            }
        }

        public void StartService()
        {
            svcHost.Open();
        }

        public string GetInfo()
        {
            string urls = string.Empty;

            foreach (Uri item in svcHost.BaseAddresses)
            {
                urls += item.Authority + " -- ";
            }

            return string.Format("State: {0}, Urls: {1}", svcHost.State, urls);
        }
    }
}
