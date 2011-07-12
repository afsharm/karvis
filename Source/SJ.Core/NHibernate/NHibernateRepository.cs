using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using NHibernate;
using NHibernate.Linq;

namespace SJ.Core
{
    public class NHibernateRepository<T> :
      NHibernateBase,
      IRepository<T> where T : Entity
    {
        public NHibernateRepository(ISessionFactory sessionFactory)
            : base(sessionFactory)
        {
        }

        public void Add(T item)
        {
            Transact(() => session.Save(item));
        }

        public bool Contains(T item)
        {
            if (item.Id == default(int))
                return false;
            return Transact(() => session.Get<T>(item.Id)) != null;
        }

        public int Count
        {
            get
            {
                return Transact(() => session.Query<T>().Count());
            }
        }

        public bool Remove(T item)
        {
            Transact(() => session.Delete(item));
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Transact(() => session.Query<T>()
                           .Take(1000).GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Transact(() => GetEnumerator());
        }

        public IQueryOver<T, T> QueryOver()
        {
            return Transact(() => session.QueryOver<T>());
        }

        public T Get(object Id)
        {
            return Transact(() => session.Get<T>(Id));
        }

        public T Load(object Id)
        {
            return Transact(() => session.Load<T>(Id));
        }

        public void SaveOrUpdate(T item)
        {
            Transact(() => session.SaveOrUpdate(item));
        }
    }
}
