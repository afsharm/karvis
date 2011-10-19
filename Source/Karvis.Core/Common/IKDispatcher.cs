using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace Karvis.Core
{
    public interface IKDispatcher
    {
        bool Send(MailMessage message);
    }
}
