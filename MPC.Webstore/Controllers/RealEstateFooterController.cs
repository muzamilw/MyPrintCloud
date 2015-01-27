using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class RealEstateFooterController : Controller
    {
        // GET: RealEstateFooter
        public ActionResult Index()
        {
            return View("PartialViews/RealEstateFooter");
        }
    }
}