using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class FeaturedProductCarousalController : Controller
    {
        // GET: FeaturedProductCarousal
        public ActionResult Index()
        {
            return PartialView("PartialViews/FeaturedProductCarousal");
         
        }
    }
}