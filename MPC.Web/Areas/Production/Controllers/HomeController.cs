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
            return View();
        }

        // GET: Production/ItemJobStatus
        public ActionResult ItemJobStatus()
        {
            return View();
        }
    }
}
