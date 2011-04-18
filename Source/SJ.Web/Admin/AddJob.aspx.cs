using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SJ.Core;

namespace SJ.Web.Admin
{
    public partial class AddJob : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                JobDao.AddNewJob(txtTitle.Text, txtDescription.Text, txtURL.Text, txtTag.Text);
                New();
                lblMessage.Text = "saved!";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }

        private void New()
        {
            txtTitle.Text = txtDescription.Text = txtURL.Text = txtTag.Text = string.Empty;
        }
    }
}