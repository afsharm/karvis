using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iesi.Collections.Generic;
using Iesi.Collections;
using System.Collections;
using Fardis;

namespace Karvis.Core
{
    public class ScheduleInfo : Entity
    {
        public ScheduleInfo()
        {
        }

        public virtual string Name { set; get; }
        public virtual string Result { set; get; }
        public virtual DateTime? StartDate { set; get; }
        public virtual DateTime? EndDate { set; get; }
    }
}
