using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Karvis.Core;


namespace Karvis.Web
{
    public interface IJobListView : IView
    {
        event EventHandler SearchButtonClicked;
        event EventHandler<TEventArgs<int>> PageIndexChanged;
        event EventHandler<TEventArgs<string>> SortChanged;
        event EventHandler<TEventArgs<string>> JobSelectedForDetail;

        SearchCriteriaDto GetSearchCriteria();
        string GetSortExpression();
        int GetPageSize();
        void ShowJobs(IList<Job> jobs, int virtualItemCount, int currentPageIndex);
        void Redirect(string url);
        void UpdateSortExpression(string sortExpression);
    }
}
