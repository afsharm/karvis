using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Karvis.Core;
using System.Web.UI.HtmlControls;

namespace Karvis.Web
{
    public interface IExtractJobsView : IView
    {
        event EventHandler<TEventArgs<string>> ExtractJobsButtonPressed;
        event EventHandler ApplyJobsButtonPressed;
        event EventHandler TempSaveButtonPressed;
        void ShowJobs(IList<Job> jobs);
        void DisableExtractButton();
        void DisableApplyButton();
        void EnableApplyButton();
        void DisableTempSaveButton();
        void EnableTempSaveButton();
        List<Job> ReadJobs();
        void ClearJobs();
        List<string> ReadIgnoredJobs();

        AdSource GetSiteSource();

        void SetState(ExtractStatus extractStatus);

        ExtractStatus GetState();
    }
}
