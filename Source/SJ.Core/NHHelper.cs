using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace SJ.Core
{

    public class NHHelper
    {
        private static NHHelper instance;

        ISessionFactory sessionFactory;

        private NHHelper()
        {
            sessionFactory = new Configuration().Configure().BuildSessionFactory();
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

        public ISession GetSession()
        {
            return sessionFactory.OpenSession();
        }
    }
}
