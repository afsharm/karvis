using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SJ.Core
{
    public class JobService
    {
        public static IOrderedEnumerable<KeyValuePair<string, ushort>> ExtractTagCloud()
        {
            IList<string> tags = JobDao.GetAllTags();
            return GeneralHelper.AnalyseTags(tags);
        }
    }
}
