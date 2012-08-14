using System;
using System.Collections.Generic;
using Karvis.Core;
using NUnit.Framework;

namespace Karvis.Test
{

    [TestFixture]
    public class ScheduleInfoModelTest : NHibernateFixture
    {


        [Test]
        public void ScheduleInfoTest()
        {
            IScheduleInfoModel model = new ScheduleInfoModel(SessionFactory);

            DateTime start = DateTime.Now.AddHours(new Random(DateTime.Now.Millisecond).Next(0, 100000));
            DateTime end = DateTime.Now.AddHours(new Random(DateTime.Now.Millisecond).Next(0, 100000));
            string scheduleName = Guid.NewGuid().ToString();
            string result = Guid.NewGuid().ToString();

            model.SaveScheduleInfo(scheduleName, result, start, end);

            var rets = model.LoadScheduleInfo();

            Assert.AreEqual(1, rets.Count);
            var retrieved = rets[0];

            Assert.AreEqual(start, retrieved.StartDate);
            Assert.AreEqual(end, retrieved.EndDate);
            Assert.AreEqual(scheduleName, retrieved.Name);
            Assert.AreEqual(result, retrieved.Result);
        }
    }
}

