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

        public void SchemaUpdate()
        {
            SchemaUpdate schemaUpdate = new SchemaUpdate(new Configuration().Configure());
            schemaUpdate.Execute(false, true);
        }

        public void SchemaExport()
        {
            SchemaExport schemaExport = new SchemaExport(new Configuration().Configure());
            schemaExport.Execute(false, true, false);
        }
    }
}
