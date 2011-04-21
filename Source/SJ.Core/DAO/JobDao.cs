using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace SJ.Core
{
    public class JobDao
    {
        public static int AddNewJob(string title, string description, string url, string tag)
        {
            url = url.ToLower();

            if (!url.StartsWith("http://"))
                url = "http://" + url;

            Job job = new Job()
            {
                Title = title,
                Description = description,
                URL = url,
                Tag = tag,
                DateAdded = DateTime.UtcNow,
                VisitCount = 0
            };

            ISession session = NHHelper.Instance.GetSession();

            ITransaction tx = session.BeginTransaction();
            session.SaveOrUpdate(job);
            session.Flush();
            tx.Commit();

            return job.ID;
        }

        public IList<Job> FindAll()
        {
            ISession session = NHHelper.Instance.GetSession();

            return session.QueryOver<Job>().List<Job>();
        }

        public static Job GetJob(string jobId)
        {
            ISession session = NHHelper.Instance.GetSession();

            return session.Get<Job>(Convert.ToInt32(jobId));
        }
    }
}
