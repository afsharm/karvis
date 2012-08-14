using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace Karvis.Core
{
    public interface IScheduleInfoModel
    {
        void SaveScheduleInfo(string name, string result, DateTime start, DateTime end);
        IList<ScheduleInfo> LoadScheduleInfo();
    }
}
