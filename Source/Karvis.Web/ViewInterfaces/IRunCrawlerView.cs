using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Karvis.Core;

namespace Karvis.Web
{
    public interface IRunCrawlerView : IView
    {
        event EventHandler<TEventArgs<string>> ExtractEmailsButtonPressed;
        event EventHandler<TEventArgs<string>> ExtractJobsButtonPressed;
        void ShowJobs(IList<string> list);
        void ShowEmails(IList<string> list);
    }
}
