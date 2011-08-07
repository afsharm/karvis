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
                string jobId = Request.QueryString["ID"];

                if (string.IsNullOrEmpty(jobId))
                    Response.Redirect("JobList.aspx");

                SetJob(Convert.ToInt32(jobId));
            }
        }

        public void SetJob(int jobId)
        {
            IJobModel jobModel = new JobModel();
            var job = jobModel.GetJob(jobId, true);
            this.Title = string.Format("کارویس - {0}", job.Title);

            lblTitle.Text = job.Title;
            lblDescription.Text = job.Description;
            lnkUrl.Text = job.Url;
            lblTag.Text = job.Tag;
            lblDateAddedPersian.Text = job.DateAddedPersian;
            lblVisitCount.Text = job.VisitCount.ToString();
            lblFeedCount.Text = job.FeedCount.ToString();
        }

        public event EventHandler ViewInitialized;
    }
}