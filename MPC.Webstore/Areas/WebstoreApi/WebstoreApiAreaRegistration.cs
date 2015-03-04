﻿using System.Web.Mvc;
using System.Web.Http;

namespace MPC.Webstore.Areas.WebstoreApi
{
    public class WebstoreApiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WebstoreApi";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
          
            context.Routes.MapHttpRoute(
              name: "WebstoreApi_default",
              routeTemplate: AreaName + "/{controller}/{action}/{id}",
              defaults: new { id = UrlParameter.Optional }
           );
            context.Routes.MapHttpRoute(
             name: "WebstoreApiDefault_MultiParams",
             routeTemplate: AreaName + "/{controller}/{action}/{parameter1}/{parameter2}/{parameter3}/{parameter4}/{parameter5}/{parameter6}/{parameter7}/{parameter8}/{parameter9}",
             defaults: new { parameter1 = RouteParameter.Optional, parameter2 = RouteParameter.Optional, parameter3 = RouteParameter.Optional, parameter4 = RouteParameter.Optional, parameter5 = RouteParameter.Optional, parameter6 = RouteParameter.Optional, parameter7 = RouteParameter.Optional, parameter8 = RouteParameter.Optional, parameter9 = RouteParameter.Optional }
          );

        }
    }
}