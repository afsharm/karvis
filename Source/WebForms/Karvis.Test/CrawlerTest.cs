using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Karvis.Core;
using System.Text.RegularExpressions;
using Fardis;

namespace Karvis.Test
{
    [TestFixture]
    public class CrawlerTest
    {
        IKarvisCrawler karvisCrawler;

        [SetUp]
        public void SetupTest()
        {
            karvisCrawler = new KarvisCrawler();
        }

        [Test]
        public void ExtractEmails()
        {
            const int testCount = 12;

            string[] raw = new string[testCount]
            {
                "برنامه نویس مسلط به #C و NET. و ترجیحا آشنا با SharePoint با حداقل 1 سال سابقه کار آدرس ارسال رزومه : hacjob89@gmail.com فکس : 88279808\n\r"
                +"تلفن : 021-88279808\n\r"
                +"021-88279808",

                ", نیروی ماهر پشتیبانی , حداقل 3 سال سابقه , پشتیبانی سیستم های نرم افزاری , مهارت کامل در SQL Server , آمادگی برای ماموریتهای شهرستان , افراد ماهر در سیستمهای مالی یا , هتلداری درالویتند،تماس9الی17\n\r"
                +"gmail.com@ Poshtiban.Marlik 88532979 ",

                ", Senior Developer , C# .NET , , , jobs@pendarpajouh , com .\n\r"
                +"88140142",

                ", شرکت معتبر , عمرانی - نرم افزاری , برنامه نویسان مسلط به Delphi , را دعوت به همکاری می نماید\n\r"
                +"فكس: 88256520 com. resume@rayansazeh ",

                ", متخصص شبکه ccnp,ccna , برنامه نویس حرفه ای وب , ارسال رزومه به :\n\r"
                +"info@ITSecTeam.com",

                ", برنامه نویس خانم مسلط به , ASP - C#-SQL ,\n\r"
                +"tehranitjob@gmail.com ",

                ", شرکت معتبر نرم افزاری , به چند نفر برنامه نویس , حرفه ای با مشخصات ذیل , نیازمند است : , 1-برنامه نویس VB6,VBA , آشنابه بانک اطلاعاتی SQL , وهمچنین سیستم های یکی , ازحوزه های مالی،اداری، , بازرگانی و یاصنعتی , 2-برنامه نویس سیستم های , تحت و ب در محیط VB.NET , علاقمندان می توانند سوابق , خودرافکس ویاباشماره های , زیر تماس حاصل نمایند.\r\n"
                +"فكس: 22027534 تلفن: 5 - 22014292 ",

                ", برنامه نویس , آشنا به زبان java , ارسال رزومه :\r\n"
                +"fc.java.job@gmail.com ",

                ", برنامه نویس , آشنا و مسلط به , C#،SQL،ASP.NET , Entity Framwork , معماری لایه بندی (تمام وقت) , ارسال رزومه: , info@ratec.ir",

                ", ASP.Net Developer , 4years experience , C# , Jquery , CSS , ,\r\n"
                +"gmail.com @recarmandar ",

                ", برنامه نویس , مسلط به Ruby on Rails , نیازمندیم ,\r\n"
                +"88456802 ",

                ", کارشناس نرم افزارخانم , برنامه نویس و مسلط به , SQL و vb , با سابقه کار مرتبط , info@protopsystem"
            };

            string[] expected = new string[testCount]
            {
                "hacjob89@gmail.com",
                "poshtiban.marlik@gmail.com",
                "jobs@pendarpajouh.com",
                "resume@rayansazeh.com",
                "info@itsecteam.com",
                "tehranitjob@gmail.com",
                "",
                "fc.java.job@gmail.com",
                "info@ratec.ir",
                "recarmandar@gmail.com",
                "",
                "info@protopsystem.com"
            };

            for (int i = 0; i < testCount; i++)
                Assert.AreEqual(expected[i], karvisCrawler.ExtractEmailsByText(ref raw[i]), i.ToString());
        }
    }
}