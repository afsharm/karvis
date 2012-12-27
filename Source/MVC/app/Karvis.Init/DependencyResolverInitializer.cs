using System.Web.Mvc;
using Karvis.Domain.Tasks;
using Karvis.NHibernateProvider;
using Karvis.Tasks;
using NHibernate;
using NHibernate.Cfg;
using Razmyar.Domain.Contracts.Repositories;
using Razmyar.Domain.Contracts.Tasks;
using Razmyar.SharpLite.Repositories;
using Razmyar.SharpLite.Tasks;
using SharpLite.Domain.DataInterfaces;
using SharpLite.NHibernateProvider;
using StructureMap;

namespace Karvis.Init
{
    public class DependencyResolverInitializer
    {
        public static void Initialize()
        {
            var container = new Container(x =>
                                              {
                                                  x.For<ISessionFactory>()
                                                      .Singleton()
                                                      .Use(
                                                          () => NHibernateInitializer.Initialize().BuildSessionFactory());
                                                  x.For<Configuration>() //needed for Settings/ConfigDb
                                                      .Singleton()
                                                      .Use(
                                                          NHibernateInitializer.Initialize);
                                                  x.For<IEntityDuplicateChecker>().Use<EntityDuplicateChecker>();
                                                  x.For(typeof (IRepository<>)).Use(typeof (Repository<>));
                                                  x.For(typeof (ICrudTask<>)).Use(typeof (CrudTask<>));
                                                  x.For<IUserRepository>().Use<UserRepository>();
                                                  x.For<IUserTask>().Use<UserTask>();
                                                  x.For<IJobTask>().Use<JobTask>();
                                                  x.For<ISearchTask>().Use<SearchTask>();

                                                  x.For<IConfigDbTask>().Use<ConfigDbTask>();
                                                  x.For(typeof (IRepositoryWithTypedId<,>)).Use(
                                                      typeof (RepositoryWithTypedId<,>));
                                              });

            // ServiceLocator.SetLocatorProvider(() => new StructureMapServiceLocator(container));
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
        }
    }
}