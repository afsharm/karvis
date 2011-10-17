using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Karvis.Core;
using System.Text.RegularExpressions;
using Fardis;

namespace Karvis.Test
{
    [TestFixture]
    public class ExtractHelperTest
    {
        IKarvisCrawler karvisCrawler;
        IDateTimeHelper dateTimeHelper;
        IJobModel jobModel;
        IgnoredJobModel ignoredJobModel;
        IExtractorHelper extractorHelper;
        IExtractJobsModel extractJobsModel;

        [SetUp]
        public void SetupTest()
        {
            karvisCrawler = new KarvisCrawler();
            dateTimeHelper = new DateTimeHelper();
            jobModel = new JobModel();
            ignoredJobModel = new IgnoredJobModel();
            extractorHelper = new ExtractorHelper(jobModel, ignoredJobModel, karvisCrawler);
        }

        [Test]
        public void ExtractEmails()
        {
            //extractorHelper.ExtractTags(
        }
    }
}