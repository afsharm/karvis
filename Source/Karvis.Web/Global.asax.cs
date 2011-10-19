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
        Timer timer;
        IKScheduler scheduler;

        protected void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            ConfigureScheduler();
        }

        private void ConfigureScheduler()
        {
            scheduler = new KScheduler();
            timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Interval = 5 * 60 * 1000;
            timer.Start();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            scheduler.Trigger();
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