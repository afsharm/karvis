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
            string siteSource = ddlSiteSource.SelectedValue;
            InvokeExtractJobsButtonPressed(siteSource);
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

        public void ShowJobs(IList<Job> jobs)
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
                Label lblId = item.FindControl("lblId") as Label;
                HiddenField hdnAdSource = item.FindControl("hdnAdSource") as HiddenField;

                int preSavedJobId = 0;
                int.TryParse(lblId.Text, out preSavedJobId);

                Job job = new Job()
                {
                    Description = txtDescription.Text,
                    Emails = txtEmails.Text,
                    Tag = txtTag.Text,
                    Title = txtTitle.Text,
                    Url = txtUrl.Text,
                    PreSavedJobId = preSavedJobId,
                    AdSource = (AdSource)Enum.Parse(typeof(AdSource), hdnAdSource.Value)
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

        private void InvokeTempSaveButtonPressed()
        {
            if (TempSaveButtonPressed != null)
                TempSaveButtonPressed(this, EventArgs.Empty);
        }

        public event EventHandler TempSaveButtonPressed;

        protected void btnTempSave_Click(object sender, EventArgs e)
        {
            InvokeTempSaveButtonPressed();
        }

        public void DisableExtractButton()
        {
            btnExtractJobs.Enabled = false;
        }

        public void DisableApplyButton()
        {
            btnApplyJobs.Enabled = false;
        }

        public void EnableApplyButton()
        {
            btnApplyJobs.Enabled = true;
        }


        public void DisableTempSaveButton()
        {
            btnTempSave.Enabled = false;
        }

        public void EnableTempSaveButton()
        {
            btnTempSave.Enabled = true;
        }


        public void CleaJobs()
        {
            rptPreJob.DataSource = null;
            rptPreJob.DataBind();
        }
    }
}