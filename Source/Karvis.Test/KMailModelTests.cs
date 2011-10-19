using System;
using System.Collections.Generic;
using Karvis.Core;
using NUnit.Framework;

namespace Karvis.Test
{

    [TestFixture]
    public class KMailModelTest : NHibernateFixture
    {


        [Test]
        public void SimpleTest()
        {
            IKMailModel model = new KMailModel(SessionFactory);

            string subject = Guid.NewGuid().ToString();
            string description = Guid.NewGuid().ToString();
            string fromAddress = Guid.NewGuid().ToString();
            string formDescription = Guid.NewGuid().ToString();
            int relatedReferenceId = new Random(DateTime.Now.Millisecond).Next(1, 10000);

            model.Qeue(subject, description, fromAddress, formDescription, relatedReferenceId);

            var sent = model.GetSentItems();
            var unsent = model.GetUnSentItems();

            Assert.AreEqual(0, sent.Count);
            Assert.AreEqual(1, unsent.Count);

        }
    }
}

