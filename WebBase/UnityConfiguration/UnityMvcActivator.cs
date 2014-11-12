using System.Linq;
using System.Web.Mvc;
using MPC.WebBase.UnityConfiguration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using MPC.WebBase.UnityConfiguration;
using UnityDependencyResolver = MPC.WebBase.UnityConfiguration.UnityDependencyResolver;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(UnityWebActivator), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(UnityWebActivator), "Shutdown")]

namespace MPC.WebBase.UnityConfiguration
{
    /// <summary>Provides the bootstrapping for integrating Unity with ASP.NET MVC.</summary>
    public static class UnityWebActivator
    {
// ReSharper disable InconsistentNaming
        private static IUnityContainer container;
// ReSharper restore InconsistentNaming

        /// <summary>Integrates Unity when the application starts.</summary>
        public static void Start()
        {
            
            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(Container));

            DependencyResolver.SetResolver(new UnityDependencyResolver(Container));

            // TODO: Uncomment if you want to use PerRequestLifetimeManager
            DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));
        }

        /// <summary>Disposes the Unity container when the application is shut down.</summary>
        public static void Shutdown()
        {
            //var container = UnityConfig.GetConfiguredContainer();
            container.Dispose();
        }

        /// <summary>
        /// Container
        /// </summary>
        public static IUnityContainer Container
        {
            get { return container ?? (container = UnityConfig.GetConfiguredContainer()); }
        }
    }
}