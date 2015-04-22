using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class ErrorPageController : Controller
    {
        // GET: ErrorPage
        public ActionResult Index()
        {
            ViewBag.Message = HttpContext.Request.QueryString["ErrorMessage"];
            return View("PartialViews/ErrorPage");
        }
    }
}