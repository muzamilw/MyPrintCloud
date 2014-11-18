using System.Web.Mvc;

namespace MPC.Web.Areas.Settings.Controllers
{
    public class HomeController : Controller
    {
        // GET: Settings/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}