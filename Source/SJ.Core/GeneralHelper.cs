using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Web;
using System.Configuration;

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

        public static string GetSiteUrlPure()
        {
            return HttpContext.Current.Request.Url.Authority;
        }

        public static string GetSiteUrl()
        {
            return string.Format("http://{0}/", GetSiteUrlPure());
        }

        public static string GetAppEmail()
        {
            return ConfigurationManager.AppSettings["email"];
        }
    }
}
