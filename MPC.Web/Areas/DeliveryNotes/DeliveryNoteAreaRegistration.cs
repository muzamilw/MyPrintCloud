using System.Web.Mvc;

namespace MPC.MIS.Areas.Orders
{
    /// <summary>
    /// Orders Area Registration
    /// </summary>
    public class DeliveryNoteAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Delivery Notes";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Orders_default",
                "DeliveryNotes/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MPC.MIS.Areas.DeliveryNotes.Controllers" }
            );
        }
    }
}