using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<JobViewModel> GetSummeryPaged(string sort, string sortdir, int page)
        {
            return
                GetQueryable().Skip((page-1)*10).Take(10).OrderByDescending(x => x.DateAdded).QueryForAtiveJobsSummery().
                    Select(x => new JobViewModel
                                    {
                                        Title = x.Title,
                                        Id = x.Id.ToString(),
                                        RegistredDate = x.AddedDate.ToString(),
                                        Source = x.Source.ToString(),
                                        Tag = x.Tag.ToString()
                                    }).ToList();
        }

        #endregion
    }
}