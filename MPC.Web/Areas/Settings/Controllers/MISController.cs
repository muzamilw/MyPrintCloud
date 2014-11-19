using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.MIS.Areas.Settings.Controllers
{
    public class MISController : Controller
    {
        // GET: Settings/MIS
        public ActionResult PaperSheet()
        {
            return View();
        }
    }
}