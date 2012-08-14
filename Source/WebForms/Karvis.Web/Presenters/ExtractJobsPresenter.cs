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
        private readonly IIgnoredJobModel ignoredJobModel;
        private readonly IFeedExtractor feedExtractor;

        const int dayLimit = 14;
        const int recordLimit = 100;

        public ExtractJobsPresenter(IExtractJobsView view)
            : this(view, IoC.Resolve<IExtractJobsModel>(), IoC.Resolve<IJobModel>(),
            IoC.Resolve<IIgnoredJobModel>(), IoC.Resolve<IFeedExtractor>())
        {
        }

        public ExtractJobsPresenter(IExtractJobsView view, IExtractJobsModel extractJobsModel,
            IJobModel jobModel, IIgnoredJobModel ignoredJobModel, IFeedExtractor feedExtractor)
            : base(view)
        {
            this.extractJobsModel = extractJobsModel;
            this.jobModel = jobModel;
            this.ignoredJobModel = ignoredJobModel;
            this.feedExtractor = feedExtractor;

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
            View.ClearJobs();
        }

        void SaveJobs(bool isActive)
        {
            List<Job> jobs = View.ReadJobs();

            ExtractStatus extractStatus = View.GetState();

            //validation
            foreach (var job in jobs)
            {
                if (extractStatus == ExtractStatus.New && job.PreSavedJobId > 0)
                    throw new ApplicationException("unexpected error is SaveJobs (isNew)");

                if (extractStatus != ExtractStatus.New && job.PreSavedJobId < 1)
                    throw new ApplicationException("unexpected error is SaveJobs (!isNew)");
            }

            int count = jobModel.SaveOrUpdateJobBatch(jobs, isActive, extractStatus);
            List<string> ignoreJobUrls = View.ReadIgnoredJobs();
            AdSource siteSource = View.GetSiteSource();
            ignoredJobModel.AddBatchIgnoreJobUrls(siteSource, ignoreJobUrls);

            if (!isActive)
                View.SetState(ExtractStatus.TempLoad);

            string message =
                isActive ?
                    message = string.Format("{0} کار ثبت دائم شد.", count)
                :
                    message = string.Format("{0} کار به طور موقت ثبت شد.", count);


            View.ShowMessage(message);

            var loadedJobs = jobModel.FindAll(string.Empty, string.Empty, View.GetSiteSource(),
                false, "Id", int.MaxValue, 0);

            if (loadedJobs.Count > 0)
                View.ShowJobs(loadedJobs);
        }

        void view_ExtractJobsButtonPressed(object sender, TEventArgs<string> e)
        {
            View.DisableExtractButton();

            AdSource siteSource = (AdSource)Enum.Parse(typeof(AdSource), e.Data);

            List<Job> jobs = null;

            switch (siteSource)
            {
                case AdSource.rahnama_com:
                case AdSource.irantalent_com:
                case AdSource.Email:
                case AdSource.Misc:
                case AdSource.All:
                case AdSource.karvis_ir:
                case AdSource.itjobs_ir:
                case AdSource.agahi_ir:
                case AdSource.istgah_com:
                case AdSource.nofa_ir:
                case AdSource.unp_ir:
                    jobs = extractJobsModel.ExtractJobs(siteSource, dayLimit, recordLimit);
                    break;
                case AdSource.developercenter_ir:
                case AdSource.banki_ir:
                case AdSource.barnamenevis_org:
                case AdSource.estekhtam_com:
                    jobs = feedExtractor.ExtractFeed(siteSource);
                    break;
                default:
                    throw new ApplicationException("unkown error");
            }

            string message = jobs == null ? "هیچ چیز پیدا نشد" : string.Format("{0} تا کار استخراج شد", jobs.Count);
            View.ShowMessage(message);

            if (jobs != null && jobs.Count > 0)
            {
                View.ShowJobs(jobs);
                View.EnableTempSaveButton();
                View.EnableApplyButton();
            }
        }

        void view_ViewInitialized(object sender, EventArgs e)
        {
            //extract temp jobs
            int jobCount = jobModel.FindNoneActiveCount(View.GetSiteSource());

            if (jobCount > 0)
            {
                //there are temp jobs

                View.DisableExtractButton();
                View.EnableTempSaveButton();
                View.EnableApplyButton();
                View.SetState(ExtractStatus.TempLoad);

                var jobs = jobModel.FindAll(string.Empty, string.Empty, View.GetSiteSource(),
                    false, "Id", int.MaxValue, 0);

                View.ShowJobs(jobs);
                View.ShowMessage(string.Format("{0} کار موقتی وجود دارد", jobCount));
            }
            else
            {
                //therer are not temp jobs saved 

                View.SetState(ExtractStatus.New);

                View.DisableTempSaveButton();
                View.DisableApplyButton();
            }
        }
    }
}
