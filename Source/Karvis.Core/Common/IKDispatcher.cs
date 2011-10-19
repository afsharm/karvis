using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace Karvis.Core
{
    public interface IKDispatcher
    {
        void Send(MailMessage message);
    }
}
