using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SJ.Core;


namespace SJ.Web
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
        }
    }
}
