using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace Karvis.Core
{
    public interface IJobModel
    {
        IOrderedEnumerable<KeyValuePair<string, ushort>> ExtractTagCloud();
        IList<string> GetAllTags();
        IList<Job> FindAll(string title, string tag, AdSource adSource, bool? isActive, string sortOrder, int maximumRows, int startRowIndex);
        int FindAllCount(string title, string tag, AdSource adSource, bool? isActive);
        IEnumerable<Job> GetAllJobs();
        IEnumerable<Job> GetAllJobs(bool updateStat);
        Job GetJob(int jobId, bool updateStat);
        Job GetJob(int jobId);

        int AddNewJob(string title, string description, string url, string tag, AdSource adSource);
        void UpdateJob(string title, string description, string url, string tag, int id, AdSource adSource);
        int SaveOrUpdateJobBatch(List<Job> jobs, bool isActive, bool isNew);
        void SaveOrUpdateJob(Job job);
        void AddJob(Job job);
        void DeleteJob(int Id);

        string GetJobUrl(Job job);
        string GetJobUrl(object jobID, object jobTitle);
        string GetJobUrlPure(object jobID, object jobTitle);
        string GetJobUrl(int id, string title);
        string GetJobUrlModify(int id);
        string GetFeedDescription(Job job);

        int FindNoneActiveCount(AdSource adSource);
    }
}
