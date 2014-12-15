using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class AllProductsController : Controller
    {
        // GET: AllProducts
        public ActionResult Index()
        {
            return View();
        }
    }
}