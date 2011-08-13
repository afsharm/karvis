using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using log4net.Config;
using System.IO;
using Karvis.Core;
using NHibernate.Context;
using NHibernate;
using NHibernate.Cfg;
using Castle.Windsor;
using Castle.Core;
using Castle.MicroKernel.Registration;

namespace Karvis.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            ConfigureIoC();
        }

        private static void ConfigureIoC()
        {
            //IWindsorContainer localContainer = new WindsorContainer();
            //localContainer.Register(Component.For<IJobModel>().ActAs(typeof(JobModel), LifestyleType.Transient));
            //IoC.RegisterResolver(new WindsorDependencyResolver(localContainer));
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            NHHelper.Instance.BeginRequest();
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            NHHelper.Instance.EndRequest();
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}