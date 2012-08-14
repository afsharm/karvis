using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Karvis.Core;


namespace Karvis.Web
{
    public class SubmitJobPresenter : Presenter<ISubmitJobView>
    {
        private readonly IJobModel jobModel;

        public SubmitJobPresenter(ISubmitJobView view)
            : this(view, IoC.Resolve<IJobModel>())
        {
        }

        public SubmitJobPresenter(ISubmitJobView view, IJobModel jobModel)
            : base(view)
        {
            this.jobModel = jobModel;

            view.EditButtonPressed += view_EditButtonPressed;
            view.NewButtonPressed += view_NewButtonPressed;
            view.SaveUpdateButtonPressed += view_SaveUpdateButtonPressed;
            view.ViewInitialized += view_ViewInitialized;
        }

        void CheckValidation()
        {
            bool isUserAuthorized = View.IsUserAuthorized();

            if (!isUserAuthorized)
            {
                View.ForbidPage();
                View.ShowMessage("شما دسترسی کافی به این صفحه ندارید");
            }
        }

        void view_ViewInitialized(object sender, EventArgs e)
        {
            if (!View.IsUserAuthorized())
                View.DisableAutorizedSections();

            View.Clear();

            bool isInEditMode = View.IsInEditMode();

            View.DisableEditButton();

            if (View.IsInEditMode())
            {
                View.EnableSaveButton();
                View.UnFreez();

                CheckValidation();

                Job job = jobModel.GetJob(View.GetEditingJobId());
                View.ShowJob(job);
            }
            else
            {
                View.DisableNewButton();
            }
        }

        void view_SaveUpdateButtonPressed(object sender, EventArgs e)
        {
            Job job = View.ReadJob();

            bool isNew = job.PreSavedJobId == 0;
            string message = string.Empty;

            if (isNew)
            {
                //new
                jobModel.AddJob(job);
                job.PreSavedJobId = job.Id;
                View.ShowJob(job);
                message = "آگهی جدید ثبت شد";
            }
            else
            {
                //update
                Job loadedJob = jobModel.GetJob(job.PreSavedJobId);

                loadedJob.Description = job.Description;
                loadedJob.Tag = job.Tag;
                loadedJob.Title = job.Title;
                loadedJob.Url = job.Url;
                loadedJob.IsActive = job.IsActive;
                loadedJob.AdSource = job.AdSource;

                jobModel.SaveOrUpdateJob(loadedJob);
                View.ShowJob(loadedJob);
                message = "به روز رسانی شد";
            }

            View.EnableEditButton();
            View.EnableNewButton();
            View.Freez();
            View.ShowMessage(message);
        }

        void view_NewButtonPressed(object sender, EventArgs e)
        {
            View.Clear();
            View.DisableEditButton();
        }

        void view_EditButtonPressed(object sender, EventArgs e)
        {
            CheckValidation();
            View.ShowMessage(string.Empty);
            View.EnableSaveButton();
            View.DisableEditButton();
            View.UnFreez();
        }
    }
}