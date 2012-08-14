using System.Linq;
using Karvis.Domain.Dto;

namespace Karvis.Domain.Queries
{
    public static class ActiveJobSummeryExtension
    {
        public static IQueryable<JobSummeryDto> QueryForAtiveJobsSummery(this IQueryable<Job> jobs)
        {
            return jobs.Where(x => x.IsActive).Select(x => new JobSummeryDto
                                                               {
                                                                   Id = x.Id,
                                                                   AddedDate = x.DateAdded,
                                                                   Source = x.AdSource,
                                                                   Tag = x.Tag,
                                                                   Title = x.Title,
                                                                   VisitCount = x.VisitCount
                                                               })
                ;
        }
    }
}