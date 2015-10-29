using System.Web.Mvc;
using MPC.Interfaces.MISServices;

namespace MPC.MIS.Controllers
{
    public class OrderController : Controller
    {
        
        public ActionResult OrderList()
        {
            return View();
        }
        public ActionResult OrderDetail()
        {
            return View();
        }
        public ActionResult OrderItemDetail()
        {
            return View();
        }
        public ActionResult OrderSectionDetail()
        {
            return View();
        }

        

    }
}