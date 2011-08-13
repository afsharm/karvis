using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Karvis.Core;

namespace Karvis.Test
{
    [TestFixture]
    public class GeneralTester
    {
        [Test]
        public void ConvertToPersianDateTest()
        {
            var greDate = new DateTime(2011, 8, 7);
            var correctDate = "۱۳۹۰/۵/۱۶";
            var result = GeneralHelper.ConvertToPersianDate(greDate);

            Assert.AreEqual(correctDate, result, "date problem");
        }
    }
}
