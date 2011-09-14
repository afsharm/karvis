using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Karvis.Core;

namespace Karvis.Web
{
    public partial class SubmitJob : System.Web.UI.Page, ISubmitJobView
    {
        private SubmitJobPresenter presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            presenter = new SubmitJobPresenter(this, new JobModel());

            if (!IsPostBack)
            {
                InvokeViewInitialized();
            }
        }

        protected void btnSaveUpdate_Click(object sender, EventArgs e)
        {
            InvokeSaveUpdateButtonPressed();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            InvokeNewButtonPressed();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            InvokeEditButtonPressed();
        }

        public event EventHandler ViewInitialized;
        public event EventHandler SaveUpdateButtonPressed;
        public event EventHandler EditButtonPressed;
        public event EventHandler NewButtonPressed;

        void InvokeViewInitialized()
        {
            if (ViewInitialized != null)
                ViewInitialized(this, EventArgs.Empty);
        }

        void InvokeSaveUpdateButtonPressed()
        {
            if (SaveUpdateButtonPressed != null)
                SaveUpdateButtonPressed(this, EventArgs.Empty);
        }

        void InvokeEditButtonPressed()
        {
            if (EditButtonPressed != null)
                EditButtonPressed(this, EventArgs.Empty);
        }

        void InvokeNewButtonPressed()
        {
            if (NewButtonPressed != null)
                NewButtonPressed(this, EventArgs.Empty);
        }

        public void ShowJob(Job job)
        {
            lblId.Text = job.Id.ToString();
            txtTag.Text = job.Tag;
            chkIsActive.Checked = job.IsActive;
            txtTitle.Text = job.Title;
            txtURL.Text = job.Url;
            ckDescription.Text = job.Description;
            hdnJobId.Value = job.Id.ToString();
        }

        public Job ReadJob()
        {
            Job job = new Job()
            {
                Description = ckDescription.Text,
                Tag = txtTag.Text,
                IsActive = chkIsActive.Checked,
                Title = txtTitle.Text,
                Url = txtURL.Text
            };

            int preSavedJobId = 0;
            if (int.TryParse(lblId.Text, out preSavedJobId))
                job.PreSavedJobId = preSavedJobId;

            return job;
        }

        public bool IsUserAuthorized()
        {
            if (this.User != null && this.User.Identity != null
                && this.User.Identity.IsAuthenticated && this.User.IsInRole("AdminRole"))
                return true;

            return false;
        }

        public bool IsInEditMode()
        {
            int id = 0;

            if (int.TryParse(Request["Id"], out id))
                return true;

            if (int.TryParse(hdnJobId.Value, out id))
                return true;

            return false;
        }

        public void ForbidPage()
        {
            txtTag.Enabled = txtTitle.Enabled = txtURL.Enabled = chkIsActive.Enabled =
            btnSaveUpdate.Enabled = btnNew.Enabled = btnEdit.Enabled = false;
            ckDescription.ReadOnly = true;
        }

        public void DisableAutorizedSections()
        {
            lblActive.Visible = btnEdit.Visible = chkIsActive.Visible = false;
        }

        public void ShowMessage(string message)
        {
            lblMessage.Text = message;
        }

        public void Clear()
        {
            lblMessage.Text = txtTag.Text = txtTitle.Text = txtURL.Text = ckDescription.Text = lblId.Text = string.Empty;
            chkIsActive.Checked = false;
            txtTag.Enabled = txtTitle.Enabled = txtURL.Enabled = chkIsActive.Enabled =
               btnSaveUpdate.Enabled = true;
            ckDescription.ReadOnly = false;

            txtTitle.Focus();
        }

        public int GetEditingJobId()
        {
            //validation has been done before
            return Convert.ToInt32(Request["Id"]);
        }

        public void Freez()
        {
            txtTag.Enabled = txtTitle.Enabled = txtURL.Enabled = chkIsActive.Enabled =
               btnSaveUpdate.Enabled = false;
            ckDescription.ReadOnly = true;
        }

        public void UnFreez()
        {
            txtTag.Enabled = txtTitle.Enabled = txtURL.Enabled = chkIsActive.Enabled =
               btnSaveUpdate.Enabled = true;
            ckDescription.ReadOnly = false;
        }


        public void DisableNewButton()
        {
            btnNew.Enabled = false;
        }

        public void DisableSaveButton()
        {
            btnSaveUpdate.Enabled = false;
        }

        public void DisableEditButton()
        {
            btnEdit.Enabled = false;
        }


        public void EnableNewButton()
        {
            btnNew.Enabled = true;
        }

        public void EnableSaveButton()
        {
            btnSaveUpdate.Enabled = true;
        }

        public void EnableEditButton()
        {
            btnEdit.Enabled = true;
        }
    }
}