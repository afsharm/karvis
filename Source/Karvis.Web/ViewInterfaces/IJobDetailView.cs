using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Karvis.Core;

namespace Karvis.Web
{
    public interface IJobDetailView : IView
    {
        event EventHandler<TEventArgs<string>> JobSelectedForDisplay;
        void ShowJob(Job job);
    }
}
