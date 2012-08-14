using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace Karvis.Core
{
    public interface IIgnoredJobModel
    {
        bool ExistsJob(string url, AdSource adSource);
        void AddIgnoredJob(string url, AdSource adSource);
        IList<string> GetIgnoredJobs(AdSource adSource);

        void AddBatchIgnoreJobUrls(AdSource siteSource, List<string> ignoreJobUrls);
    }
}
