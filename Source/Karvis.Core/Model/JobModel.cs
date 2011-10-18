using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using Fardis;
using NHibernate.Linq;
using System.Net.Mail;
using System.Net;

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
                "<div style='direction:rtl; text-align:right'>Date: {0} - Ad Source: {1} </div>",
                job.DateAddedPersian, job.AdSourceDescription);
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

        private IQueryOver<Job, Job> CreateQuery(string title, string tag, AdSource adSource, bool? isActive)
        {
            var q = _jobRepository.QueryOver();

            if (isActive != null)
                q = q.Where(job => job.IsActive == isActive);

            if (!string.IsNullOrEmpty(title))
                q = q.WhereRestrictionOn(j => j.Title).IsInsensitiveLike(title, MatchMode.Anywhere);

            if (!string.IsNullOrEmpty(tag))
                q = q.WhereRestrictionOn(j => j.Tag).IsInsensitiveLike(tag, MatchMode.Anywhere);

            if (adSource != AdSource.All)
                q = q.Where(job => job.AdSource == adSource);

            return q;
        }

        public IList<Job> FindAll(string title, string tag, AdSource adSource, bool? isActive,
            string sortOrder, int maximumRows, int startRowIndex)
        {
            IQueryOver<Job, Job> q = CreateQuery(title, tag, adSource, isActive);

            //avoiding null reference
            if (sortOrder == null)
                sortOrder = string.Empty;

            switch (sortOrder.ToLower())
            {
                case "id":
                    q = q.OrderBy(j => j.Id).Asc;
                    break;
                case "id desc":
                    q = q.OrderBy(j => j.Id).Desc;
                    break;
                case "visitcount":
                    q = q.OrderBy(j => j.VisitCount).Asc;
                    break;
                case "visitcount desc":
                    q = q.OrderBy(j => j.VisitCount).Desc;
                    break;
                case "title":
                    q = q.OrderBy(j => j.Title).Asc;
                    break;
                case "title desc":
                    q = q.OrderBy(j => j.Title).Desc;
                    break;
                case "tag":
                    q = q.OrderBy(j => j.Tag).Asc;
                    break;
                case "tag desc":
                    q = q.OrderBy(j => j.Tag).Desc;
                    break;
                case "adsource":
                    q = q.OrderBy(j => j.AdSource).Asc;
                    break;
                case "adsource desc":
                    q = q.OrderBy(j => j.AdSource).Desc;
                    break;
                case "dateadded":
                    q = q.OrderBy(j => j.DateAdded).Asc;
                    break;
                case "dateadded desc":
                    q = q.OrderBy(j => j.DateAdded).Desc;
                    break;
                case "isactive":
                    q = q.OrderBy(j => j.IsActive).Asc;
                    break;
                case "isactive desc":
                    q = q.OrderBy(j => j.IsActive).Desc;
                    break;
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

        public void DeleteJob(int Id)
        {
            _jobRepository.Remove(_jobRepository.Load(Id));
        }

        public int FindAllCount(string title, string tag, AdSource adSource, bool? isActive)
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
            SendMail(job);
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
            var jobs = FindAll(null, tag, AdSource.All, true, null, int.MaxValue, 0);

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

        public string GetJobUrlModify(int id)
        {
            return string.Format("{0}Job/SubmitJob.aspx?id={1}", GeneralHelper.GetAppUrl(), id);
        }

        public string GetJobUrl(int id, string title)
        {
            return string.Format("{0}{1}", GeneralHelper.GetAppUrl(), GetJobUrlPure(id, title));
        }


        public int SaveOrUpdateJobBatch(List<Job> jobs, bool isActive, ExtractStatus extractStatus)
        {
            //I wan old id should saved sooner in database
            for (int i = 0; i < jobs.Count; i++)
            {
                Job job = jobs[i];
                Job selectedJob = extractStatus == ExtractStatus.New ? job : _jobRepository.Get(job.PreSavedJobId);

                if (extractStatus != ExtractStatus.New)
                {
                    selectedJob.Description = job.Description;
                    selectedJob.Emails = job.Emails;
                    selectedJob.Tag = job.Tag;
                    selectedJob.Title = job.Title;
                    selectedJob.Url = job.Url;
                }

                AddComplementaryInfo(selectedJob);
                selectedJob.IsActive = isActive;

                _jobRepository.SaveOrUpdate(selectedJob);
                SendMail(selectedJob);
            }

            return jobs.Count;
        }

        private void AddComplementaryInfo(Job job)
        {
            job.DateAdded = DateTime.UtcNow;
            job.VisitCount = 0;
            job.FeedCount = 0;
        }

        public void SaveOrUpdateJob(Job job)
        {
            _jobRepository.SaveOrUpdate(job);
            SendMail(job);
        }

        private void SendMail(Job job)
        {
            //dont send email for none actis
            if (!job.IsActive)
                return;

            string[] emails = job.Emails.Split(',');

            //send for valid emails
            if (emails.Length < 1)
                return;

            SmtpClient client = new SmtpClient("mail.karvis.ir", 25);
            client.Credentials = new NetworkCredential("info@karvis.ir", "ra5c2y&f");

            foreach (var email in emails)
            {
                if (string.IsNullOrEmpty(email))
                    return;

                string subject = "آگهی استخدام شما در «کارویس» به ثبت رسید";
                
                string body = string.Format(
                    "عنوان: {0}\n\rشرح: {1}\n\rتگ: {2}\n\rتاریخ: {3}\n\rلینک: {4}\n\rکارویس http://karvis.ir سایت کاریابی مخصوص برنامه‌نویسان"
                , job.Title, job.Description, job.Tag, job.DateAddedPersian, job.Url);

                MailMessage message = new MailMessage(new MailAddress("no-reply@karvis.ir", "کارویس"), new MailAddress(email, email));
                message.To.Add("afshar.mohebbi@gmail.com");
                message.Body = body;
                message.Subject = subject;

                try
                {
                    client.Send(message);
                }
                catch { }
            }
        }

        public void AddJob(Job job)
        {
            AddComplementaryInfo(job);
            _jobRepository.Add(job);
        }

        public int FindNoneActiveCount(AdSource adSource)
        {
            return FindAllCount(string.Empty, string.Empty, adSource, false);
        }

        public string GetLastJobUrl(AdSource siteSource)
        {
            var q = _jobRepository.QueryOver().Where(j => j.AdSource == siteSource && j.IsActive).OrderBy(j => j.DateAdded).Desc;

            var res = q.Take(1).List();

            if (res != null && res.Count > 0)
                return res[0].Url;
            else
                return null;
        }

        public bool ExistsJobUrl(string jobUrl)
        {
            var q = _jobRepository.QueryOver().Where(j => j.Url.ToLower().Trim() == jobUrl.ToLower().Trim());

            return q.RowCount() > 0;
        }

        public IList<string> GetJobUrlsByAdSource(AdSource siteSource)
        {
            var q = _jobRepository.QueryOver().Where(j => j.AdSource == siteSource).Select(j => j.Url);
            return q.List<string>();
        }

        public IList<AdSourceStatDto> ExtractAdSourceStat()
        {
            IList<AdSourceStatDto> result = new List<AdSourceStatDto>();
            int totalCount = GetTotalJobCount();

            foreach (var item in Enum.GetValues(typeof(AdSource)))
            {
                AdSource siteSource = (AdSource)item;

                if (siteSource == AdSource.All)
                    continue;

                int count = AdSourceCount(siteSource);
                if (count < 1)
                    continue;

                int percent = (count * 100) / totalCount;
                string siteSourceDescription = Job.GetAdSourceDescription(siteSource);

                result.Add(new AdSourceStatDto
                {
                    SiteSource = (AdSource)item,
                    SiteSourceDescription = siteSourceDescription,
                    Count = count,
                    Percent = percent
                });
            }

            var sorted = result.OrderByDescending(dto => dto.Count);
            return sorted.ToList();
        }

        private int AdSourceCount(AdSource siteSource)
        {
            return _jobRepository.QueryOver().Where(j => j.IsActive && j.AdSource == siteSource).RowCount();
        }

        public int GetTotalJobCount()
        {
            return _jobRepository.QueryOver().Where(j => j.IsActive).RowCount();
        }
    }
}
