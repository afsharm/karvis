using System;
using System.Collections.Generic;
using Karvis.Core;
using NUnit.Framework;

namespace Karvis.Test
{

    [TestFixture]
    public class JobModelTests : NHibernateFixture
    {


        [Test]
        public void JobPersistenceTest()
        {

            int jobId;
            Job job = new Job()
                                {
                                    Title = "title",
                                    Description = "Cool"
                                };

            using (var session = SessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                jobId = (int)session.Save(job);
                tx.Commit();
            }

            using (var session = SessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                job = session.Get<Job>(jobId);
                job.Title = "Junk";
                tx.Commit();
            }

        }

        [Test]
        public void TestJobModel()
        {
            IJobModel model = new JobModel(SessionFactory);

            string title = Guid.NewGuid().ToString();
            int jobId = model.AddNewJob(title, string.Empty, string.Empty, string.Empty, AdSource.Misc);

            Job retJob = model.GetJob(jobId);
            Assert.AreEqual(title, retJob.Title);
        }

        [Test]
        public void FindAllZero()
        {
            IJobModel model = new JobModel(SessionFactory);

            int count = model.FindAll(null, null, AdSource.All, true, null, int.MaxValue, 0).Count;
            Assert.AreEqual(0, count);
        }

        [Test]
        public void MassInjectionTest()
        {
            IJobModel model = new JobModel(SessionFactory);

            const int max = 300;
            for (int i = 0; i < max; i++)
            {
                model.AddNewJob(string.Empty, string.Empty, string.Empty, string.Empty, AdSource.Misc);
            }

            int count = 0;
            foreach (var item in model.GetAllJobs())
                count++;

            Assert.AreEqual(max, count);
        }

        [Test]
        public void AdSourceTest()
        {
            IJobModel model = new JobModel(SessionFactory);

            var job = new Job { AdSource = AdSource.Email };

            model.AddJob(job);

            var retJob = model.GetJob(job.Id);

            Assert.AreEqual(job.AdSource, retJob.AdSource);


            var job2 = new Job { AdSource = AdSource.irantalent_com };

            model.AddJob(job2);

            var retJob2 = model.GetJob(job2.Id);

            Assert.AreEqual(job2.AdSource, retJob2.AdSource);


        }
    }
}

