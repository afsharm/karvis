using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Karvis.Core;


namespace Karvis.Web
{
    public class StatsAdSourcePresenter : Presenter<IStatsAdSourceView>
    {
        private readonly IJobModel jobModel;

        public StatsAdSourcePresenter(IStatsAdSourceView view)
            : this(view, IoC.Resolve<IJobModel>())
        {
        }

        public StatsAdSourcePresenter(IStatsAdSourceView view, IJobModel jobModel)
            : base(view)
        {
            this.jobModel = jobModel;

            view.ViewInitialized += view_ViewInitialized;
        }

        void view_ViewInitialized(object sender, EventArgs e)
        {
            IList<AdSourceStatDto> list = jobModel.ExtractAdSourceStat();
            int totalCount = jobModel.GetTotalJobCount();
            View.ShowAdSourceStat(list, totalCount);
        }
    }
}
