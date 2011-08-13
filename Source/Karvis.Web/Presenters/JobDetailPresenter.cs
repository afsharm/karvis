using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Karvis.Core;


namespace Karvis.Web
{
    public class JobDetailPresenter : Presenter<IJobDetailView>
    {
        private readonly IJobModel jobModel;

        public JobDetailPresenter(IJobDetailView view)
            : this(view, IoC.Resolve<IJobModel>())
        {
        }

        public JobDetailPresenter(IJobDetailView view, IJobModel jobModel)
            : base(view)
        {
            this.jobModel = jobModel;
            view.JobSelectedForDisplay += view_JobSelectedForDisplay;
        }

        void view_JobSelectedForDisplay(object sender, TEventArgs<string> e)
        {
            var job = jobModel.GetJob(Convert.ToInt32(e.Data), true);
            View.ShowJob(job);
        }

        protected override void ViewInitialized(object sender, EventArgs e)
        {

        }
    }
}
