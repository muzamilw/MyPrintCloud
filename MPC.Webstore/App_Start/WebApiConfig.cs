using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace MPC.Webstore
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "APIControllers/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional, action = "Login" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApiWithoutController",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
               name: "defaultAttachmentUploader",
               routeTemplate: "api/{controller}/{id}/{name}/{ItemId}/{ContactId}/{CompanyId}",
               defaults: new { id = RouteParameter.Optional, name = RouteParameter.Optional, ItemId = RouteParameter.Optional, ContactId = RouteParameter.Optional, CompanyId = RouteParameter.Optional }
           );

            // addded by saqib to get json service in json from instead of xml 
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
        }
    }
}
