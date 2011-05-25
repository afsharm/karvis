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

        public static IDictionary<string, UInt16> AnalyseTags(List<string> rawTags)
        {
            //todo
            IDictionary<string, UInt16> result = new Dictionary<string, UInt16>();

            result.Add("WCF", 4);
            result.Add(".Net", 3);
            result.Add("آزمایش", 1);
            result.Add("C#", 4);
            result.Add(".Net66", 13);
            result.Add("ایران", 1);
            result.Add("ASP", 4);
            result.Add("PHP", 7);
            result.Add("تست", 9);
            result.Add("Java", 4);
            result.Add("NHibernate", 11);
            result.Add("Social", 21);

            return result;
        }
    }
}
