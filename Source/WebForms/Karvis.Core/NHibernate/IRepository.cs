using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Karvis.Core
{
    public interface IRepository<T> : IEnumerable<T>
    where T : Entity
    {
        void Add(T item);
        bool Contains(T item);
        int Count { get; }
        bool Remove(T item);
        T Get(object Id);
        T Load(object Id);
        void SaveOrUpdate(T item);
        IQueryOver<T, T> QueryOver();
    }
}