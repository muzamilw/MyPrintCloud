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

        // GET: Settings/MIS
        public ActionResult Inventory()
        {
            return View();
        }
    }
}