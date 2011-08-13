using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Karvis.Core;

namespace Karvis.Web.Admin
{
    public partial class Monitor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Refresh();
        }

        public void btnRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            //lblFeedInfo.Text = FeedHelper.Instance.GetInfo();
        }
    }
}