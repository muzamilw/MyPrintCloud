using System.Web.Http;
using System.Web.Mvc;

namespace MPC.Webstore.Areas.OrderReceipt
{
    public class OrderReceiptAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "OrderReceipt";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                     "ReceiptDefault_MultiParams",
                    AreaName + "/{OrderId}/{StoreId}/{IsPrintReceipt}",
                     new { controller = "OrderReceipt", action = "Index", OrderId = RouteParameter.Optional, StoreId = RouteParameter.Optional, IsPrintReceipt = RouteParameter.Optional }
               );
        }
    }
}