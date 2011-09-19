using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Karvis.Core;

namespace Karvis.Web
{
    public class JobListPresenter : Presenter<IJobListView>
    {
        private readonly IJobModel jobModel;

        public JobListPresenter(IJobListView view)
            : this(view, IoC.Resolve<IJobModel>())
        {
        }

        public JobListPresenter(IJobListView view, IJobModel jobModel)
            : base(view)
        {
            this.jobModel = jobModel;

            view.PageIndexChanged += view_PageIndexChanged;
            view.SearchButtonClicked += view_SearchButtonClicked;
            view.SortChanged += view_SortChanged;
            view.ViewInitialized += view_ViewInitialized;
            view.DeleteButtonPressed += view_DeleteButtonPressed;
        }

        void view_DeleteButtonPressed(object sender, TEventArgs<int> e)
        {
            if (!View.IsUserAuthorized())
            {
                View.ShowMessage("شما دسترسی لازم را ندارید");
                return;
            }

            jobModel.DeleteJob(e.Data);
            ShowFirstPage();
            View.ShowMessage("حذف شد");
        }

        void view_ViewInitialized(object sender, EventArgs e)
        {
            ShowFirstPage();
        }

        void view_SortChanged(object sender, TEventArgs<string> e)
        {
            string oldValue = View.GetSortExpression();
            const string DESC = " desc";
            int descLength = DESC.Length;
            string newValue = e.Data;

            if (oldValue == newValue)
                newValue += DESC;
            if (oldValue.EndsWith(DESC))
                newValue = oldValue.Remove(oldValue.Length - descLength, descLength);

            View.UpdateSortExpression(newValue);
            ShowFirstPage();
        }

        void ShowFirstPage()
        {
            if (!View.IsUserAuthorized())
                View.DisableAutorizedSections();

            SearchCriteriaDto crit = View.GetSearchCriteria();
            ShowPages(crit, 0);
        }

        private void ShowPages(SearchCriteriaDto crit, int pageIndex)
        {
            string sortExpression = View.GetSortExpression();
            int pageSize = View.GetPageSize();

            bool? isActive = ConvertIsActiveValue(crit.ActiveStatus);
            if (!View.IsUserAuthorized())
                isActive = true;

            var jobs = jobModel.FindAll(crit.Title, crit.Tag, crit.AdSource, isActive, sortExpression, pageSize, pageIndex * pageSize);
            int count = jobModel.FindAllCount(crit.Title, crit.Tag, crit.AdSource, isActive);
            View.ShowJobs(jobs, count, pageIndex);
        }

        private bool? ConvertIsActiveValue(string activeStatus)
        {
            switch (activeStatus)
            {
                case "Active":
                    return true;
                case "NotActive":
                    return false;
                default:
                    return null;
            }
        }

        void view_SearchButtonClicked(object sender, EventArgs e)
        {
            ShowFirstPage();
        }

        void view_PageIndexChanged(object sender, TEventArgs<int> e)
        {
            SearchCriteriaDto crit = View.GetSearchCriteria();
            ShowPages(crit, e.Data);
        }
    }
}
