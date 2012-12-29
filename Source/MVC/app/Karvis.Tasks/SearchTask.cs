using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fardis;
using Karvis.Domain;
using Karvis.Domain.Queries;
using Karvis.Domain.Tasks;
using Karvis.Domain.ViewModels;

namespace Karvis.Tasks
{
 public     class SearchTask :ISearchTask
 {
     private readonly IJobTask _jobTask;

     public SearchTask(IJobTask jobTask)
     {
         _jobTask = jobTask;
     }

     #region Implementation of ISearchTask

     public SearchViewModel GetRawModel()
     {
         var adSources = GetAdSources();
         return new SearchViewModel()
                    {
                        SearchSource = adSources
                    };
     }

     private static IEnumerable<SelectListItem> GetAdSources()
     {
         IEnumerable<SelectListItem> adSources = Enum.GetValues(typeof (AdSource))
             .Cast<AdSource>()
             .Select(x => new SelectListItem
                              {
                                  Text = JobSummeryViewModel.GetAdSourceDescription(x),
                                  Value = x.ToString()
                              });
         return adSources;
     }

     public SearchResultViewModel Search(SearchViewModel searchViewModel,string sort, string sortdir, int page)
     {
         var activeJobs = _jobTask.GetQueryable();
         if (!string.IsNullOrEmpty(searchViewModel.SearchTerm))
         {
         activeJobs= activeJobs.Where(x => x.Title.Contains(searchViewModel.SearchTerm));
             }
         if (!string.IsNullOrEmpty(searchViewModel.SearchTag))
         {
             activeJobs = activeJobs.Where(x => x.Tag.Contains(searchViewModel.SearchTag));
         }
         if (!string.IsNullOrEmpty(searchViewModel.AdSource))
         {
             var adSource = (AdSource)Enum.Parse(typeof(AdSource), searchViewModel.AdSource);
             if (adSource!=AdSource.All)
             {
                 activeJobs = activeJobs.Where(x => x.AdSource == adSource);
               
             }
         }
         var totalJobs = activeJobs.Count();
         var takeCount = 10;
         if (totalJobs<10)
         {
             takeCount = totalJobs;
         }
         var fardis = new DateTimeHelper();
         var jobSummeryList =
             activeJobs.OrderByStringColumnName(sort, sortdir).
                 QueryForAtiveJobsSummery().
                 Select(x => new JobSummeryViewModel
                                 {
                                     Title = x.Title,
                                     Id = x.Id,
                                     DateAdded = fardis.ConvertToPersianDatePersianDigit(x.AddedDate),
                                     AdSource = x.Source,
                                     Tag = x.Tag.ToString(CultureInfo.InvariantCulture),
                                     VisitCount = x.VisitCount.ToString(CultureInfo.InvariantCulture)
                                 });
         if (totalJobs>10)
         {
             jobSummeryList = jobSummeryList.Skip((page - 1)*10).Take(takeCount);
         }
         searchViewModel.SearchSource =           GetAdSources();

         return new SearchResultViewModel(){JobViewModel = new JobViewModel()
                                                               {
                                                                   Jobs = jobSummeryList.ToList(),TotalJobsCount = totalJobs
                                                               },SearchViewModel = searchViewModel};
     
     }

     #endregion
    }
}
