using System;
using System.Collections.Generic;
using Karvis.Core;
using NUnit.Framework;

namespace Karvis.Test
{

    [TestFixture]
    public class KMailModelTest : NHibernateFixture
    {
        IKMailModel model;

        [SetUp]
        public void SetUpTest()
        {
            model = new KMailModel(SessionFactory);
        }

        [Test]
        public void SimpleTest()
        {
            string subject = Guid.NewGuid().ToString();
            string description = Guid.NewGuid().ToString();
            string fromAddress = Guid.NewGuid().ToString();
            string fromDescription = Guid.NewGuid().ToString();
            int relatedReferenceId = new Random(DateTime.Now.Millisecond).Next(1, 10000);

            model.Qeue(subject, description, fromAddress, fromDescription, relatedReferenceId);

            var sent = model.GetSentItems();
            var unsent = model.GetUnSentItems();

            Assert.AreEqual(0, sent.Count, "111");
            Assert.AreEqual(1, unsent.Count, "222");

            var ret = unsent[0];

            Assert.AreEqual(0, ret.TryCounter, "333");
            Assert.AreEqual(false, ret.IsSent, "444");
            Assert.AreEqual(description, ret.Description, "555");
            Assert.IsNotNull(ret.AddDate, "666");
            Assert.Greater((DateTime.Now - ret.AddDate.Value).TotalMilliseconds, 0, "777");
            Assert.AreEqual(fromAddress, ret.FromAddress, "888");
            Assert.AreEqual(fromDescription, ret.FromDescription, "999");
            Assert.AreEqual(relatedReferenceId, ret.RelatedReferenceId, "000");
            Assert.AreEqual(subject, ret.Subject, "aaa");



        }

        [Test]
        public void AdvancedTest()
        {
            model.Qeue(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), 111);

            var unsent = model.GetUnSentItems();
            var item = unsent[0];

            model.IncreaseTry(item);
            model.IncreaseTry(item);
            model.IncreaseTry(item);

            var unsent2 = model.GetUnSentItems();
            var item2 = unsent2[0];

            Assert.AreEqual(3, item2.TryCounter);

            model.SaveSuccess(item2);
            var sent2 = model.GetSentItems();
            Assert.AreEqual(1, sent2.Count);
            
            var unsent3 = model.GetUnSentItems();
            Assert.AreEqual(0, unsent3.Count);
        }
    }
}

