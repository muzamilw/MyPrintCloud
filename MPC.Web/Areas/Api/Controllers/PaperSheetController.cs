using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Web.Areas.Api.Controllers
{
    public class PaperSheetController : Controller
    {
        // GET: Api/PaperSheet
        public ActionResult Index()
        {
            return View();
        }
    }
}