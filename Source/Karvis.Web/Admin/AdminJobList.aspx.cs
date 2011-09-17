﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Karvis.Core;

namespace Karvis.Web.Admin
{
    public partial class AdminJobList : System.Web.UI.Page
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

        protected string MyGetJobUrl(object id, object title)
        {
            return "~/SubmitJob.aspx?id=" + id.ToString();
            //return new JobModel().GetJobUrl(Convert.ToInt32(id), Convert.ToString(title));
        }
    }
}