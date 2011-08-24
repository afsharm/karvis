using System;
using System.Collections.Generic;
using Karvis.Core;
using NUnit.Framework;

namespace Karvis.Test
{

    [TestFixture]
    public class PersistenceTests : NHibernateFixture
    {

        [Test]
        public void Movie_cascades_save_to_ActorRole()
        {

            //Guid movieId;
            //Movie movie = new Movie()
            //{
            //  Name = "Mars Attacks",
            //  Description = "Sci-Fi Parody",
            //  Director = "Tim Burton",
            //  UnitPrice = 12M,
            //  Actors = new List<ActorRole>()
            //    {
            //      new ActorRole() {
            //        Actor = "Jack Nicholson",
            //        Role = "President James Dale"
            //      }
            //    }
            //};

            //using (var session = SessionFactory.OpenSession())
            //using (var tx = session.BeginTransaction())
            //{
            //  movieId = (Guid)session.Save(movie);
            //  tx.Commit();
            //}


            //using (var session = SessionFactory.OpenSession())
            //using (var tx = session.BeginTransaction())
            //{
            //  movie = session.Get<Movie>(movieId);
            //  tx.Commit();
            //}

            //Assert.That(movie.Actors.Count == 1);

        }

        [Test]
        public void ProductPersistenceTest()
        {

            //  Guid productId;
            //  Product product = new Product()
            //                      {
            //                        Name = "Stuff",
            //                        Description = "Cool",
            //                        UnitPrice = 100M
            //                      };

            //  using (var session = SessionFactory.OpenSession())
            //  using (var tx = session.BeginTransaction())
            //  {
            //    productId = (Guid)session.Save(product);
            //    tx.Commit();
            //  }

            //  using (var session = SessionFactory.OpenSession())
            //  using (var tx = session.BeginTransaction())
            //  {
            //    product = session.Get<Product>(productId);
            //    product.Name = "Junk";
            //    tx.Commit();
            //  }

        }

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
            JobModel model = new JobModel(SessionFactory);

            string title = Guid.NewGuid().ToString();
            int jobId = model.AddNewJob(title, string.Empty, string.Empty, string.Empty);

            Job retJob = model.GetJob(jobId);
            Assert.AreEqual(title, retJob.Title);
        }
    }
}

