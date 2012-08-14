using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Karvis.Domain.Queries
{
   public static class ActiveJobSummeryExtension
   {
       public static IQueryable<JobSummeryDto> QueryForCustomerOrderSummaries(this IQueryable<Job> jobs)
       {
           return from customer in jobs
                  select new CustomerOrderSummaryDto()
                  {
                      FirstName = customer.FirstName,
                      LastName = customer.LastName,
                      OrderCount = customer.Orders.Count
                  };
       }
   }
}
