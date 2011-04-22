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

        public IList<Job> FindAll(int maximumRows, int startRowIndex)
        {
            ISession session = NHHelper.Instance.GetSession();

            return session.QueryOver<Job>()
                .Skip(startRowIndex)
                .Take(maximumRows)
                .List<Job>();
        }

        public int FindAllCount()
        {
            ISession session = NHHelper.Instance.GetSession();

            return session.QueryOver<Job>().RowCount();
        }

        public static Job GetJob(string jobId)
        {
            ISession session = NHHelper.Instance.GetSession();

            return session.Get<Job>(Convert.ToInt32(jobId));
        }

        public static void UpdateJob(string title, string description, string URL, string tag, int ID)
        {
            ISession session = NHHelper.Instance.GetSession();

            Job job = session.Load<Job>(ID);

            job.Title = title;
            job.Description = description;
            job.URL = URL;
            job.Tag = tag;

            ITransaction tx = session.BeginTransaction();
            session.SaveOrUpdate(job);
            session.Flush();
            tx.Commit();
        }

        public static void IncreaseVisitCount(string ID)
        {
            ISession session = NHHelper.Instance.GetSession();

            Job job = session.Load<Job>(Convert.ToInt32(ID));

            job.VisitCount++;

            ITransaction tx = session.BeginTransaction();
            session.SaveOrUpdate(job);
            session.Flush();
            tx.Commit();
        }
    }
}
