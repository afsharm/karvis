using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Karvis.Domain.JobExtract
{
    public interface IFeedExtractor
    {
        List<Job> ExtractFeed(AdSource siteSource);
    }
}
