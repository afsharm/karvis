using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Karvis.Core;

namespace Karvis.Web
{
    public interface IJobListView : IView
    {
        event EventHandler<TEventArgs<string>> JobSelectedForDetail;
        IList<Job> SearchJobs(string title, string tag);
    }
}
