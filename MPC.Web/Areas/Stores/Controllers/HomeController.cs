using System.Web.Mvc;
using MPC.Interfaces.Data;
using MPC.Models.Common;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Stores.Controllers
{
    [SiteAuthorize(MisRoles = new[] { SecurityRoles.Admin }, AccessRights = new[] { SecurityAccessRight.CanViewSecurity })]
    public class HomeController : Controller
    {
        // GET: Stores/Home
        [SiteAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrganisation })]
        public ActionResult Index()
        {
            return View();
        }
    }
}