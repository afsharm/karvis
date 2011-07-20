using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SJ.Core;

namespace SJ.Web.Admin
{
    public partial class RunCrawler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRunCrawler_Click(object sender, EventArgs e)
        {
            var crawler = new Crawler();

            var emails = crawler.ExtractEmails("http://afsharm.com/");
        }
    }
}