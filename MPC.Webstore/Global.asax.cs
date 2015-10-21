using System;
using System.Globalization;
using System.Net;
using System.Security.Claims;
using System.Security.Policy;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.Unity;
using MPC.Implementation.WebStoreServices;
using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using MPC.WebBase.UnityConfiguration;
using MPC.Webstore.Common;
using MPC.Webstore.ModelMappers;
using MPC.Webstore.ResponseModels;
using UnityDependencyResolver = MPC.WebBase.UnityConfiguration.UnityDependencyResolver;
using System.Web;
using MPC.Interfaces.WebStoreServices;
using System.Threading;
using FluentScheduler;
using System.Collections.Generic;
using System.Runtime.Caching;
using MPC.Webstore.Controllers;
namespace MPC.Webstore
{
    public class MvcApplication : System.Web.HttpApplication
    {


        #region Private
        private static IUnityContainer container;
        private ICompanyService companyService;
        private ICampaignService campaignService;
        private IUserManagerService userManagerService;
        /// <summary>
        /// Configure Logger
        /// </summary>
        private void ConfigureLogger()
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());
            IConfigurationSource configurationSource = ConfigurationSourceFactory.Create();
            LogWriterFactory logWriterFactory = new LogWriterFactory(configurationSource);
          //  Logger.SetLogWriter(logWriterFactory.Create());
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
        #endregion
        protected void Application_Start()
        {
            RegisterIoC();
            ConfigureLogger();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters, container);
          //  GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Set MVC resolver
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            // Set Web Api resolver
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
            campaignService = container.Resolve<ICampaignService>();
            TaskManager.Initialize(new EmailBackgroundTask(HttpContext.Current, campaignService));
            //AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);

             

            //Commented By Naveeds
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
      
        protected void Session_Start()
        {
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;
            Uri sdsd = Request.Url;
            if (UserCookieManager.WBStoreId == 0 || (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>) == null)
            {
                Uri urlReferrer = Request.UrlReferrer;
                
                companyService = container.Resolve<ICompanyService>();

                string url = Convert.ToString(HttpContext.Current.Request.Url.DnsSafeHost);

                long storeId = companyService.GetStoreIdFromDomain(url);

                if (storeId > 0)
                {
                    MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = null;
                    if ((cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>) != null && (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>).ContainsKey(storeId))
                    {
                        StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[storeId];
                    }
                    else
                    {
                        StoreBaseResopnse = companyService.GetStoreFromCache(storeId);
                    }

                    if (StoreBaseResopnse.Company != null)
                    {
                        UserCookieManager.WBStoreId = StoreBaseResopnse.Company.CompanyId;
                        UserCookieManager.WEBStoreMode = StoreBaseResopnse.Company.IsCustomer;
                        UserCookieManager.isIncludeTax = StoreBaseResopnse.Company.isIncludeVAT ?? false;
                        UserCookieManager.TaxRate = StoreBaseResopnse.Company.TaxRate ?? 0;
                        UserCookieManager.WEBOrganisationID = StoreBaseResopnse.Company.OrganisationId ?? 0;
                        //UserCookieManager.OrganisationLanguageIdentifier = "_" + UserCookieManager.OrganisationID.ToString();
                        // set global language of store

                        string languageName =
                            companyService.GetUiCulture(Convert.ToInt64(StoreBaseResopnse.Company.OrganisationId));

                        CultureInfo ci = null;

                        if (string.IsNullOrEmpty(languageName))
                        {
                            languageName = "en-US";
                        }

                        ci = new CultureInfo(languageName);

                        Thread.CurrentThread.CurrentUICulture = ci;
                        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);

                        // checking autologin if auto login then do not redirect as he redirection will be handle by the auto login action
                        if(!Request.Url.AbsolutePath.ToLower().Contains("autologin"))
                        {
                            if (StoreBaseResopnse.Company.IsCustomer == 3) // corporate customer
                            {
                                Response.Redirect("/Login");
                            }
                        }
                    }
                }
            }
        }

        private void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();

            var httpContext = ((HttpApplication)sender).Context;
            string url = httpContext.Request.Path;
            if (httpContext.Request.Url != null)
            {
                url = httpContext.Request.Url.AbsoluteUri;
            }
            httpContext.Response.Clear();
            httpContext.ClearError();
            ExecuteErrorController(httpContext, exception, url);
        }
        private void ExecuteErrorController(HttpContext httpContext, Exception exception, string url)
        {
            var routeData = new RouteData();
            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = "Index";
            routeData.Values["errorType"] = 10; //this is your error code. Can this be retrieved from your error controller instead?
            routeData.Values["exception"] = exception;
            routeData.Values["url"] = url;
            userManagerService = container.Resolve<IUserManagerService>();
            companyService = container.Resolve<ICompanyService>();
            campaignService = container.Resolve<ICampaignService>();
            using (Controller controller = new ErrorController(companyService, campaignService, userManagerService))
            {
                ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
            }
        }
    }
}
