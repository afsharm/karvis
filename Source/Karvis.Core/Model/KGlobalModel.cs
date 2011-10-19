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
    public class KGlobalModel : IKGlobalModel
    {
        ISessionFactory _sessionFactory;
        NHibernateRepository<KGlobal> _kglobalRepository;

        public KGlobalModel()
        {
            _sessionFactory = NHHelper.Instance.GetSessionFactory();
            _kglobalRepository = new NHibernateRepository<KGlobal>(_sessionFactory);
        }

        public KGlobalModel(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
            _kglobalRepository = new NHibernateRepository<KGlobal>(_sessionFactory);
        }


        public string GetValue(string key)
        {
            var item = GetGlobal(key);
            if (item == null)
                return null;
            else
                return item.Value;
        }

        public KGlobal GetGlobal(string key)
        {
            var results = _kglobalRepository.QueryOver()
                .Where(item => item.Key == key).Select(item => item.Value).List();

            if (results != null && results.Count > 0)
                return results[0];
            else
                return null;
        }


        public void SetValue(string key, string value)
        {
            KGlobal global = GetGlobal(key);
            
            if (global == null)
                global = new KGlobal { Key = key };

            global.Value = value;
            _kglobalRepository.SaveOrUpdate(global);
        }
    }
}
