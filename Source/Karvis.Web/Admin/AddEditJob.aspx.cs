using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Karvis.Core;

namespace Karvis.Web.Admin
{
    public partial class AddJob : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMessage.Text = string.Empty;

                string ID = Request["ID"];

                if (!string.IsNullOrEmpty(ID))
                {
                    hdnID.Value = ID;
                    frmJob.ChangeMode(FormViewMode.Edit);
                }
                else
                    frmJob.ChangeMode(FormViewMode.Insert);
            }
        }

        protected void odsJob_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            hdnID.Value = e.ReturnValue.ToString();
        }
    }
}