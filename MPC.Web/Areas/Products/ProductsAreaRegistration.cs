using System.Web.Mvc;

namespace MPC.MIS.Areas.Products
{
    /// <summary>
    /// Products Area Registration
    /// </summary>
    public class ProductsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Products";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Products_default",
                "Products/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MPC.MIS.Areas.Products.Controllers" }
            );
        }
    }
}