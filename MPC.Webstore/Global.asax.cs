using System;
using System.Globalization;
using System.Security.Policy;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.Unity;
using MPC.Implementation.WebStoreServices;
using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using MPC.WebBase.UnityConfiguration;
using UnityDependencyResolver = MPC.WebBase.UnityConfiguration.UnityDependencyResolver;
using System.Web;
using MPC.Interfaces.WebStoreServices;
using System.Threading;

namespace MPC.Webstore
{
    public class MvcApplication : System.Web.HttpApplication
    {


        #region Private
        private static IUnityContainer container;
        private ICompanyService companyService;

        /// <summary>
        /// Configure Logger
        /// </summary>
        private void ConfigureLogger()
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());
            IConfigurationSource configurationSource = ConfigurationSourceFactory.Create();
            LogWriterFactory logWriterFactory = new LogWriterFactory(configurationSource);
            // Logger.SetLogWriter(logWriterFactory.Create());
        }
        /// <summary>
        /// Create the unity container
        /// </summary>
        private static IUnityContainer CreateUnityContainer()
        {
            container = UnityWebActivator.Container;
            RegisterTypes();

            return container;
        }
        /// <summary>
        /// Register types with the IoC
        /// </summary>
        private static void RegisterTypes()
        {
            MPC.WebBase.TypeRegistrations.RegisterTypes(container);
            MPC.Implementation.TypeRegistrations.RegisterType(container);

        }
        /// <summary>
        /// Register unity 
        /// </summary>
        private static void RegisterIoC()
        {
            if (container == null)
            {
                container = CreateUnityContainer();
            }
        }
        #endregion
        protected void Application_Start()
        {

            RegisterIoC();
            ConfigureLogger();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters, container);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Set MVC resolver
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            // Set Web Api resolver
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);


            //AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);



            //Commented By Naveed
            /*  RegisterIoC();
              AreaRegistration.RegisterAllAreas();
            //  ConfigureLogger();

              FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters, container);
              RouteConfig.RegisterRoutes(RouteTable.Routes);
              BundleConfig.RegisterBundles(BundleTable.Bundles);

              // Set MVC resolver
              //DependencyResolver.SetResolver(new UnityDependencyResolver(container));
              DependencyResolver.SetResolver(new UnityDependencyResolver(container));
              // Set Web Api resolver
              //GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
              GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);*/
        }

        //public override void Init()
        //{
        //    this.PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
        //    base.Init();
        //}

        //void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        //{
        //    System.Web.HttpContext.Current.SetSessionStateBehavior(
        //        SessionStateBehavior.Required);
        //}
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            //It's important to check whether session object is ready
            if (HttpContext.Current.Session != null)
            {
                CultureInfo ci = (CultureInfo)this.Session["Culture"];
                //Checking first if there is no value in session 
                //and set default language 
                //this can happen for first user's request
                if (ci == null)
                {
                    //Sets default culture to english invariant
                    string langName = "fr-FR";
                    //string langName = "en-US";
                    //Try to get values from Accept lang HTTP header
                    if (HttpContext.Current.Request.UserLanguages != null &&
                       HttpContext.Current.Request.UserLanguages.Length != 0)
                    {
                        //Gets accepted list 
                        //langName = HttpContext.Current.Request.UserLanguages[0].Substring(0, 2);
                    }
                    ci = new CultureInfo(langName);
                    this.Session["Culture"] = ci;
                }
                //Finally setting culture for each request
                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            }
        }
        protected void Session_Start()
        {
            companyService = container.Resolve<ICompanyService>();

            string url = Convert.ToString(HttpContext.Current.Request.Url.DnsSafeHost);
            long storeid = companyService.GetCompanyIdByDomain(url);

            if (storeid == 0)
            {
                Response.Redirect("/Home/About");
            }
            else
            {
                Session["storeId"] = storeid;

            }
        }
    }
}
