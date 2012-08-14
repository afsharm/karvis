using System;
using System.Web.UI;
using Karvis.Core;

namespace Karvis.Web.Admin
{
    public partial class Config : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                lblMessage.Text = string.Empty;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Membership.CanLogin(txtPassword.Text))
            {
                divControlPanel.Visible = true;
                divLogin.Visible = false;
            }
            else
            {
                lblMessage.Text = "Incorrect Password";
                txtPassword.Text = string.Empty;
            }
        }

        protected void btnCreateDB_Click(object sender, EventArgs e)
        {
            if (Membership.CanLogin(txtDBPassword.Text))
            {
                ConfigHelper.SchemaExport();
                lblMessage.Text = "Database created from scratch.";
            }
            else
            {
                lblMessage.Text = "Incorrect Password";
                txtPassword.Text = string.Empty;
            }
        }
    }
}