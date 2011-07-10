using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace SJ.Core
{
    public class JobModel
    {
        ISessionFactory _sessionFactory;

        public JobModel(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public virtual string GetJobUrl(Job job)
        {
            return string.Format("{0}{1}", GeneralHelper.GetAppUrl(), GetJobUrlPure(job.Id, job.Title));
        }

        public static string GetJobUrl(object jobID, object jobTitle)
        {
            return string.Format("~/{0}", GetJobUrlPure(jobID, jobTitle));
        }

        public static string GetJobUrlPure(object jobID, object jobTitle)
        {
            return string.Format("Job/{0}.aspx/{1}", jobID, jobTitle);
        }

        public virtual string GetFeedDescription(Job job)
        {
            return string.Format(
                "<div>{0}<hr/>{1}<hr/>Visit Count: {2} - Feed Count: {3} - Date: {4}</div>",
                job.Description, job.Tag, job.VisitCount, job.FeedCount, job.DateAddedPersian);
        }

        public int AddNewJob(string title, string description, string url, string tag)
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

            NHibernateRepository<Job> jobRepository = new NHibernateRepository<Job>(_sessionFactory);

            ISession session = NHHelper.Instance.GetCurrentSession();

            using (ITransaction tx = session.BeginTransaction())
            {
                session.SaveOrUpdate(job);
                session.Flush();
                tx.Commit();
            }
            return job.Id;
        }

        public static IOrderedEnumerable<KeyValuePair<string, ushort>> ExtractTagCloud()
        {
            IList<string> tags = JobDao.GetAllTags();
            return GeneralHelper.AnalyseTags(tags);
        }
    }
}
