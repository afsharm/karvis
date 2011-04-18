using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SJ.Core;

namespace SJ.Web.Admin
{
    public partial class Config : System.Web.UI.Page
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
                NHHelper.Instance.SchemaExport();
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