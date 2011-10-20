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
using System.Timers;

namespace Karvis.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();

            //here we have not NHibernate session yet
            NHHelper.Instance.BeginRequest();
            KSingleton.Instance.ConfigTimer();
            NHHelper.Instance.EndRequest();
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