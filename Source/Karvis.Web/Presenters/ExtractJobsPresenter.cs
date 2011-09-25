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
            view.TempSaveButtonPressed += view_TempSaveButtonPressed;
            view.ViewInitialized += view_ViewInitialized;
        }

        void view_TempSaveButtonPressed(object sender, EventArgs e)
        {
            SaveJobs(false);
        }

        void view_ApplyJobsButtonPressed(object sender, EventArgs e)
        {
            View.DisableExtractButton();
            View.DisableApplyButton();
            View.DisableTempSaveButton();

            SaveJobs(true);
            View.CleaJobs();
        }

        void SaveJobs(bool isActive)
        {
            List<Job> jobs = View.ReadJobs();

            //is saved beforely in database or not
            bool isNew = true;
            foreach (var job in jobs)
                if (job.PreSavedJobId > 0)
                {
                    isNew = false;
                    break;
                }

            //validation
            foreach (var job in jobs)
            {
                if (isNew && job.PreSavedJobId > 0)
                    throw new ApplicationException("unexpected error is SaveJobs (isNew)");

                if (!isNew && job.PreSavedJobId < 1)
                    throw new ApplicationException("unexpected error is SaveJobs (!isNew)");
            }

            int count = jobModel.SaveOrUpdateJobBatch(jobs, AdSource.rahnama_com, isActive, isNew);

            string message =
                isActive ?
                    message = string.Format("{0} کار از سایت راهنما ثبت دائم شد.", count)
                :
                    message = string.Format("{0} کار از سایت راهنما به طور موقت ثبت شد.", count);

            View.ShowMessage(message);

        }
        void view_ExtractJobsButtonPressed(object sender, TEventArgs<string> e)
        {
            View.DisableExtractButton();

            AdSource siteSource = (AdSource)Enum.Parse(typeof(AdSource), e.Data);
            var jobs = extractJobsModel.ExtractJobs(siteSource);
            View.ShowMessage(string.Format("{0} تا کار استخراج شد", jobs.Count));
            if (jobs.Count > 0)
            {
                View.ShowJobs(jobs);
                View.EnableTempSaveButton();
                View.EnableApplyButton();
            }
        }

        void view_ViewInitialized(object sender, EventArgs e)
        {
            //extract temp jobs
            int jobCount = jobModel.FindNoneActiveCount(adSource: AdSource.rahnama_com);

            if (jobCount > 0)
            {
                //there are temp jobs

                View.DisableExtractButton();
                View.EnableTempSaveButton();
                View.EnableApplyButton();

                var jobs = jobModel.FindAll(string.Empty, string.Empty, AdSource.rahnama_com, false, string.Empty, int.MaxValue, 0);
                View.ShowJobs(jobs);
                View.ShowMessage(string.Format("{0} کار موقتی وجود دارد", jobCount));
            }
            else
            {
                //therer are not temp jobs saved 

                View.DisableTempSaveButton();
                View.DisableApplyButton();
            }
        }
    }
}
