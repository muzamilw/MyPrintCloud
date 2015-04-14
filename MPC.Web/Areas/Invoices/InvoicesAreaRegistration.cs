using System.Web.Mvc;

namespace MPC.MIS.Areas.Invoices
{
    /// <summary>
    /// Invoices Area Registration
    /// </summary>
    public class InvoicesAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Invoices";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Invoices_default",
                "Invoices/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MPC.MIS.Areas.Invoices.Controllers" }
            );
        }
    }
}