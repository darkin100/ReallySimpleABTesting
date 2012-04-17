using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Website.App;

namespace Website
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication, IContainerAccessor
    {

        private static IWindsorContainer _container;

        public static IWindsorContainer Container
        {
            get { return _container; }
        }

        #region IContainerAccessor Members

        IWindsorContainer IContainerAccessor.Container
        {
            get { return _container; }
        }

        #endregion

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        protected void Application_Start()
        {
            ConfigureWindsor();
            ConfigureMvc();

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        private void ConfigureMvc()
        {
            ControllerBuilder.Current.SetControllerFactory(Container.Resolve<IControllerFactory>());
        }

        private void ConfigureWindsor()
        {
            _container = new WindsorContainer();
            _container.Install(FromAssembly.This());
        }

        protected void Application_End(object sender, EventArgs e)
        {
            _container.Dispose();
        }

        protected void Application_BeginRequest()
        {
            var testname = ConfigurationManager.AppSettings["testname"];

            var httpCookie = base.Request.Cookies[testname];

            if (httpCookie == null)
            {
                HttpCookie cookie = new HttpCookie(testname);
                cookie.Value = Chance.FlipACoin();
                cookie.Expires = DateTime.Now.AddDays(2);
                base.Request.Cookies.Add(cookie);
                base.Response.Cookies.Add(cookie);
            }
        }

        protected void Application_EndRequest()
        {
            
        }
    }
}