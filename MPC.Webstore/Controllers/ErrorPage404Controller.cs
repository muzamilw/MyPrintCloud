using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class ErrorPage404Controller : Controller
    {
        // GET: ErrorPage404
        public ActionResult Index()
        {
            return PartialView("PartialViews/ErrorPage404");
        }
    }
}