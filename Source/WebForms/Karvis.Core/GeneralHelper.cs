using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Web;
using System.Configuration;
using System.ServiceModel;

namespace Karvis.Core
{
    public class GeneralHelper
    {
        public static string GetAppUrlPure()
        {
            //HttpContext is null when this method is called in a WCF service

            if (HttpContext.Current != null)
                return HttpContext.Current.Request.Url.Authority;
            else
                return OperationContext.Current.Host.BaseAddresses[0].Authority;
        }

        public static string GetAppUrl()
        {
            return string.Format("http://{0}/", GetAppUrlPure());
        }

        public static string GetAppEmail()
        {
            return ConfigurationManager.AppSettings["email"];
        }

        public static IOrderedEnumerable<KeyValuePair<string, ushort>> AnalyseTags(IList<string> rawTags)
        {
            var result = new SortedDictionary<string, UInt16>(StringComparer.OrdinalIgnoreCase);

            foreach (string tagList in rawTags)
            {
                foreach (string tag in tagList.Split(','))
                {
                    string trimmedTag = tag.Trim();

                    if (!result.ContainsKey(trimmedTag))
                        result.Add(trimmedTag, 0);

                    result[trimmedTag]++;
                }
            }

            return result.OrderByDescending(x => x.Value);
        }
    }
}
