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
        void ShowJobs(List<Job> jobs);
        List<Job> ReadJobs();
    }
}
