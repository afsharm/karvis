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
    public class KMail : Entity
    {
        IDateTimeHelper dateTimeHelper;

        public KMail()
        {
            dateTimeHelper = new DateTimeHelper();
        }

        public virtual string Subject { set; get; }
        public virtual string Description { set; get; }
        public virtual string FromAddress { set; get; }
        public virtual string FromDescription { set; get; }
        public virtual int RelatedReferenceId { set; get; }

        public virtual DateTime? AddDate { set; get; }
        public virtual bool IsSent { set; get; }
    }
}
