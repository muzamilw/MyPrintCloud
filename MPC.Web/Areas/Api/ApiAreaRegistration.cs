using System.Web.Http;
using System.Web.Mvc;

namespace MPC.MIS.Areas.Api
{
    public class ApiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Api";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {

            context.Routes.MapHttpRoute(
                name: "ApiDefault",
                routeTemplate: AreaName + "/{controller}/{id}",
                defaults: new { id = UrlParameter.Optional },
                constraints: new { id = @"^[0-9]+$" }
            );

            context.Routes.MapHttpRoute(
                name: "ApiDefaultWithoutId",
                routeTemplate: AreaName + "/{controller}"
            );
            context.Routes.MapHttpRoute(
              name: "ImportAPI",
              routeTemplate: AreaName + "/{controller}/{parameter1}/{parameter2}",
              defaults: new { parameter1 = UrlParameter.Optional, parameter2 = UrlParameter.Optional }
          );
            context.Routes.MapHttpRoute(
              name: "ImportStoreAPI",
              routeTemplate: AreaName + "/{controller}/{parameter1}/{parameter2}/{parameter3}/{parameter4}/{parameter5}",
              defaults: new { parameter1 = UrlParameter.Optional, parameter2 = UrlParameter.Optional, parameter3 = UrlParameter.Optional, parameter4 = UrlParameter.Optional,parameter5 = UrlParameter.Optional }
          );
           
        }
    }
}