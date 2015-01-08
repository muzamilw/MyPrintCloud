using System.Web.Mvc;
using System.Web.Http;

namespace MPC.Webstore.Areas.Designer
{
    public class DesignerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Designer";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.Routes.MapHttpRoute(
                  name: "DesignerDefault_MultiParams",
                  routeTemplate: AreaName + "/{controller}/{action}/{parameter1}/{parameter2}/{parameter3}/{parameter4}/{parameter5}/{parameter6}",
                  defaults: new { parameter1 = RouteParameter.Optional, parameter2 = RouteParameter.Optional, parameter3 = RouteParameter.Optional, parameter4 = RouteParameter.Optional, parameter5 = RouteParameter.Optional, parameter6 = RouteParameter.Optional }
            );
        }
    }
}