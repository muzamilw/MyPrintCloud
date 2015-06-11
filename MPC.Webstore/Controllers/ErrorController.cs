using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(Exception exception, int errorType)
        {
            Response.TrySkipIisCustomErrors = true;

            ViewBag.Exception = exception.Message;
            ViewBag.StackTrace = exception.StackTrace;

            return View();
        }

    }
}