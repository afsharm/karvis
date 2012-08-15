using System.Collections.Generic;
using Karvis.Domain.ViewModels;

namespace Karvis.Domain.Tasks
{
    public interface IJobTask
    {
        JobViewModel GetSummeryPaged(string sort, string sortdir, int page);
    }
}