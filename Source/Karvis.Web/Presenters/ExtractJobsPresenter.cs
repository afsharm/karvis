using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Karvis.Core;


namespace Karvis.Web
{
    public class ExtractJobsPresenter : Presenter<IExtractJobsView>
    {
        private readonly IExtractJobsModel model;

        public ExtractJobsPresenter(IExtractJobsView view)
            : this(view, IoC.Resolve<IExtractJobsModel>())
        {
        }

        public ExtractJobsPresenter(IExtractJobsView view, IExtractJobsModel model)
            : base(view)
        {
            this.model = model;

            view.ExtractJobsButtonPressed += view_ExtractJobsButtonPressed;
            view.InjectJobsButtonPressed += view_InjectJobsButtonPressed;
        }

        void view_InjectJobsButtonPressed(object sender, TEventArgs<string> e)
        {
            throw new NotImplementedException();
        }

        void view_ExtractJobsButtonPressed(object sender, TEventArgs<string> e)
        {
            var jobs = model.ExtractJobs(e.Data);
            View.ShowJobs(jobs);
        }

        protected override void ViewInitialized(object sender, EventArgs e)
        {
            //
        }
    }
}
