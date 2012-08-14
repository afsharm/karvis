using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace Karvis.Core
{
    public interface IKGlobalModel
    {
        string GetValue(string key);
        KGlobal GetGlobal(string key);

        void SetValue(string key, string value);
    }
}
