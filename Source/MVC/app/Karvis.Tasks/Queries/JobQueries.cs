using System;
using System.Linq;
using System.Linq.Expressions;
using Karvis.Domain.Dto;

namespace Karvis.Domain.Queries
{
    public static class JobQueries
    {
        public static IQueryable<Job> QueryForAtiveJobsSpecific(this IQueryable<Job> jobs)
        {
            return jobs.Where(x => x.IsActive).Select(x => x);
        }

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

        public static IQueryable<Job> OrderByStringColumnName (this IQueryable<Job> jobs , string sort,string sortdir )
        {

            
            var type = typeof(Job);
            
            if (string.IsNullOrEmpty(sort))
            {
                sort = "DateAdded";
            }
            string sortdirection = sortdir=="ASC" ? "OrderBy" : "OrderByDescending";
            var property = type.GetProperty(sort);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            var resultExp = Expression.Call(typeof(Queryable), sortdirection, new Type[] { type, property.PropertyType }, jobs.Expression, Expression.Quote(orderByExp));
            return jobs.Provider.CreateQuery<Job>(resultExp);

        }


    }
}