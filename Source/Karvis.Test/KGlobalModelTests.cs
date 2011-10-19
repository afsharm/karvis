using System;
using System.Collections.Generic;
using Karvis.Core;
using NUnit.Framework;

namespace Karvis.Test
{

    [TestFixture]
    public class KGlobalModelTest : NHibernateFixture
    {


        [Test]
        public void KGlobalTest()
        {
            IKGlobalModel model = new KGlobalModel(SessionFactory);

            string key = Guid.NewGuid().ToString();
            string value = Guid.NewGuid().ToString();
            string invalidKey = Guid.NewGuid().ToString();

            model.SetValue(key, value);

            var invalid1 = model.GetGlobal(invalidKey);
            Assert.IsNull(invalid1, "111");

            var invalid2 = model.GetValue(invalidKey);
            Assert.IsNull(invalid2, "222");

            var valid1 = model.GetGlobal(key);
            Assert.AreEqual(value, valid1.Value, "333");

            var valid2 = model.GetValue(key);
            Assert.AreEqual(value, valid2, "444");
        }
    }
}

