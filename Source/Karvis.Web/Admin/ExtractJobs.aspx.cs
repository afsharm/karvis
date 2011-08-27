using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Karvis.Core;

namespace Karvis.Web.Admin
{
    public partial class ExtractJobs : System.Web.UI.Page, IExtractJobsView
    {
        private ExtractJobsPresenter presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            presenter = new ExtractJobsPresenter(this, new ExtractJobsModel());

            if (!IsPostBack)
            {
                InvokeViewInitialized();
            }
        }

        private void InvokeViewInitialized()
        {
            if (ViewInitialized != null)
                ViewInitialized(this, EventArgs.Empty);
        }

        protected void btnExtractJobs_Click(object sender, EventArgs e)
        {
            var rahnama = "http://www.rahnama.com/component/mtree/%DA%AF%D8%B1%D9%88%D9%87/35179/%D8%A8%D8%B1%D9%86%D8%A7%D9%85%D9%87-%D9%86%D9%88%D9%8A%D8%B3.html";

            InvokeExtractJobsButtonPressed(rahnama);
        }

        public event EventHandler<TEventArgs<string>> ExtractJobsButtonPressed;

        void InvokeExtractJobsButtonPressed(string data)
        {
            if (ExtractJobsButtonPressed != null)
                ExtractJobsButtonPressed(this, new TEventArgs<string>(data));
        }

        public event EventHandler<TEventArgs<string>> InjectJobsButtonPressed;

        public void ShowJobs(List<Job> jobs)
        {
            grdEmails.DataSource = jobs;
            grdEmails.DataBind();
        }

        public void ReadJobs(System.Web.UI.HtmlControls.HtmlTable htmlTable)
        {
            throw new NotImplementedException();
        }

        public event EventHandler ViewInitialized;
    }
}