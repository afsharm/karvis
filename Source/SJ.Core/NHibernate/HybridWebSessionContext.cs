using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Context;
using NHibernate;
using NHibernate.Engine;

namespace SJ.Core
{
    public class HybridWebSessionContext : CurrentSessionContext
    {
        private const string _itemsKey = "HybridWebSessionContext";
        [ThreadStatic]
        private static ISession _threadSession;

        // This constructor should be kept, otherwise NHibernate will fail to create an instance of this class.
        public HybridWebSessionContext(ISessionFactoryImplementor factory)
        {
        }

        protected override ISession Session
        {
            get
            {
                var currentContext = ReflectiveHttpContext.HttpContextCurrentGetter();
                if (currentContext != null)
                {
                    var items = ReflectiveHttpContext.HttpContextItemsGetter(currentContext);
                    var session = items[_itemsKey] as ISession;
                    if (session != null)
                    {
                        return session;
                    }
                }

                if (_threadSession != null)
                    return _threadSession;

                return NHHelper.Instance.GetMySession();
            }
            set
            {
                var currentContext = ReflectiveHttpContext.HttpContextCurrentGetter();
                if (currentContext != null)
                {
                    var items = ReflectiveHttpContext.HttpContextItemsGetter(currentContext);
                    items[_itemsKey] = value;
                    //return;
                }

                _threadSession = value;

                NHHelper.Instance.SetMySession(value);
            }
        }
    }
}
