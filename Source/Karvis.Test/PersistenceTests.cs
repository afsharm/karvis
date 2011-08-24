using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Karvis.Test
{
    [TestFixture]
    public class PersistenceTests : NHibernateFixture
    {
        [Test]
        public void Movie_cascades_save_to_ActorRole()
        {
            //      Guid movieId;
            //      Movie movie = new Movie()
            //      {
            //          Name = "Mars Attacks",
            //          Description = "Sci-Fi Parody",
            //          Director = "Tim Burton",
            //          UnitPrice = 12M,
            //          Actors = new List<ActorRole>()
            //{
            //  new ActorRole() {
            //    Actor = "Jack Nicholson",
            //    Role = "President James Dale"
            //  }
            //}
            //      };



            //      using (var session = SessionFactory.OpenSession())


            //      using (var tx = session.BeginTransaction())
            //      {
            //          movieId = (Guid)session.Save(movie);
            //          tx.Commit();
            //      }
            //      using (var session = SessionFactory.OpenSession())
            //      using (var tx = session.BeginTransaction())
            //      {
            //          movie = session.Get<Movie>(movieId);
            //          tx.Commit();
            //      }
            //      Assert.That(movie.Actors.Count == 1);
        }
    }
}
