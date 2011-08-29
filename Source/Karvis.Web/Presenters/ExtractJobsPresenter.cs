using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Karvis.Core;


namespace Karvis.Web
{
    public class ExtractJobsPresenter : Presenter<IExtractJobsView>
    {
        private readonly IExtractJobsModel extractJobsModel;
        private readonly IJobModel jobModel;

        public ExtractJobsPresenter(IExtractJobsView view)
            : this(view, IoC.Resolve<IExtractJobsModel>(), IoC.Resolve<IJobModel>())
        {
        }

        public ExtractJobsPresenter(IExtractJobsView view, IExtractJobsModel extractJobsModel, IJobModel jobModel)
            : base(view)
        {
            this.extractJobsModel = extractJobsModel;
            this.jobModel = jobModel;

            view.ExtractJobsButtonPressed += view_ExtractJobsButtonPressed;
            view.ApplyJobsButtonPressed += view_ApplyJobsButtonPressed;
        }

        void view_ApplyJobsButtonPressed(object sender, EventArgs e)
        {
            List<Job> jobs = View.ReadJobs();
            int count = jobModel.AddJobBatch(jobs);

            View.ShowMessage(string.Format("{0} کار از سایت راهنما ثبت شد.", count));
        }

        void view_ExtractJobsButtonPressed(object sender, TEventArgs<string> e)
        {
            var jobs = extractJobsModel.ExtractJobs(e.Data);
            View.ShowJobs(jobs);
        }

        protected override void ViewInitialized(object sender, EventArgs e)
        {
            //
        }
    }
}
