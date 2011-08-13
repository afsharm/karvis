using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Karvis.Core
{
    public interface IView
    {
        event EventHandler ViewInitialized;
   }
}
