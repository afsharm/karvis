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
        public virtual int FeedCount { set; get; }
        public virtual string Tag { set; get; }
        public virtual DateTime? DateAdded { set; get; }
        public virtual string URL { set; get; }

        public virtual string DateAddedPersian
        {
            get
            {
                return GeneralHelper.ConvertToPersianDate(DateAdded);
            }
        }

        public virtual ISet Comments { set; get; }

        public virtual void AddComment(Comment comment)
        {
            comment.ParentJob = this;
            Comments.Add(comment);
        }

        public virtual string GetJobUrl()
        {
            return string.Format("{0}{1}", GeneralHelper.GetAppUrl(), GetJobUrlPure(this.ID, this.Title));
        }

        public static string GetJobUrl(object jobID, object jobTitle)
        {
            return string.Format("~/{0}", GetJobUrlPure(jobID, jobTitle));
        }

        public static string GetJobUrlPure(object jobID, object jobTitle)
        {
            return string.Format("Job/{0}.aspx/{1}", jobID, jobTitle);
        }

        public virtual string FeedDescription
        {
            get
            {
                return string.Format(
                    "<div>{0}<hr/>{1}<hr/>Visit Count: {2} - Feed Count: {3} - Date: {4}</div>",
                    Description, Tag, VisitCount, FeedCount, DateAddedPersian);
            }
        }
    }
}
