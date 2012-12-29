using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Karvis.Domain.Dto;

namespace Karvis.Domain.Queries
{
   public static class TagQueries
    {
       public static IQueryable<TagDto>  GetAllActiveTags (this IQueryable<Job> jobs )
       {
           return jobs.Where(x => x.IsActive).Select(x => new TagDto() {TagName = x.Tag});
       }
    }
}
