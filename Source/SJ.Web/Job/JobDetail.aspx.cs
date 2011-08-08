using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SJ.Core;

namespace SJ.Web
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

                string jobIdArgument = Request.QueryString["ID"];

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
            lnkUrl.Text = job.Url;
            lblTag.Text = job.Tag;
            lblDateAddedPersian.Text = job.DateAddedPersian;
            lblVisitCount.Text = job.VisitCount.ToString();
            lblFeedCount.Text = job.FeedCount.ToString();
        }

        private void InvokeViewInitialized(EventArgs e)
        {
            if (ViewInitialized != null) ViewInitialized(this, e);
        }

        public event EventHandler ViewInitialized;
    }
}