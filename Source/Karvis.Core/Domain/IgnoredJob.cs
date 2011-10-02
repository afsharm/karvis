using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iesi.Collections.Generic;
using Iesi.Collections;
using System.Collections;

namespace Karvis.Core
{
    public class IgnoredJob : Entity
    {
        public IgnoredJob()
        {
        }

        public virtual string Url { set; get; }
        public virtual AdSource AdSource { set; get; }

    }
}