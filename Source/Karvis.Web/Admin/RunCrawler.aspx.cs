using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Karvis.Core;

namespace Karvis.Web.Admin
{
    public partial class RunCrawler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtSample.Text = "<div class=\"listimage\"><img title=\"برنامه‌نويس #C  مسلط به\" src=\"http://www.rahnama.com/images/ricon.jpg\"></div>        ";
        }

        protected void btnRunCrawler_Click(object sender, EventArgs e)
        {
            var crawler = new Crawler();

            var emails = crawler.ExtractEmails(txtUrl.Text);

            grdEmails.DataSource = emails;
            grdEmails.DataBind();
        }

        protected void btnExtractJobs_Click(object sender, EventArgs e)
        {
            var crawler = new Crawler();

            var rahnama = "http://www.rahnama.com/component/mtree/%DA%AF%D8%B1%D9%88%D9%87/35179/%D8%A8%D8%B1%D9%86%D8%A7%D9%85%D9%87-%D9%86%D9%88%D9%8A%D8%B3.html";
            var jobs = crawler.ExtractJobs(txtUrl.Text);

            grdEmails.DataSource = jobs;
            grdEmails.DataBind();
        }
    }
}