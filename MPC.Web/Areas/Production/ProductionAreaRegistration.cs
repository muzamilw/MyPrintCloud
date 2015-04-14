using System.Web.Mvc;

namespace MPC.MIS.Areas.Production
{
    public class ProductionAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Production";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Production_default",
                "Production/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                 namespaces: new[] { "MPC.MIS.Areas.Production.Controllers" }
            );
        }
    }
}