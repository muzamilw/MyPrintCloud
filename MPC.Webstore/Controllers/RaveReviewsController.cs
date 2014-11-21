using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class RaveReviewsController : Controller
    {
        // GET: News
        public ActionResult Index()
        {

            return PartialView("PartialViews/RaveReviews");
        }
    }
}