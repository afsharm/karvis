using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace SJ.Core
{
    public interface IJobModel
    {
        string GetJobUrl(Job job);

        string GetJobUrl(object jobID, object jobTitle);

        string GetJobUrlPure(object jobID, object jobTitle);

        string GetFeedDescription(Job job);

        int AddNewJob(string title, string description, string url, string tag);

        IOrderedEnumerable<KeyValuePair<string, ushort>> ExtractTagCloud();

        IList<Job> FindAll(string title, string tag, string sortOrder, int maximumRows, int startRowIndex);

        int FindAllCount(string title, string tag);

        void UpdateJob(string title, string description, string url, string tag, int id);

        IEnumerable<Job> GetAllJobs();

        IEnumerable<Job> GetAllJobs(bool updateStat);

        IList<string> GetAllTags();

        Job GetJob(int jobId, bool updateStat);

        string GetJobUrl(int id, string title);
    }
}
