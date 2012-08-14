using System;
using SharpLite.Domain;

namespace Karvis.Core
{
    public class ScheduleInfo : Entity
    {
        public virtual string Name { set; get; }
        public virtual string Result { set; get; }
        public virtual DateTime? StartDate { set; get; }
        public virtual DateTime? EndDate { set; get; }
    }
}