using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class RealEstateHeaderController : Controller
    {
        // GET: RealEstateHeader
        public ActionResult Index()
        {
            return View("PartialViews/RealEstateHeader");
        }
    }
}