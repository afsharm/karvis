using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Context;

namespace SJ.Core
{

    public class NHHelper
    {
        private static NHHelper instance;

        ISessionFactory _sessionFactory;

        private NHHelper()
        {
            var nhConfig = new Configuration().Configure();
            _sessionFactory = nhConfig.BuildSessionFactory();
        }

        public static NHHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NHHelper();
                }
                return instance;
            }
        }

        public ISession GetCurrentSession()
        {
            return _sessionFactory.GetCurrentSession();
        }

        public void BeginRequest()
        {
            var session = _sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);
        }

        public void EndRequest()
        {
            var session = CurrentSessionContext.Unbind(_sessionFactory);
            session.Dispose();
        }
    }
}
