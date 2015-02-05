using System.Web.Mvc;

namespace MPC.MIS.Areas.CRM
{
    public class CrmAreaRegistration : AreaRegistration 
    {
        public override string AreaName
        {
            get
            {
                return "CRM";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CRM_default",
                "CRM/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MPC.MIS.Areas.CRM.Controllers" }
            );
        }
    }
}