using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace SJ.Core
{
    public class JobModel
    {
        ISessionFactory _sessionFactory;
        NHibernateRepository<Job> _jobRepository;

        public JobModel(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
            _jobRepository = new NHibernateRepository<Job>(_sessionFactory);
        }

        public string GetJobUrl(Job job)
        {
            return string.Format("{0}{1}", GeneralHelper.GetAppUrl(), GetJobUrlPure(job.Id, job.Title));
        }

        public string GetJobUrl(object jobID, object jobTitle)
        {
            return string.Format("~/{0}", GetJobUrlPure(jobID, jobTitle));
        }

        public string GetJobUrlPure(object jobID, object jobTitle)
        {
            return string.Format("Job/{0}.aspx/{1}", jobID, jobTitle);
        }

        public string GetFeedDescription(Job job)
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
                Url = url,
                Tag = tag,
                DateAdded = DateTime.UtcNow,
                VisitCount = 0
            };

            _jobRepository.Add(job);

            return job.Id;
        }

        public IOrderedEnumerable<KeyValuePair<string, ushort>> ExtractTagCloud()
        {
            IList<string> tags = GetAllTags();
            return GeneralHelper.AnalyseTags(tags);
        }

        private IQueryOver<Job, Job> CreateQuery(string title, string tag)
        {
            var q = _jobRepository.QueryOver();

            if (!string.IsNullOrEmpty(title))
                q = q.WhereRestrictionOn(j => j.Title).IsInsensitiveLike(title, MatchMode.Anywhere);

            if (!string.IsNullOrEmpty(tag))
                q = q.WhereRestrictionOn(j => j.Tag).IsInsensitiveLike(tag, MatchMode.Anywhere);

            return q;
        }

        public IList<Job> FindAll(string title, string tag, string sortOrder, int maximumRows, int startRowIndex)
        {
            IQueryOver<Job, Job> q = CreateQuery(title, tag);

            switch (sortOrder)
            {
                case "ID":
                    q = q.OrderBy(j => j.Id).Asc;
                    break;
                case "ID DESC":
                    q = q.OrderBy(j => j.Id).Desc;
                    break;
                case "VisitCount":
                    q = q.OrderBy(j => j.VisitCount).Asc;
                    break;
                case "VisitCount DESC":
                    q = q.OrderBy(j => j.VisitCount).Desc;
                    break;
                case "Title":
                    q = q.OrderBy(j => j.Title).Asc;
                    break;
                case "Title DESC":
                    q = q.OrderBy(j => j.Title).Desc;
                    break;
                case "Tag":
                    q = q.OrderBy(j => j.Tag).Asc;
                    break;
                case "Tag DESC":
                    q = q.OrderBy(j => j.Tag).Desc;
                    break;
                case "DateAdded":
                    q = q.OrderBy(j => j.DateAdded).Asc;
                    break;
                case "DateAdded DESC":
                default:
                    q = q.OrderBy(j => j.DateAdded).Desc;
                    break;
            }

            IList<Job> retval = q
                .Skip(startRowIndex)
                .Take(maximumRows)
                .List<Job>();

            return retval;
        }

        public int FindAllCount(string title, string tag)
        {
            return CreateQuery(title, tag).RowCount();
        }

        public void UpdateJob(string title, string description, string url, string tag, int id)
        {
            Job job = _jobRepository.Load(id);

            job.Title = title;
            job.Description = description;
            job.Url = url;
            job.Tag = tag;

            _jobRepository.SaveOrUpdate(job);
        }

        public void IncreaseVisitCount(string id)
        {
            Job job = _jobRepository.Load(Convert.ToInt32(id));

            job.VisitCount++;

            _jobRepository.SaveOrUpdate(job);
        }

        public IEnumerable<Job> GetAllJobs()
        {
            return GetAllJobs(false);
        }

        public IEnumerable<Job> GetAllJobs(bool updateStat)
        {
            var q = _jobRepository.QueryOver().OrderBy(j => j.DateAdded).Asc;

            var jobs = q.List<Job>();

            if (updateStat)
            {
                foreach (var job in jobs)
                {
                    job.FeedCount++;

                    _jobRepository.SaveOrUpdate(job);
                }
            }

            return jobs;
        }

        public IList<string> GetAllTags()
        {
            var q = _jobRepository.QueryOver().Select(j => j.Tag);
            return q.List<String>();
        }

        internal IEnumerable<Job> GetJobsByTag(string tag, bool updateStat)
        {
            var jobs = FindAll(null, tag, null, int.MaxValue, 0);

            if (updateStat)
            {
                foreach (var job in jobs)
                {
                    job.FeedCount++;
                    _jobRepository.SaveOrUpdate(job);
                }
            }

            return jobs;
        }
    }
}
