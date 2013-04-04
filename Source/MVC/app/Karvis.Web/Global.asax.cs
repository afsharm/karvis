using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Karvis.Init;
using Karvis.Web.Controllers.Controllers;
using Razmyar.Web.Controllers.Help;
using Razmyar.Web.Controllers.Home;
using Razmyar.Web.Controllers.Settings;
using SharpLite.Web.Mvc.ModelBinder;


namespace Karvis.Web
{
    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                new[]
                    {
                        typeof (JobsController).Namespace,
                        typeof (ConfigDbController).Namespace,
                        typeof (AccountController).Namespace,
                        //typeof (UsersController).Namespace,
                        typeof (AboutController).Namespace,
                        typeof (JobController).Namespace
                    });
        }

        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();

            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            DependencyResolverInitializer.Initialize();

            ModelBinders.Binders.DefaultBinder = new SharpModelBinder();
        }
    }
}