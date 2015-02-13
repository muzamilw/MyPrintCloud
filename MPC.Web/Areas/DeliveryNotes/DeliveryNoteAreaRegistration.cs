using System.Web.Mvc;

namespace MPC.MIS.Areas.DeliveryNotes
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
                return "DeliveryNotes";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "delivery_default",
                "DeliveryNotes/{controller}/{action}/{id}",
                new { action = "DeliveryNote", id = UrlParameter.Optional },
                namespaces: new[] { "MPC.MIS.Areas.DeliveryNotes.Controllers" }
            );
        }
    }
}