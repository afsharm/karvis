using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SJ.Core;

namespace SJ.Web
{
    public partial class JobDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string jobId = Request.QueryString["ID"];

                if (string.IsNullOrEmpty(jobId))
                    Response.Redirect("JobList.aspx");

                Job job = JobDao.GetJob(jobId);

                txtTitle.Text = job.Title;
                txtDescription.Text = job.Description;
                txtTag.Text = job.Tag;
                hlkURL.NavigateUrl = job.URL;

                txtDateAdded.Text = job.DateAdded.ToString();
            }
        }
    }
}