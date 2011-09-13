using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Karvis.Core;
using Fardis;

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
            IDateTimeHelper dateTimeHelper = new DateTimeHelper();
            var result = dateTimeHelper.ConvertToPersianDatePersianDigit(greDate);

            Assert.AreEqual(correctDate, result, "date problem");
        }
    }
}
