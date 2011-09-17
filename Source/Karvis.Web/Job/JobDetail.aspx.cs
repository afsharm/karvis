using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Karvis.Core;

namespace Karvis.Web
{
    public partial class JobDetail : System.Web.UI.Page, IJobDetailView
    {
        private JobDetailPresenter presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                presenter = new JobDetailPresenter(this, new JobModel());
                InvokeViewInitialized(EventArgs.Empty);

                string jobIdArgument = Request.QueryString["Id"];

                if (string.IsNullOrEmpty(jobIdArgument))
                    Response.Redirect("JobList.aspx");

                InvokeJobSelectedForDisplay(jobIdArgument);
            }
        }

        void InvokeJobSelectedForDisplay(string jobId)
        {
            if (JobSelectedForDisplay != null)
                JobSelectedForDisplay(this, new TEventArgs<string>(jobId));
        }

        public event EventHandler<TEventArgs<string>> JobSelectedForDisplay;

        public void ShowJob(Job job)
        {
            this.Title = string.Format("کارویس - {0}", job.Title);

            lblTitle.Text = job.Title;
            lblDescription.Text = job.Description;
            lnkUrl.NavigateUrl = job.Url;
            lblTag.Text = job.Tag;
            lblAdSource.Text = job.AdSourceDescription;
            lblDateAddedPersian.Text = job.DateAddedPersian;
            lblVisitCount.Text = job.VisitCount.ToString();
            lblFeedCount.Text = job.FeedCount.ToString();
        }

        private void InvokeViewInitialized(EventArgs e)
        {
            if (ViewInitialized != null) ViewInitialized(this, e);
        }

        public event EventHandler ViewInitialized;

        public void ShowMessage(string message)
        {
            throw new NotImplementedException();
        }
    }
}