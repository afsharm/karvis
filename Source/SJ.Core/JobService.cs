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
            IList<string> tags = JobDao.GetAllTags();
            return GeneralHelper.AnalyseTags(tags);
        }
    }
}
