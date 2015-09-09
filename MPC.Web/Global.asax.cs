using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.Unity;
using MPC.MIS.App_Start;
using MPC.Models.LoggerModels;
using MPC.WebBase.UnityConfiguration;
using UnityDependencyResolver = MPC.WebBase.UnityConfiguration.UnityDependencyResolver;
using System.Web;
using MPC.WebBase.WebApi;


namespace MPC.MIS
{
    // Note: For instructions on enabling IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=301868
    public class MvcApplication : System.Web.HttpApplication
    {
        #region Private
        private static IUnityContainer container;
        /// <summary>
        /// Configure Logger
        /// </summary>
        private void ConfigureLogger()
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());
            IConfigurationSource configurationSource = ConfigurationSourceFactory.Create();
            LogWriterFactory logWriterFactory = new LogWriterFactory(configurationSource);
            Logger.SetLogWriter(logWriterFactory.Create());
            //IMPCLogger logger = container.Resolve<IMPCLogger>();
            //logger.Write("asfasf", "General", 1, -1, TraceEventType.Error);
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
            MPC.ExceptionHandling.TypeRegistrations.RegisterType(container);

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

        /// <summary>
        /// Change MVC Configuration
        /// </summary>
        private static void ChangeMvcConfiguration()
        {
            //ViewEngines.Engines.Clear();
            //ViewEngines.Engines.Add(new CustomRazorViewEngine());
        }

        #endregion
        protected void Application_Start()
        {
            RegisterIoC();
            ConfigureLogger();
            ChangeMvcConfiguration();
            AreaRegistration.RegisterAllAreas();

            BundleTable.EnableOptimizations = !HttpContext.Current.IsDebuggingEnabled;
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters, container);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Set MVC resolver
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            // Set Web Api resolver
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

    }
}