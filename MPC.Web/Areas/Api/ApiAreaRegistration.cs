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
              name: "ZohaibAPICollerntro",
              routeTemplate: AreaName + "/{controller}/{parameter1}/{parameter2}",
              defaults: new { parameter1 = UrlParameter.Optional, parameter2 = UrlParameter.Optional }
          );
        }
    }
}