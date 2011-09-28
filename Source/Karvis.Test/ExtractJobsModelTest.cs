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
    public class ExtractJobsModelTest
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

            IExtractJobsModel extractJobsModel = new ExtractJobsModel();

            for (int i = 0; i < testCount; i++)
                Assert.AreEqual(expected[i], extractJobsModel.ExtractRootUrl(raw[i]), i.ToString());
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

            IExtractJobsModel extractJobsModel = new ExtractJobsModel();

            Regex regex = new Regex("<span>|<br>|</span>|</br>");
            foreach (var item in raw)
                Assert.AreEqual(0, regex.Matches(extractJobsModel.ProcessDescription(item)).Count, string.Format("problem in: {0}", item));
        }

        [Test]
        public void ProcessLinkTest()
        {
            const int testCount = 2;
            string rootUrl = "http://www.rahnama.com";

            string[] raw = new string[testCount]
            {
                "/component/mtree/آگهي/5796255/برنامه-نویس-حرفه-ای-نیازمندیم.html",
                "/123/456"
            };

            string[] answer = new string[testCount]
            {
                "http://www.rahnama.com/component/mtree/آگهي/5796255/برنامه-نویس-حرفه-ای-نیازمندیم.html",
                "http://www.rahnama.com/123/456"
            };

            IExtractJobsModel extractJobsModel = new ExtractJobsModel();

            for (int i = 0; i < testCount; i++)
                Assert.AreEqual(answer[i], extractJobsModel.GetAbsoluteUrl(raw[i], rootUrl));
        }

        [Test]
        public void ExtractEmailsByTextTest()
        {
            const int testCount = 5;
            string[] raw = new string[]
            {
                "dfsdf kshdfk sdfkjh@dfjsf.com",
                "yyy@iran.ir, kkkk@lksdf ,,,,,sdf lksd llkjdf @ sdfl k@d.org",
                "",
                "skdhskhdfkshdfkshdfkskjdf sdkh sdlf ",
                "abc@zdef.co"//"abc@zdef.co.uk"
            };
            string[] expected = new string[testCount]
            {
                "sdfkjh@dfjsf.com, ",
                "yyy@iran.ir, k@d.org, ",
                "",
                "",
                "abc@zdef.co, "//"abc@zdef.co.uk, "
            };

            IExtractJobsModel model = new ExtractJobsModel();
            IKarvisCrawler crawler = new KarvisCrawler();
            for (int i = 0; i < testCount; i++)
                Assert.AreEqual(expected[i], crawler.ExtractEmailsByText(raw[i]), i.ToString());
        }

        [Test]
        public void ExtractTagsTest()
        {
            const int testCount = 14;
            string[] raw = new string[testCount]
            {
                ", برنامه نویس PHP , مسلط به , PHP Programming و , پرتال نویسی و طراحی قالب , و Javascript , با تجربه عالی , جهت انجام پروژه های حرفه ای yahoo.com @amir_rajabi88 تلفن: 22674456 ",
                "",
                "",
                "skdhskhdfkshdfkshdfkskjdf sdkh sdlf ",
                "abc@zdef.co",
                "asp.net",
                "asp.net, vb.net",
                "C#",
                "jack@smith.net",
                ".net",
                "C++",
                "A+",
                " , برنامه نویس مسلط به , SQL,C#,Asp.net  فكس    22461236 Job@Azmdp.com  ",
                " , برنامه نویس C# و .NET  , مسلط به ASP.NET4وSQL , و WCF و معماری چندلایه  jobs@alphakaran.com  "
            };
            string[] expected = new string[testCount]
            {
                "PHP, Programming, Javascript, yahoo.com, @amir_rajabi, ",
                "",     
                "",
                "skdhskhdfkshdfkshdfkskjdf, sdkh, sdlf, ",
                "",
                "asp.net, ",
                "asp.net, vb.net, ",
                "C#, ",
                "",
                ".net, ",
                "C++, ",
                "A+, ",
                "SQL, C#, Asp.net, ",
                "C#, .NET, ASP.NET, SQL, WCF, "
            };

            IExtractJobsModel model = new ExtractJobsModel();
            for (int i = 0; i < testCount; i++)
                Assert.AreEqual(expected[i], model.ExtractTags(raw[i]), i.ToString());
        }

    }
}