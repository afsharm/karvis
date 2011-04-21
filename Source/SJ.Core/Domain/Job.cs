using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iesi.Collections.Generic;
using Iesi.Collections;
using System.Collections;

namespace SJ.Core
{
    public class Job
    {
        public Job()
        {
            Comments = new HashedSet<Comment>();
        }

        public virtual int ID { set; get; }
        public virtual string Title { set; get; }
        public virtual string Description { set; get; }
        public virtual int VisitCount { set; get; }
        public virtual string Tag { set; get; }
        public virtual DateTime? DateAdded { set; get; }
        public virtual string URL { set; get; }

        public virtual ISet Comments { set; get; }

        public virtual void AddComment(Comment comment)
        {
            comment.ParentJob = this;
            Comments.Add(comment);
        }
    }
}
