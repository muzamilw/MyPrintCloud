using System;
using System.Web.Mvc;

namespace MPC.MIS.Areas.Production.Controllers
{
    /// <summary>
    /// Production Board Controller
    /// </summary>
    public class HomeController : Controller
    {
        // GET: Production/Home
        public ActionResult Index()
        {
            Boolean istrial = false;
            int count = 0;
            ViewBag.Istrial = istrial;
            ViewBag.TrialCount = count;
            return View();
        }

        // GET: Production/ItemJobStatus
        public ActionResult ItemJobStatus()
        {
            return View();
        }

        public ActionResult ItemLateStart()
        {
            ViewBag.IsLateScreen = "Late";
            return View();
        }
    }
}
