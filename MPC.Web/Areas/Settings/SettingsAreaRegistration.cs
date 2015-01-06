using System.Web.Mvc;

namespace MPC.MIS.Areas.Settings
{
    public class SettingsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Settings";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Settings_default",
                "Settings/{controller}/{action}/{id}",
                new { action = "MyOrganisation", id = UrlParameter.Optional },
                namespaces: new[] { "MPC.MIS.Areas.Settings.Controllers" }
            );
        }
    }
}