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
        public static ISession GetSession()
        {
            ISessionFactory sessionFactory = new Configuration().Configure().BuildSessionFactory();

            return sessionFactory.OpenSession();
        }

        public static void SchemaUpdate()
        {
            SchemaUpdate schemaUpdate = new SchemaUpdate(new Configuration().Configure());
            schemaUpdate.Execute(false, true);
        }

        public static void SchemaExport()
        {
            SchemaExport schemaExport = new SchemaExport(new Configuration().Configure());
            schemaExport.Execute(false, true, false);
        }
    }
}
