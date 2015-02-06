using System.Web.Mvc;

namespace MPC.MIS.Areas.Orders
{
    /// <summary>
    /// Orders Area Registration
    /// </summary>
    public class OrdersAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Orders";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Orders_default",
                "Orders/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MPC.MIS.Areas.Orders.Controllers" }
            );
        }
    }
}