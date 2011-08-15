using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Karvis.Core;

namespace Karvis.Web
{
    public interface IAddEditJobView : IView
    {
        event EventHandler<TEventArgs<string>> JobSelectedForEdit;
        event EventHandler AddButtonPressed;
        void AddNewJob(string title, string description, string url, string tag);
        void EditJob(Job job);
        Job GetJob(int id);
        void ShowJob(Job job);
    }
}
