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
    public class ScheduleInfoModel : IScheduleInfoModel
    {
        ISessionFactory _sessionFactory;
        NHibernateRepository<ScheduleInfo> _scheduleInfoRepository;

        public ScheduleInfoModel()
        {
            _sessionFactory = NHHelper.Instance.GetSessionFactory();
            _scheduleInfoRepository = new NHibernateRepository<ScheduleInfo>(_sessionFactory);
        }

        public ScheduleInfoModel(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
            _scheduleInfoRepository = new NHibernateRepository<ScheduleInfo>(_sessionFactory);
        }

        public void SaveScheduleRun(DateTime start, DateTime end, string p)
        {
            throw new NotImplementedException();
        }


        public List<ScheduleInfo> GetSchedulesInfo()
        {
            throw new NotImplementedException();
        }
    }
}
