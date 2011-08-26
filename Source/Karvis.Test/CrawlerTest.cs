using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Karvis.Core;

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
    }
}