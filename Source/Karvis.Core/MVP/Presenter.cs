using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Karvis.Core
{
    public abstract class Presenter<TView> where TView : IView
    {
        private TView view;
        protected virtual TView View
        {
            get { return view; }
            set { view = value; }
        }

        protected Presenter(TView view)
        {
            this.view = view;
        }
    }
}
