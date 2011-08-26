using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Karvis.Core;
using System.Text.RegularExpressions;

namespace Karvis.Test
{
    [TestFixture]
    public class CrawlerTest
    {
        [Test]
        public void ExtractRootUrlTest()
        {
            const int testCount = 5;

            string[] raw = new string[testCount] {                               
                "http://www.rahnama.com/component/mtree/%DA%AF%D8%B1%D9%88%D9%87/35179/%D8%A8%D8%B1%D9%86%D8%A7%D9%85%D9%87-%D9%86%D9%88%D9%8A%D8%B3.html",
                "http://karvis.afsharm.com/admin/AddEditJob.aspx",
                "http://hootsuite.com/dashboard",
                "http://www.epay.ir/paypal.htm",
                "https://modern.enbank.net/ibnew/login/loginPage.action?serial=2262511290&currency=IRR&selectedDeposit=2306-800-3721964-1&accountType=SHORT_ACCOUNT&depositNumber=101-1-73925-1&firstTime=true" };

            string[] expected = new string[testCount] {
                "http://www.rahnama.com",
                "http://karvis.afsharm.com",
                "http://hootsuite.com",
                "http://www.epay.ir",
                "https://modern.enbank.net" };

            ICrawler crawler = new Crawler();

            for (int i = 0; i < testCount; i++)
                Assert.AreEqual(expected[i], crawler.ExtractRootUrl(raw[i]), i.ToString());
        }

        [Test]
        public void ProcessDescriptionTest()
        {
            const int testCount = 4;
            string[] raw = new string[testCount]
            {
                "شرکت سامه آرا جهت انجام پروژه های SAP-ERP در سازمانهای بزرگ به تعدادی برنامه نویس تمام وقت فارغ التحصیل از دانشگاههای معتبر دولتی در مقاطع کارشناسی به بالا به صورت بلند مدت نیازمند است. علاقمندان رزومه خود را به info@samehara.com ارسال فرمایند.<br> تلفن : 44697280<br><span>44697280</span>",
                ", برنامه نویس حرفه ای , مسلط به ASP.Netو#Cو , Jquery<br><span>jobs@faranam.net </span>",
                ", استخدام برنامه نویس PHP , ترجیحا خانم , Support@rahco.ir",
                ", استخدام فوری , در یک شرکت معتبر , 4 نفر برنامه نویس , SQL Server-ASP.NET , با تسلط کامل #C , info@behsazan.net"
            };

            ICrawler crawler = new Crawler();

            Regex regex = new Regex("<span>|<br>|</span>|</br>");
            foreach (var item in raw)
                Assert.AreEqual(0, regex.Matches(crawler.ProcessDescription(item)).Count, string.Format("problem in: {0}", item));
        }
    }
}