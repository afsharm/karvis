using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SJ.Core
{
    public class TEventArgs<T> : EventArgs
    {
        public T Data { get; set; }

        public TEventArgs(T data)
        {
            Data = data;
        }
    }
}
