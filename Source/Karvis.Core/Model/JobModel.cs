using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using Fardis;

namespace Karvis.Core
{
    public class JobModel : IJobModel
    {
        ISessionFactory _sessionFactory;
        NHibernateRepository<Job> _jobRepository;
        const int recentJobsCount = 10;

        public JobModel()
        {
            _sessionFactory = NHHelper.Instance.GetSessionFactory();
            _jobRepository = new NHibernateRepository<Job>(_sessionFactory);
        }

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
            return string.Format("Job/{0}.aspx/{1}", jobID, FConvert.MakeUrlFriendly(jobTitle.ToString()));
        }

        public string GetFeedDescription(Job job)
        {
            return string.Format(
                "<div>{0}<hr/>{1}<hr/>Visit Count: {2} - Feed Count: {3} - Date: {4} - Ad Source: {5} </div>",
                job.Description, job.Tag, job.VisitCount, job.FeedCount, job.DateAddedPersian, job.AdSource);
        }

        public int AddNewJob(string title, string description, string url, string tag, AdSource adSource)
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
                VisitCount = 0,
                AdSource = adSource,
                IsActive = true
            };

            _jobRepository.Add(job);

            return job.Id;
        }

        public IOrderedEnumerable<KeyValuePair<string, ushort>> ExtractTagCloud()
        {
            IList<string> tags = GetAllTags();
            return GeneralHelper.AnalyseTags(tags);
        }

        private IQueryOver<Job, Job> CreateQuery(string title, string tag, AdSource adSource, bool isActive)
        {
            var q = _jobRepository.QueryOver().Where(job => job.IsActive == isActive);

            if (!string.IsNullOrEmpty(title))
                q = q.WhereRestrictionOn(j => j.Title).IsInsensitiveLike(title, MatchMode.Anywhere);

            if (!string.IsNullOrEmpty(tag))
                q = q.WhereRestrictionOn(j => j.Tag).IsInsensitiveLike(tag, MatchMode.Anywhere);

            if (adSource != AdSource.All)
                q = q.Where(job => job.AdSource == adSource);

            return q;
        }

        public IList<Job> FindAll(string title, string tag, AdSource adSource, string sortOrder, int maximumRows, int startRowIndex)
        {
            return FindAll(title, tag, adSource, true, sortOrder, maximumRows, startRowIndex);
        }

        public IList<Job> FindAllNoneActive(string title, string tag, AdSource adSource, string sortOrder, int maximumRows, int startRowIndex)
        {
            return FindAll(title, tag, adSource, false, sortOrder, maximumRows, startRowIndex);
        }

        IList<Job> FindAll(string title, string tag, AdSource adSource, bool isActive,
            string sortOrder, int maximumRows, int startRowIndex)
        {
            IQueryOver<Job, Job> q = CreateQuery(title, tag, adSource, isActive);

            switch (sortOrder)
            {
                case "Id":
                    q = q.OrderBy(j => j.Id).Asc;
                    break;
                case "Id DESC":
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
                case "AdSource":
                    q = q.OrderBy(j => j.AdSource).Asc;
                    break;
                case "AdSource DESC":
                    q = q.OrderBy(j => j.AdSource).Desc;
                    break;
                case "DateAdded":
                    q = q.OrderBy(j => j.DateAdded).Asc;
                    break;
                case "DateAdded DESC":
                default:
                    q = q.OrderBy(j => j.DateAdded).Desc.OrderBy(job => job.Id).Desc;
                    break;
            }

            IList<Job> retval = q
                .Skip(startRowIndex)
                .Take(maximumRows)
                .List<Job>();

            return retval;
        }

        public int FindAllCount(string title, string tag, AdSource adSource)
        {
            return FindAllCount(title, tag, adSource, true);
        }

        public int FindAllCountNoneActive(string title, string tag, AdSource adSource)
        {
            return FindAllCount(title, tag, adSource, false);
        }

        public void DeleteJob(int Id)
        {
            _jobRepository.Remove(_jobRepository.Load(Id));
        }

        private int FindAllCount(string title, string tag, AdSource adSource, bool isActive)
        {
            return CreateQuery(title, tag, adSource, isActive).RowCount();
        }

        public void UpdateJob(string title, string description, string url, string tag, int id, AdSource adSource)
        {
            Job job = _jobRepository.Load(id);

            job.Title = title;
            job.Description = description;
            job.Url = url;
            job.Tag = tag;
            job.AdSource = adSource;

            _jobRepository.SaveOrUpdate(job);
        }

        void IncreaseVisitCount(int id)
        {
            Job job = _jobRepository.Load(id);

            job.VisitCount++;

            _jobRepository.SaveOrUpdate(job);
        }

        public IEnumerable<Job> GetAllJobs()
        {
            return GetAllJobs(false);
        }

        public IEnumerable<Job> GetAllJobs(bool updateStat)
        {
            var q = _jobRepository.QueryOver().Where(job => job.IsActive).OrderBy(j => j.DateAdded).Desc;

            if (updateStat)
            {
                var recentJobs = q.Skip(0).Take(recentJobsCount).List<Job>();

                foreach (var job in recentJobs)
                {
                    job.FeedCount++;

                    _jobRepository.SaveOrUpdate(job);
                }
            }

            var jobs = q.Skip(0).Take(int.MaxValue).List<Job>();
            return jobs;
        }

        public IList<string> GetAllTags()
        {
            var q = _jobRepository.QueryOver().Where(job => job.IsActive).Select(j => j.Tag);
            return q.List<String>();
        }

        internal IEnumerable<Job> GetJobsByTag(string tag, bool updateStat)
        {
            var jobs = FindAll(null, tag, AdSource.All, null, int.MaxValue, 0);

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

        public Job GetJob(int jobId)
        {
            return GetJob(jobId, false);
        }

        public Job GetJob(int jobId, bool updateStat)
        {
            if (updateStat)
                IncreaseVisitCount(jobId);

            return _jobRepository.Get(jobId);
        }

        public string GetJobUrl(int id, string title)
        {
            return string.Format("{0}{1}", GeneralHelper.GetAppUrl(), GetJobUrlPure(id, title));
        }


        public int SaveOrUpdateJobBatch(List<Job> jobs, AdSource adSource, bool isActive, bool isNew)
        {
            //I wan old id should saved sooner in database
            for (int i = 0; i < jobs.Count; i++)
            {
                Job job = jobs[i];
                Job selectedJob = isNew ? job : _jobRepository.Get(job.PreSavedJobId);

                if (!isNew)
                {
                    selectedJob.Description = job.Description;
                    selectedJob.Emails = job.Emails;
                    selectedJob.Tag = job.Tag;
                    selectedJob.Title = job.Title;
                    selectedJob.Url = job.Url;
                }

                AddComplementaryInfo(selectedJob);
                selectedJob.AdSource = adSource;
                selectedJob.IsActive = isActive;

                _jobRepository.SaveOrUpdate(selectedJob);
            }

            return jobs.Count;
        }

        private void AddComplementaryInfo(Job job)
        {
            job.DateAdded = DateTime.UtcNow;
            job.VisitCount = 0;
            job.FeedCount = 0;
        }

        public IList<Job> FindAllNoneActive(AdSource adSource)
        {
            return FindAll(title: null, tag: null, adSource: adSource, isActive: false,
            sortOrder: "Id", maximumRows: int.MaxValue, startRowIndex: 0);
        }

        public void SaveOrUpdateJob(Job job)
        {
            _jobRepository.SaveOrUpdate(job);
        }

        public void AddJob(Job job)
        {
            AddComplementaryInfo(job);
            _jobRepository.Add(job);
        }
    }
}
