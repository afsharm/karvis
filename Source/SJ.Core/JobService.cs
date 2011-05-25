using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SJ.Core
{
    public class JobService
    {
        public static IDictionary<string, UInt16> ExtractTagCloud()
        {
            //todo
            return GeneralHelper.AnalyseTags(null);
        }
    }
}
