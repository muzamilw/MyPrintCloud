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

           // routes.MapRoute(
           //   "Error",
           //   "Error",
           //   new { controller = "Home", action = "Error", id = UrlParameter.Optional }
           //);

            routes.MapRoute("clear"
                 , "clear/{name}"
                 , new { controller = "Domain", action = "updateCache", name = "" });


            routes.MapRoute("store"
                 , "store/{name}"
                 , new { controller = "Domain", action = "Index", name = "" });

    

            routes.MapRoute(
             "AllProducts",
             "AllProducts",
             new { controller = "Home", action = "Index", id = UrlParameter.Optional }
          );
            routes.MapRoute(
             "RealEstateProducts",
             "RealEstateProducts/{listingId}",
             new { controller = "Home", action = "Index", listingId = "" }
          );
            routes.MapRoute(
             "RealEstateSmartForm",
             "RealEstateSmartForm/{listingId}/{itemId}",
             new { controller = "Home", action = "Index", listingId = "", itemId = "" }
          );
            routes.MapRoute(
            "Dashboard",
            "Dashboard",
            new { controller = "Home", action = "Index", id = UrlParameter.Optional }
         );

            routes.MapRoute(
            "About",
            "About/{controller}/{action}/{id}",
            new { controller = "Home", action = "About", id = UrlParameter.Optional }
         );

            routes.MapRoute(
             "ContactUs",
             "ContactUs",
             new { controller = "Home", action = "Index", id = UrlParameter.Optional }
          );
           
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
              "ProductOptions/{CategoryId}/{ItemId}/{ItemMode}/{TemplateId}",
              new { controller = "Home", action = "Index", CategoryId = UrlParameter.Optional, ItemId = UrlParameter.Optional, ItemMode = UrlParameter.Optional, TemplateId = UrlParameter.Optional }
             );
            routes.MapRoute(
              "ProductOptionCostCentre",
              "ProductOptions/GetDateTimeString",
              new { controller = "ProductOptions", action = "GetDateTimeString"}
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
               "Receipt",
               "Receipt/{OrderID}",
               new { controller = "Home", action = "Index", OrderID = UrlParameter.Optional}
                 );
         
                  routes.MapRoute(
           "ShopCartAddressSelect",
           "ShopCartAddressSelect/{OrderID}",
           new { controller = "Home", action = "Index", OrderID = UrlParameter.Optional }
             );

                  routes.MapRoute(
               "OrderConfirmation",
               "OrderConfirmation/{OrderID}",
               new { controller = "Home", action = "Index", OrderID = UrlParameter.Optional }
                 );
         

            routes.MapRoute(
         "pages",
         "pages/{name}/{PageID}",
         new { controller = "Home", action = "Index", name = UrlParameter.Optional, PageID = UrlParameter.Optional }
           );

            routes.MapRoute(
             "ProductDetail",
             "ProductDetail/{CategoryID}/{ItemID}/{TemplateID}/{TemplateName}/{Mode}",
             new { controller = "Home", action = "Index", CategoryID = UrlParameter.Optional, ItemID = UrlParameter.Optional, TemplateID = UrlParameter.Optional, TemplateName = UrlParameter.Optional, Mode = UrlParameter.Optional}
               );

            routes.MapRoute(
             "CloneItem",
             "CloneItem/{id}",
             new { controller = "Category", action = "CloneItem", id = UrlParameter.Optional }
               );
            routes.MapRoute(
            "EditDesign",
            "EditDesign/{DesignState}/{EditType}/{ItemID}/{TemplateId}",
            new { controller = "ProductDetail", action = "EditDesign", DesignState = UrlParameter.Optional, EditType = UrlParameter.Optional,ItemID = UrlParameter.Optional, TemplateId = UrlParameter.Optional }
              );
              routes.MapRoute(
            "RemoveProduct",
            "RemoveProduct/{ItemID}/{OrderID}",
            new { controller = "ShopCart", action = "RemoveProduct",ItemID = UrlParameter.Optional, OrderID = UrlParameter.Optional }
              );
      
            routes.MapRoute(
               "Default", // Route name
               "",        // URL with parameters
               new { controller = "Home", action = "Index", id = UrlParameter.Optional }  // Parameter defaults
           );
        }
    }
}
