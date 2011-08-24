using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;
using NHibernate;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace Karvis.Test
{
    public static class NHConfigurator
    {
        private const string CONN_STR =
       "Data Source=:memory:;Version=3;New=True;";
        private static readonly Configuration _configuration;
        private static readonly ISessionFactory _sessionFactory;
        static NHConfigurator()
        {

            _configuration = new Configuration().Configure()
              .DataBaseIntegration(db =>
              {
                  db.Dialect<SQLiteDialect>();
                  db.Driver<SQLite20Driver>();
                  db.ConnectionProvider<TestConnectionProvider>();
                  db.ConnectionString = CONN_STR;
              })
              .SetProperty(NHibernate.Cfg.Environment.CurrentSessionContextClass,
                "thread_static");

            var props = _configuration.Properties;

            if (props.ContainsKey(NHibernate.Cfg.Environment.ConnectionStringName))
                props.Remove(NHibernate.Cfg.Environment.ConnectionStringName);
            _sessionFactory = _configuration.BuildSessionFactory();
        }
        public static Configuration Configuration
        {
            get
            {
                return _configuration;
            }
        }


        public static ISessionFactory SessionFactory
        {
            get
            {
                return _sessionFactory;
            }
        }

    }
}