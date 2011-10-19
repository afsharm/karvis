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
        void SaveScheduleRun(DateTime start, DateTime end, string scheduleName);
        List<ScheduleInfo> GetSchedulesInfo();
    }
}
