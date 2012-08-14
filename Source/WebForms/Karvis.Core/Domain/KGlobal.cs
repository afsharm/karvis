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
    public class KGlobal : Entity
    {
        public KGlobal()
        {
        }

        public virtual string Key { set; get; }
        public virtual string Value { set; get; }
    }
}
