using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Cfg;

namespace Karvis.Core
{
    public class ConfigHelper
    {
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
