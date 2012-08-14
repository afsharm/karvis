using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Karvis.Core;

namespace Karvis.Web.Stats
{
    public partial class StatsAdSource : System.Web.UI.Page, IStatsAdSourceView
    {
        private StatsAdSourcePresenter presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            presenter = new StatsAdSourcePresenter(this, new JobModel());

            if (!IsPostBack)
                InvokeViewInitialized();
        }

        private void InvokeViewInitialized()
        {
            if (ViewInitialized != null)
                ViewInitialized(this, EventArgs.Empty);
        }

        public void ShowAdSourceStat(IList<AdSourceStatDto> dto, int totalJobCount)
        {
            jobsCount.Text = totalJobCount.ToString();

            rptJobAdSource.DataSource = dto;
            rptJobAdSource.DataBind();

            chartAdSource.DataSource = dto;

            chartAdSource.Series["Series 1"].XValueMember = "SiteSourceDescription";
            chartAdSource.Series["Series 1"].YValueMembers = "Count";

            chartAdSource.DataBind();
        }

        public event EventHandler ViewInitialized;

        public void ShowMessage(string message)
        {
            //
        }
    }
}