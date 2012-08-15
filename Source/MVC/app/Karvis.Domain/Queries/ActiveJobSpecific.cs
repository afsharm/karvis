using System.Linq;

namespace Karvis.Domain.Queries
{
    public static class ActiveJobSpecificExtension
    {
        public static IQueryable<Job> QueryForAtiveJobsSpecific(this IQueryable<Job> jobs)
        {
            return jobs.Where(x => x.IsActive).Select(x => x);
        }
    }
}