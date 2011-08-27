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

        public ExtractJobsPresenter(IExtractJobsView view, IExtractJobsModel aModel)
            : base(view)
        {
            this.model = aModel;
            //view.JobSelectedForDisplay += view_JobSelectedForDisplay;
        }

        protected override void ViewInitialized(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
