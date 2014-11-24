using System.Web.Mvc;

namespace MPC.MIS.Areas.Settings.Controllers
{
    //[SiteAuthorize(MisRoles = new []{ SecurityRoles.Admin }, AccessRights = new []{ SecurityAccessRight.CanViewSecurity })]
    public class HomeController : Controller
    {
        // GET: Settings/Home
        //[SiteAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrganisation })]
        public ActionResult Index()
        {
            return View();
        }
    }
}