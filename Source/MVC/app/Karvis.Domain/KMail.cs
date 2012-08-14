using System;
using SharpLite.Domain;

namespace Karvis.Core
{
    public class KMail : Entity
    {
        public virtual string Subject { set; get; }
        public virtual string Description { set; get; }
        public virtual string FromAddress { set; get; }
        public virtual string FromDescription { set; get; }

        /// <summary>
        /// comma sperated
        /// </summary>
        public virtual string Receivers { set; get; }

        public virtual int RelatedReferenceId { set; get; }
        public virtual DateTime? AddDate { set; get; }
        public virtual bool IsSent { set; get; }
        public virtual int TryCounter { set; get; }
    }
}