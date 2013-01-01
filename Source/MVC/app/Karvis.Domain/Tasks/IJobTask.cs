using System.Collections.Generic;
using Karvis.Domain.ViewModels;
using Razmyar.Domain.Contracts.Tasks;

namespace Karvis.Domain.Tasks
{
    public interface IJobTask : ICrudTask<Job>
    {
        JobViewModel GetSummeryPaged(string sort, string sortdir, int page);
        JobDescriptionViewModel GetJobDescription(int id);
        void SubmitJob  (SubmitJobViewModel submitJobViewModel);
    }
}