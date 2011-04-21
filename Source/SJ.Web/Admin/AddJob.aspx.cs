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
            if (!IsPostBack)
            {
                lblMessage.Text = string.Empty;
                frmJob.ChangeMode(FormViewMode.Insert);
            }
        }

        protected void odsJob_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            hdnID.Value = e.ReturnValue.ToString();
        }
    }
}