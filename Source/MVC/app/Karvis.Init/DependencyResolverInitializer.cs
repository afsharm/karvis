using System.Web.Mvc;
using Karvis.Domain.JobExtract;
using Karvis.Domain.Tasks;
using Karvis.NHibernateProvider;
using Karvis.Tasks;
using Karvis.Tasks.JobExtract;
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
                                                  x.For<IAdSourceTask>().Use<AdSourceTask>();
                                                  x.For<ITagTask>().Use<TagTask>();
                                                  x.For<IAdminTask>().Use<AdminTask>();
                                                  x.For<IExtractJobs>().Use<ExtractJob>();
                                                  x.For<IExtractorHelper>().Use<ExtractorHelper>();
                                                  x.For<IKarvisCrawler>().Use<KarvisCrawler>();

                                                  x.For<IConfigDbTask>().Use<ConfigDbTask>();
                                                  x.For(typeof (IRepositoryWithTypedId<,>)).Use(
                                                      typeof (RepositoryWithTypedId<,>));
                                              });

            // ServiceLocator.SetLocatorProvider(() => new StructureMapServiceLocator(container));
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
        }
    }
}