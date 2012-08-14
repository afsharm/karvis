using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using Karvis.Core;

namespace Karvis.Web
{
    public partial class JobList : Page, IJobListView
    {
        private IJobModel jobModel;
        private JobListPresenter presenter;

        #region IJobListView Members

        public event EventHandler SearchButtonClicked;

        public event EventHandler<TEventArgs<int>> PageIndexChanged;

        public event EventHandler<TEventArgs<string>> SortChanged;

        public SearchCriteriaDto GetSearchCriteria()
        {
            var crit = new SearchCriteriaDto
                           {
                               Title = txtTitle.Text,
                               Tag = txtTag.Text,
                               AdSource = (AdSource) Enum.Parse(typeof (AdSource), ddlAdSource.SelectedValue),
                               ActiveStatus = rblIsActive.SelectedValue
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

        public event EventHandler<TEventArgs<int>> DeleteButtonPressed;

        public void ShowMessage(string message)
        {
            lblMessage.Text = message;
        }

        public bool IsUserAuthorized()
        {
            if (User != null && User.Identity != null
                && User.Identity.IsAuthenticated && User.IsInRole("AdminRole"))
                return true;

            return false;
        }

        public void DisableAutorizedSections()
        {
            int columnCount = dgJobList.Columns.Count;

            dgJobList.Columns[columnCount - 2].Visible = false;
            dgJobList.Columns[columnCount - 3].Visible = false;
            dgJobList.Columns[columnCount - 4].Visible = false;

            rblIsActive.Visible = false;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            presenter = new JobListPresenter(this, new JobModel());
            lblMessage.Text = string.Empty;

            //mvp can not be applied
            if (jobModel == null)
                jobModel = new JobModel();

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
            //mvp can not helpe here

            return jobModel.GetJobUrl(Convert.ToInt32(id), Convert.ToString(title));
        }

        protected string MyGetJobUrlModify(object id)
        {
            //mvp can not helpe here

            return jobModel.GetJobUrlModify(Convert.ToInt32(id));
        }

        protected void dgJobList_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            InvokeSortChanged(e.SortExpression);
        }

        protected void dgJobList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            InvokePageIndexChanged(e.NewPageIndex);
        }

        private void InvokeSearchButtonClicked()
        {
            if (SearchButtonClicked != null)
                SearchButtonClicked(this, EventArgs.Empty);
        }

        private void InvokePageIndexChanged(int newPageIndex)
        {
            if (PageIndexChanged != null)
                PageIndexChanged(this, new TEventArgs<int>(newPageIndex));
        }

        private void InvokeSortChanged(string sortExpression)
        {
            if (SortChanged != null)
                SortChanged(this, new TEventArgs<string>(sortExpression));
        }

        private void InvokeViewInitialized()
        {
            if (ViewInitialized != null)
                ViewInitialized(this, EventArgs.Empty);
        }

        private void InvokeDeleteButtonPressed(int jobId)
        {
            if (DeleteButtonPressed != null)
                DeleteButtonPressed(this, new TEventArgs<int>(jobId));
        }


    }
}