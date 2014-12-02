using System.Web.Mvc;

namespace MPC.MIS.Areas.Stores
{
    public class StoresAreaRegistration : AreaRegistration 
    {
        public override string AreaName
        {
            get
            {
                return "Stores";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Stores_default",
                "Stores/{controller}/{action}/{id}",
                //"{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MPC.MIS.Areas.Stores.Controllers" }
            );
        }
    }
}