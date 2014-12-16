﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;
using System.Web.Routing;

namespace MPC.Webstore
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               "Error",
               "Error/{controller}/{action}/{id}",
               new { controller = "Home", action = "Error", id = UrlParameter.Optional }
           );

            routes.MapRoute("store"
                 , "store/{name}"
                 , new { controller = "Domain", action = "Index", name = "" });

            
            routes.MapRoute(
                "Login",
                "Login/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
            "SignUp",
            "SignUp/{controller}/{action}/{id}",
            new { controller = "Home", action = "Index", id = UrlParameter.Optional }
              );

            routes.MapRoute(
          "ForgotPassword",
          "ForgotPassword/{controller}/{action}/{id}",
          new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
