using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Karvis.Core;

namespace Karvis.Web.Admin
{
    public partial class ExtractJobs : System.Web.UI.Page, IExtractJobsView
    {
        private ExtractJobsPresenter presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            presenter = new ExtractJobsPresenter(this, new ExtractJobsModel(), new JobModel());

            if (!IsPostBack)
            {
                InvokeViewInitialized();
            }
        }

        private void InvokeViewInitialized()
        {
            if (ViewInitialized != null)
                ViewInitialized(this, EventArgs.Empty);
        }

        protected void btnApplyJobs_Click(object sender, EventArgs e)
        {
            InvokeApplyJobsButtonPressed();
        }

        protected void btnExtractJobs_Click(object sender, EventArgs e)
        {
            var rahnama = "http://www.rahnama.com/component/mtree/%DA%AF%D8%B1%D9%88%D9%87/35179/%D8%A8%D8%B1%D9%86%D8%A7%D9%85%D9%87-%D9%86%D9%88%D9%8A%D8%B3.html";

            InvokeExtractJobsButtonPressed(rahnama);
        }

        public event EventHandler<TEventArgs<string>> ExtractJobsButtonPressed;

        void InvokeExtractJobsButtonPressed(string data)
        {
            if (ExtractJobsButtonPressed != null)
                ExtractJobsButtonPressed(this, new TEventArgs<string>(data));
        }

        void InvokeApplyJobsButtonPressed()
        {
            if (ApplyJobsButtonPressed != null)
                ApplyJobsButtonPressed(this, EventArgs.Empty);
        }

        public event EventHandler ApplyJobsButtonPressed;

        public void ShowJobs(List<Job> jobs)
        {
            rptPreJob.DataSource = jobs;
            rptPreJob.DataBind();
        }

        public List<Job> ReadJobs()
        {
            List<Job> retval = new List<Job>();

            foreach (RepeaterItem item in rptPreJob.Items)
            {
                CheckBox chkApply = item.FindControl("chkApply") as CheckBox;
                if (!chkApply.Checked)
                    continue;

                TextBox txtDescription = item.FindControl("txtDescription") as TextBox;
                TextBox txtEmails = item.FindControl("txtEmails") as TextBox;
                TextBox txtTag = item.FindControl("txtTag") as TextBox;
                TextBox txtTitle = item.FindControl("txtTitle") as TextBox;
                TextBox txtUrl = item.FindControl("txtUrl") as TextBox;

                Job job = new Job()
                {
                    Description = txtDescription.Text,
                    Emails = txtEmails.Text,
                    Tag = txtTag.Text,
                    Title = txtTitle.Text,
                    Url = txtUrl.Text
                };

                retval.Add(job);
            }

            return retval;
        }

        public event EventHandler ViewInitialized;

        public void ShowMessage(string message)
        {
            lblMessage.Text = message;
        }
    }
}