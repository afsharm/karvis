using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Web;
using System.Configuration;
using System.ServiceModel;

namespace SJ.Core
{
    public class GeneralHelper
    {
        public static string ConvertToPersianDate(DateTime? date)
        {
            if (date == null)
                return string.Empty;

            PersianCalendar calendar = new PersianCalendar();

            int day = calendar.GetDayOfMonth(date.Value);
            int month = calendar.GetMonth(date.Value);
            int year = calendar.GetYear(date.Value);

            string persian = string.Format("{0}/{1}/{2}", year, month, day);

            return ConvertToPersianNumber(persian);
        }

        public static string ConvertToPersianNumber(string persian)
        {
            return persian
                .Replace('0', '۰')
                .Replace('1', '۱')
                .Replace('2', '۲')
                .Replace('3', '۳')
                .Replace('4', '۴')
                .Replace('5', '۵')
                .Replace('6', '۶')
                .Replace('7', '۷')
                .Replace('8', '۸')
                .Replace('9', '۹');
        }

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

        public static bool HasRtlText(string text)
        {
            for (int i = 0; i < text.Length; i++)
                if (IsRtlChar(text[i]))
                    return true;

            return false;
        }

        private static bool IsRtlChar(char character)
        {
            return (int)character > 128;
        }

        /// <summary>
        /// Converts characters like space and comma to underscore to be better SEO
        /// </summary>
        public static string GetSeoText(string text)
        {
            return text
                .Replace(' ', '_')
                .Replace(',', '_')
                .Replace('،', '_');
        }
    }
}
