using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Fardis;
using Karvis.Domain;
using Karvis.Domain.Dto;
using Karvis.Domain.Queries;
using Karvis.Domain.Tasks;
using Karvis.Domain.ViewModels;
using Razmyar.SharpLite.Tasks;
using SharpLite.Domain.DataInterfaces;

namespace Karvis.Tasks
{
    public class JobTask : CrudTask<Job>, IJobTask
    {
        public JobTask(IRepository<Job> repository)
            : base(repository)
        {
        }

        #region IJobTask Members

        public JobViewModel GetSummeryPaged(string sort, string sortdir, int page)
        {
            var fardis = new DateTimeHelper();
            var jobSummeryList =
                    base.GetQueryable().Skip((page - 1) * 10).Take(10).OrderByStringColumnName(sort,sortdir).QueryForAtiveJobsSummery().
                        Select(x => new JobSummeryViewModel
                                        {
                                            Title = x.Title,
                                            Id = x.Id,
                                            DateAdded = fardis.ConvertToPersianDatePersianDigit(x.AddedDate),
                                            AdSource = x.Source,
                                            Tag = x.Tag.ToString(CultureInfo.InvariantCulture),
                                            VisitCount = x.VisitCount.ToString(CultureInfo.InvariantCulture)
                                        }).ToList();

            var jobViewModel = new JobViewModel
                                   {
                                       Jobs = jobSummeryList,
                                       TotalJobsCount = GetQueryable().QueryForAtiveJobsSummery().Count()
                                   };
            return jobViewModel;
        }

        public JobDescriptionViewModel GetJobDescription(int id)
        {
            IncreaseVisitCount(id);
            var job=
                GetQueryable().QueryForAtiveJobsSpecific().Where(x => x.Id == id).Select(
                    x => new JobDescriptionViewModel
                             {
                                 Description =
                                     x.Description
                                 ,
                                 FeedVisitsCount =
                                     x.FeedCount.
                                     ToString(CultureInfo.InvariantCulture),
                                 Link = x.Url,
                                 JobSummeryViewModel =
                                     GetJobSummery(x)
                             }).Single();

            job.Tags = job.JobSummeryViewModel.Tag.Split(',').Select(x => new TagDto(){TagName = x});
            return job;
        }


       

        #endregion
        private  void IncreaseVisitCount(int jobId)
        {
            var job = GetItem(jobId);
            job.VisitCount++;
            UpdateItem(job);
        }
        private static JobSummeryViewModel GetJobSummery(Job x)
        {
            var fardis = new DateTimeHelper();
            return
                new JobSummeryViewModel
                    {
                        DateAdded =
                            fardis.
                            ConvertToPersianDatePersianDigit
                            (x.
                                 DateAdded),
                        AdSource =
                            x.AdSource
                        ,
                        Id =
                            x.Id
                        ,
                        Tag = x.Tag,
                        Title =
                            x.Title,
                        VisitCount =
                            x.
                            VisitCount
                            .ToString()
                    };
        }
    }
}