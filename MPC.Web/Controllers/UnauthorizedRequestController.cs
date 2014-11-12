using System.Web.Mvc;

namespace MPC.Web.Controllers
{
    public class UnauthorizedRequestController : Controller
    {
        //
        // GET: /UnauthorizedRequest/
        public ActionResult Index()
        {
            return View();
        }
	}
}