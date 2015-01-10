using System;
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



            routes.MapRoute("clear"
                 , "clear/{name}"
                 , new { controller = "Domain", action = "updateCache", name = "" });


            routes.MapRoute("store"
                 , "store/{name}"
                 , new { controller = "Domain", action = "Index", name = "" });


            routes.MapRoute(
                "Login",
                "Login/{controller}/{action}/{FirstName}/{LastName}/{Email}/{IsMarketing}",
                new { controller = "Home", action = "Index", FirstName = UrlParameter.Optional, LastName = UrlParameter.Optional, Email = UrlParameter.Optional, IsMarketing = UrlParameter.Optional }
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
        "Category",
        "Category/{name}/{id}",
        new { controller = "Home", action = "Index", id = UrlParameter.Optional }
          );
            routes.MapRoute(
  "ProductOptions",
  "ProductOptions/{CategoryId}/{ReferenceItemId}/{ItemMode}",
  new { controller = "Home", action = "Index", Cid = UrlParameter.Optional, Itemid = UrlParameter.Optional, Mode = UrlParameter.Optional }
    );
            routes.MapRoute(
     "MarketingBrief",
     "MarketingBrief/{ProductName}/{ItemID}",
     new { controller = "Home", action = "Index", ProductName = UrlParameter.Optional, ItemID = UrlParameter.Optional }
       );

            routes.MapRoute(
   "ShopCart",
   "ShopCart/{optionalOrderId}",
   new { controller = "Home", action = "Index", optionalOrderId = UrlParameter.Optional }
     );

            routes.MapRoute(
             "CloneItem",
             "CloneItem/{id}",
             new { controller = "Category", action = "CloneItem", id = UrlParameter.Optional }
               );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               "Error",
               "Error/{controller}/{action}/{id}",
               new { controller = "Home", action = "Error", id = UrlParameter.Optional }
            );
        }
    }
}
