using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using Fardis;
using NHibernate.Linq;
using System.Net.Mail;
using System.Net;

namespace Karvis.Core
{
    public class KMailModel : IKMailModel
    {
        ISessionFactory _sessionFactory;
        NHibernateRepository<KMail> _mailRepository;
        IScheduleInfoModel _scheduleInfoModel;

        public KMailModel()
        {
            _sessionFactory = NHHelper.Instance.GetSessionFactory();
            _mailRepository = new NHibernateRepository<KMail>(_sessionFactory);
            _scheduleInfoModel = new ScheduleInfoModel();
        }

        public KMailModel(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
            _mailRepository = new NHibernateRepository<KMail>(_sessionFactory);
        }


        public void DoSchedule()
        {
            DateTime start = DateTime.Now;

            //do the real work
            string result = string.Empty;
            //todo

            DateTime end = DateTime.Now;
            _scheduleInfoModel.SaveScheduleInfo(KConstants.MailSchedule, result, start, end);
        }


        public void Qeue(string subject, string description, string fromAddress, string fromDescription, int relatedReferenceId)
        {
            throw new NotImplementedException();
        }

        public List<KMail> GetSentItems()
        {
            throw new NotImplementedException();
        }

        public List<KMail> GetUnSentItems()
        {
            throw new NotImplementedException();
        }

        public void IncreaseTry(KMail mail)
        {
            throw new NotImplementedException();
        }

        public void SaveSuccess(KMail mail)
        {
            throw new NotImplementedException();
        }
    }
}
