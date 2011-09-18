using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Karvis.Core;

namespace Karvis.Web
{
    public partial class JobList : System.Web.UI.Page, IJobListView
    {
        JobListPresenter presenter;
        protected void Page_Load(object sender, EventArgs e)
        {
            presenter = new JobListPresenter(this, new JobModel());
            lblMessage.Text = string.Empty;

            if (!IsPostBack)
            {
                string tag = Request.QueryString["Tag"];

                if (!string.IsNullOrEmpty(tag))
                {
                    txtTag.Text = tag;
                    InvokeSearchButtonClicked();

                    return;
                }

                InvokeViewInitialized();
                int lastColumn = dgJobList.Columns.Count;
                dgJobList.Columns[lastColumn - 1].Visible = true;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            InvokeSearchButtonClicked();
        }

        protected void dgJobList_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
            InvokeDeleteButtonPressed(Convert.ToInt32(e.CommandArgument));
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.AppRelativeCurrentExecutionFilePath);
        }

        protected string MyGetJobUrl(object id, object title)
        {
            return new JobModel().GetJobUrl(Convert.ToInt32(id), Convert.ToString(title));
        }

        protected string MyGetJobUrlModify(object id, object title)
        {
            return new JobModel().GetJobUrl(Convert.ToInt32(id), Convert.ToString(title));
        }

        protected void dgJobList_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            InvokeSortChanged(e.SortExpression);
        }

        protected void dgJobList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            InvokePageIndexChanged(e.NewPageIndex);
        }

        public event EventHandler SearchButtonClicked;
        void InvokeSearchButtonClicked()
        {
            if (SearchButtonClicked != null)
                SearchButtonClicked(this, EventArgs.Empty);
        }

        public event EventHandler<TEventArgs<int>> PageIndexChanged;
        void InvokePageIndexChanged(int newPageIndex)
        {
            if (PageIndexChanged != null)
                PageIndexChanged(this, new TEventArgs<int>(newPageIndex));
        }

        public event EventHandler<TEventArgs<string>> SortChanged;
        void InvokeSortChanged(string sortExpression)
        {
            if (SortChanged != null)
                SortChanged(this, new TEventArgs<string>(sortExpression));
        }

        public event EventHandler<TEventArgs<string>> JobSelectedForDetail;
        void InvokeJobSelectedForDetail(string jobId)
        {
            if (JobSelectedForDetail != null)
                JobSelectedForDetail(this, new TEventArgs<string>(jobId));
        }

        public SearchCriteriaDto GetSearchCriteria()
        {
            SearchCriteriaDto crit = new SearchCriteriaDto
            {
                Title = txtTitle.Text,
                Tag = txtTag.Text,
                AdSource = (AdSource)Enum.Parse(typeof(AdSource), ddlAdSource.SelectedValue)
            };

            return crit;
        }

        public string GetSortExpression()
        {
            return hdnSortExpression.Value;
        }

        public int GetPageSize()
        {
            return dgJobList.PageSize;
        }

        public void ShowJobs(IList<Job> jobs, int virtualItemCount, int currentPageIndex)
        {
            dgJobList.DataSource = jobs;
            dgJobList.VirtualItemCount = virtualItemCount;
            dgJobList.CurrentPageIndex = currentPageIndex;
            dgJobList.DataBind();
        }

        public void Redirect(string url)
        {
            Response.Redirect(url);
        }

        public void UpdateSortExpression(string sortExpression)
        {
            hdnSortExpression.Value = sortExpression;
        }

        public event EventHandler ViewInitialized;
        void InvokeViewInitialized()
        {
            if (ViewInitialized != null)
                ViewInitialized(this, EventArgs.Empty);
        }

        public event EventHandler<TEventArgs<int>> DeleteButtonPressed;
        private void InvokeDeleteButtonPressed(int jobId)
        {
            if (DeleteButtonPressed != null)
                DeleteButtonPressed(this, new TEventArgs<int>(jobId));
        }

        public void ShowMessage(string message)
        {
            lblMessage.Text = message;
        }
    }
}