using System;
using System.Collections.Generic;
namespace Karvis.Core
{
    public interface IFeedExtractor
    {
        List<Job> ExtractFeed(AdSource siteSource);
    }
}
