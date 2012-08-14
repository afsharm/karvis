using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Karvis.Core
{
    public class IgnoredJobModel : IIgnoredJobModel
    {
        ISessionFactory _sessionFactory;
        NHibernateRepository<IgnoredJob> _ignoredJobRepository;

        public IgnoredJobModel()
        {
            _sessionFactory = NHHelper.Instance.GetSessionFactory();
            _ignoredJobRepository = new NHibernateRepository<IgnoredJob>(_sessionFactory);
        }

        public bool ExistsJob(string url, AdSource adSource)
        {
            var q = _ignoredJobRepository.QueryOver().Where(
                ij => ij.AdSource == adSource && ij.Url.ToLower() == url.ToLower());

            var res = q.Take(1).List();
            if (res != null && res.Count > 0)
                return true;
            else
                return false;
        }

        public void AddIgnoredJob(string url, AdSource adSource)
        {
            _ignoredJobRepository.Add(new IgnoredJob
            {
                AdSource = adSource,
                Url = url
            });
        }

        public IList<string> GetIgnoredJobs(AdSource adSource)
        {
            var q = _ignoredJobRepository.QueryOver().Where(ij => ij.AdSource == adSource).Select(ij => ij.Url);

            return q.List<string>();
        }


        public void AddBatchIgnoreJobUrls(AdSource siteSource, List<string> ignoreJobUrls)
        {
            foreach (var ignoredJobUrl in ignoreJobUrls)
                _ignoredJobRepository.Add(new IgnoredJob
                {
                    Url = ignoredJobUrl,
                    AdSource = siteSource
                });
        }
    }
}
