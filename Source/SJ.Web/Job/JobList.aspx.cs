using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SJ.Web
{
    public partial class JobList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string tag = Request.QueryString["Tag"];

            if (!string.IsNullOrEmpty(tag))
                txtTag.Text = tag;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            grdJobList.DataBind();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.AppRelativeCurrentExecutionFilePath);
        }
    }
}