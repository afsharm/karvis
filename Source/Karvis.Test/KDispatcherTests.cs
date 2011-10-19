using System;
using System.Collections.Generic;
using Karvis.Core;
using NUnit.Framework;
using System.Net.Mail;

namespace Karvis.Test
{

    [TestFixture]
    public class KDispatcherTest : NHibernateFixture
    {
        [Test]
        public void SimpleTest()
        {
            IKDispatcher dispatcher = new KDispatcher();

            var message = new MailMessage("from", "to", "subject", "body");
            dispatcher.Send(message);
        }
    }
}

