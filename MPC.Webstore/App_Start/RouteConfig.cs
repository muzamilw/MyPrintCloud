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

            routes.MapRoute(
              "Error",
              "Error",
              new { controller = "Home", action = "Error", Message = UrlParameter.Optional }
           );

            routes.MapRoute(
                 "AllClear",
                 "AllClear",
                 new { controller = "Domain", action = "ClearCacheObject", Message = UrlParameter.Optional }
            );

            routes.MapRoute(
                "NotFound",
                "NotFound",
                new { controller = "Home", action = "NotFound", Message = UrlParameter.Optional }
            );

            routes.MapRoute("clear"
                 , "clear/{StoreId}"
                 , new { controller = "Domain", action = "ClearCache", StoreId = UrlParameter.Optional 
            });


            routes.MapRoute(
                "Orderhistory"
              , "ProductOrderHistory"
              , new { controller = "Home", action = "Index", name = "" 
            });

             routes.MapRoute(
                "UserManager"
              , "UserManager"
              , new { controller = "Home", action = "Index", name = "" 
            });

            routes.MapRoute(
                "BillingShippingAddressManager"
              , "BillingShippingAddressManager"
              , new { controller = "Home", action = "Index", name = "" 
            });

            routes.MapRoute(
                "BillingShipping",
                "BillingShippingAddressManager/FillAddresses",
                new { controller = "BillingShippingAddressManager", action = "FillAddresses", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                "Logoutrealestate",
                "RealestateSideBarWithSlider/LogOut",
                new { controller = "RealestateSideBarWithSlider", action = "LogOut", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               "loadaddWithTerritory",
               "UserManager/GetAddressesByTerritorID",
               new { controller = "UserManager", action = "GetAddressesByTerritorID", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               "loadaddContactsIntellicence",
               "UserManager/GetContactsIntellicence",
               new { controller = "UserManager", action = "GetContactsIntellicence", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              "GetCompanyFlags",
              "UserManager/GetCompanyFlags",
              new { controller = "UserManager", action = "GetCompanyFlags", id = UrlParameter.Optional }
          );
            routes.MapRoute(
                "BillingShippingIntellisenceData",
                "BillingShippingAddressManager/IntellisenceData",
                new { controller = "BillingShippingAddressManager", action = "IntellisenceData", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               "loadSystemUserData",
               "UserManager/UserProfileData",
               new { controller = "UserManager", action = "UserProfileData", id = UrlParameter.Optional }
           );
            routes.MapRoute(
             "loadSystemUserDataAddress",
             "UserManager/getAddress",
             new { controller = "UserManager", action = "getAddress", id = UrlParameter.Optional }
         );

            routes.MapRoute(
                "OxfordTop",
                "OxfordTopLevelCategories/SubmitSubscribeData",
                new { controller = "OxfordTopLevelCategories", action = "SubmitSubscribeData", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                "metroCatggggggg",
                "MetroTestimonial/SubmitTestimonialData",
                new { controller = "MetroTestimonial", action = "SubmitTestimonialData", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                            "logdoggg",
                            "BlackAndWhiteHeaderMenu/LogOut",
                            new { controller = "BlackAndWhiteHeaderMenu", action = "LogOut", id = UrlParameter.Optional }
                        );
            routes.MapRoute(
                "QuickCAlculatorProducts",
                "QuickCalculator/GetAllProducts",
                new { controller = "QuickCalculator", action = "GetAllProducts", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "QuickCAlculatorPrises",
                "QuickCalculator/GetQuantityPrises",
                new { controller = "QuickCalculator", action = "GetQuantityPrises", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              "BillingShippingDisplaySearchedData",
              "BillingShippingAddressManager/DisplaySearchedData",
              new { controller = "BillingShippingAddressManager", action = "DisplaySearchedData", id = UrlParameter.Optional }
            );

            routes.MapRoute("store"
                 , "store/{name}"
                 , new { controller = "Domain", action = "Index", name = "" 
            });

            routes.MapRoute(
                "ContactDetail",
                "ContactDetail",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "RequestQuote",
                "RequestQuote",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                "ProductPendingOrders",
                "ProductPendingOrders",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "orderPolicy",
                "PersonalDetailAndOrderPolicy/SaveOrderPolicy",
                new { controller = "PersonalDetailAndOrderPolicy", action = "SaveOrderPolicy", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "ApproverOrder",
                "ProductPendingOrders/Save",
                new { controller = "ProductPendingOrders", action = "Save", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "RejectOrder",
                "ProductPendingOrders/ApporRejectOrder",
                new { controller = "ProductPendingOrders", action = "ApporRejectOrder", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "DashboardGePassword",
                "Dashboard/GetPassWord",
                new { controller = "Dashboard", action = "GetPassWord", id = UrlParameter.Optional }
            );
            
            routes.MapRoute(
                "ContryList",
                "BillingShippingAddressManager/LoadCountriesList",
                new { controller = "BillingShippingAddressManager", action = "LoadCountriesList", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "UpdateContactDetails",
                "ContactDetail/Update",
                new { controller = "ContactDetail", action = "Update", id = UrlParameter.Optional }
             );


            routes.MapRoute(
                  "StateList",
                  "BillingShippingAddressManager/LoadAllStates",
                  new { controller = "BillingShippingAddressManager", action = "LoadAllStates", id = UrlParameter.Optional }
             );

            routes.MapRoute(
                 "LoadStatesByCountryID",
                 "BillingShippingAddressManager/LoadStatesByCountryID",
                 new { controller = "BillingShippingAddressManager", action = "LoadStatesByCountryID", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                    "UpdateAddress",
                    "BillingShippingAddressManager/UpdateAddress",
                    new { controller = "BillingShippingAddressManager", action = "UpdateAddress", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "AddAddress",
                "BillingShippingAddressManager/AddNewAddress",
                new { controller = "BillingShippingAddressManager", action = "AddNewAddress", id = UrlParameter.Optional }
             );

            routes.MapRoute(
                "RebindGrid",
                "BillingShippingAddressManager/RebindGrid",
                new { controller = "BillingShippingAddressManager", action = "RebindGrid", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "orderview",
                "ProductOrderHistory/OrderResult",
                new { controller = "ProductOrderHistory", action = "OrderResult", id = UrlParameter.Optional }
            );


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
                new { controller = "ProductOptions", action = "GetDateTimeString" }
             );
            routes.MapRoute(
                "MarketingBrief",
                "MarketingBrief/{ProductName}/{ItemID}/{CategoryId}",
                new { controller = "Home", action = "Index", ProductName = UrlParameter.Optional, ItemID = UrlParameter.Optional, CategoryId = UrlParameter.Optional }
            );

            routes.MapRoute(
                "ShopCart",
                "ShopCart",
                new { controller = "Home", action = "Index"}
            );

            routes.MapRoute(
               "Receipt",
               "Receipt/{OrderID}",
               new { controller = "Home", action = "Index", OrderID = UrlParameter.Optional }
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
                "ProductDetail/{ProductName}/{CategoryID}/{ItemID}/{TemplateID}/{TemplateName}/{Mode}",
                 new { controller = "Home", action = "Index", ProductName = UrlParameter.Optional, CategoryID = UrlParameter.Optional, ItemID = UrlParameter.Optional, TemplateID = UrlParameter.Optional, TemplateName = UrlParameter.Optional, Mode = UrlParameter.Optional }
            );

            routes.MapRoute(
                "CloneFeatureItem",
                "CloneFeatureItem/{id}",
                new { controller = "FeaturedProductCarousal", action = "CloneFeatureItem", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "CloneProducts",
                "CloneProducts/{id}",
                new { controller = "CategoriesAndProducts", action = "CloneProducts", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "CloneItem",
                "CloneItem/{id}",
                new { controller = "Category", action = "CloneItem", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "CloneProductDetail",
                "CloneItem/{id}",
                new { controller = "ProductDetail", action = "CloneItem", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Dashboard/ResetPassword",
                "Dashboard/ResetPassword/{CurrentPassword}/{NewPassword}",
                new { controller = "Dashboard", action = "ResetPassword", CurrentPassword = UrlParameter.Optional, NewPassword = UrlParameter.Optional }
            );

            routes.MapRoute(
                "EditDesign",
                "EditDesign/{DesignState}/{EditType}/{ItemID}/{TemplateId}",
                new { controller = "ProductDetail", action = "EditDesign", DesignState = UrlParameter.Optional, EditType = UrlParameter.Optional, ItemID = UrlParameter.Optional, TemplateId = UrlParameter.Optional }
            );

            routes.MapRoute(
                "RemoveProduct",
                "RemoveProduct/{ItemID}/{OrderID}",
                new { controller = "ShopCart", action = "RemoveProduct", ItemID = UrlParameter.Optional, OrderID = UrlParameter.Optional }
            );

            routes.MapRoute(
                "RemoveSaveDesign",
                "RemoveSaveDesign/{ItemID}",
                new { controller = "SavedDesigns", action = "RemoveSaveDesign", ItemID = UrlParameter.Optional }
            );

            routes.MapRoute(
                "SavedDesigns",
                "SavedDesigns",
                new { controller = "Home", action = "Index" }
             );

            routes.MapRoute(
                "ReOrder",
                "ReOrder/{ItemID}",
                new { controller = "SavedDesigns", action = "ReOrder", ItemID = UrlParameter.Optional }
            );

            routes.MapRoute(
                "PaypalSubmit",
                "PaypalSubmit/{controller}/{action}/{id}",
                new { controller = "Payment", action = "PaypalSubmit", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                "NabSubmit",
                "NabSubmit/{OrderID}",
                new { controller = "Home", action = "Index", OrderID = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    "ReceiptPlain",
            //    "ReceiptPlain/{OrderId}/{StoreId}/{IsPrintReceipt}",
            //    new { controller = "ReceiptPlain", action = "ReceiptPlain", OrderId = UrlParameter.Optional, StoreId = UrlParameter.Optional, IsPrintReceipt = UrlParameter.Optional }
            //);

            routes.MapRoute(
                "autologin",
                "autologin/{C}/{F}/{L}/{E}/{CC}",
                new { controller = "Home", action = "AutoLoginOrRegister", C = UrlParameter.Optional, F = UrlParameter.Optional, L = UrlParameter.Optional, E = UrlParameter.Optional, CC = UrlParameter.Optional }
            );

            routes.MapRoute(
                "RemoveArtwork",
                "DeleteArtworkAttachment/{AttachmentID}",
                new { controller = "ProductOptions", action = "DeleteArtworkAttachment", AttachmentID = UrlParameter.Optional}
            );



            /////////////////////////////////////////////////////////////////////////////Payment gateway redirects
            routes.MapRoute(
               "PaypalHandler",
               "SignupPaypal/{OrderId}",
               new { controller = "Payment", action = "PaypalSubmit", OrderId = UrlParameter.Optional }
           );

            routes.MapRoute(
             "PaypalIPN",
             "PaypalIPN",
             new { controller = "Payment", action = "PaypalIPN"}
            );

            routes.MapRoute(
               "oAuth",
               "oAuth/{LoginWithId}/{isRegistrationProcess}/{StoreId}/{ReturnUrl}",
               new { controller = "Home", action = "oAuth", LoginWithId = UrlParameter.Optional, isRegistrationProcess = UrlParameter.Optional, StoreId = UrlParameter.Optional, ReturnUrl = UrlParameter.Optional }
           );


            routes.MapRoute(
             "ANZHandler",
             "ANZSubmit/{OrderId}",
             new { controller = "Payment", action = "ANZSubmit", OrderId = UrlParameter.Optional }
         );

            routes.MapRoute(
              "FBAuth",
              "FBAuth",
              new { controller = "Home", action = "FBAuthentication", token = UrlParameter.Optional }
          );
            
        routes.MapRoute(
          "ANZResponse",
          "ANZResponse",
          new { controller = "Payment", action = "ANZResponse" }
         );

        routes.MapRoute(
         "ApplyDelivery",
         "ShopCartAddressSelect/AddDeliveryToOrder/{DeliveryMethodId}",
         new { controller = "ShopCartAddressSelect", action = "AddDeliveryToOrder", DeliveryMethodId = UrlParameter.Optional }
       );
   
            routes.MapRoute(
               "Default", // Route name
               "",        // URL with parameters
               new { controller = "Home", action = "Index", id = UrlParameter.Optional }  // Parameter defaults
            );

            routes.MapRoute(
               name: "404-PageNotFound",
                        // This will handle any non-existing urls
               url: "{*url}",
                        // "Shared" is the name of your error controller, and "Error" is the action/page
                        // that handles all your custom errors
               defaults: new { controller = "Home", action = "NotFound" }
           );
        }
    }
}
