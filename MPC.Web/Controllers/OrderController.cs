using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Web.Controllers
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