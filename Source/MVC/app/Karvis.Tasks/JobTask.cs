using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Fardis;
using Karvis.Domain;
using Karvis.Domain.Queries;
using Karvis.Domain.Tasks;
using Karvis.Domain.ViewModels;
using Razmyar.SharpLite.Tasks;
using SharpLite.Domain.DataInterfaces;

namespace Karvis.Tasks
{
    public class JobTask : CrudTask<Job>, IJobTask
    {
        public JobTask(IRepository<Job> repository) : base(repository)
        {
        }

        #region IJobTask Members

        public JobViewModel GetSummeryPaged(string sort, string sortdir, int page)
        {
            var fardis = new DateTimeHelper();
            List<JobSummery> jobSummeryList =
                base.GetQueryable().Skip((page - 1)*10).Take(10).OrderByDescending(x => x.DateAdded).
                    QueryForAtiveJobsSummery
                    ().
                    Select(x => new JobSummery
                                    {
                                        Title = x.Title,
                                        Id = x.Id.ToString(CultureInfo.InvariantCulture),
                                        AddedDate = fardis.ConvertToPersianDatePersianDigit(x.AddedDate),
                                        AdSource = x.Source,
                                        Tag = x.Tag.ToString(CultureInfo.InvariantCulture),
                                        VisitsCount = x.VisitCount.ToString(CultureInfo.InvariantCulture)
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
            var fardis = new DateTimeHelper();

            GetQueryable().QueryForAtiveJobsSpecific().Where(x => x.Id == id).Select(x => new JobDescriptionViewModel
                                                                                              {
                                                                                                  Description =
                                                                                                      x.Description
                                                                                                  ,
                                                                                                  FeedVisitsCount =
                                                                                                      x.FeedCount.
                                                                                                      ToString(),
                                                                                                  Link = x.Url,
                                                                                                  JobSummery =
                                                                                                      GetJobSummery(x)
                                                                                              });
        }

        #endregion

        private static JobSummery GetJobSummery(Job x)
        {
            var fardis = new DateTimeHelper();
            return
                new JobSummery
                    {
                        AddedDate =
                            fardis.
                            ConvertToPersianDatePersianDigit
                            (x.
                                 DateAdded),
                        AdSource =
                            x.AdSource
                        ,
                        Id =
                            x.Id.
                            ToString(),
                        Tag = x.Tag,
                        Title =
                            x.Title,
                        VisitsCount =
                            x.
                            VisitCount
                            .ToString()
                    };
        }
    }
}