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
            context.MapRoute(
                "Stores_default",
                "Stores/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MPC.MIS.Areas.Stores.Controllers" }
            );
            context.MapRoute(
                  "DesignerDefault_MultiParams",
                  AreaName + "/{designName}/{categoryID}/{categoryIDV2}/{templateID}/{itemID}/{customerID}/{contactID}/{printCropMarks}/{printWaterMarks}",
                  new { controller = "Designer", action = "Index", designName = RouteParameter.Optional, categoryID = RouteParameter.Optional, categoryIDV2 = RouteParameter.Optional, templateID = RouteParameter.Optional, itemID = RouteParameter.Optional, customerID = RouteParameter.Optional, contactID = RouteParameter.Optional, printCropMarks = RouteParameter.Optional, printWaterMarks = RouteParameter.Optional }
            );
        }
    }
}