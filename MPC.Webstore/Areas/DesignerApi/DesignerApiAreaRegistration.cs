using System.Web.Mvc;
using System.Web.Http;

namespace MPC.Webstore.Areas.DesignerApi
{
    public class DesignerApiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DesignerApi";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            //context.MapRoute(
            //    "DesignerApi_default",
            //    "DesignerApi/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
            context.Routes.MapHttpRoute(
               name: "DesignerApiDefault",
               routeTemplate: AreaName + "/{controller}/{id}",
               defaults: new { id = UrlParameter.Optional },
               constraints: new { id = @"^[0-9]+$" }
           );

            context.Routes.MapHttpRoute(
                name: "DesignerApiDefaultWithoutId",
                routeTemplate: AreaName + "/{controller}"
            );
        }
    }
}