using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace Karvis.Core
{
    public interface IKMailModel
    {
        void DoSchedule();
        void Qeue(string subject, string description, string fromAddress, string fromDescription, string receivers, int relatedReferenceId);
        IList<KMail> GetSentItems();
        IList<KMail> GetUnSentItems();
        void IncreaseTry(KMail mail);
        void SaveSuccess(KMail mail);
    }
}
