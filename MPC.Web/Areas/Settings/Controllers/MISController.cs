using System.Web.Mvc;
using MPC.Interfaces.Data;
using MPC.Models.Common;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Settings.Controllers
{
    [SiteAuthorize(MisRoles = new[] { SecurityRoles.Admin }, AccessRights = new[] { SecurityAccessRight.CanViewSecurity })]
    public class MISController : Controller
    {
        // GET: Settings/MIS
        [SiteAuthorize(AccessRights = new[] { SecurityAccessRight.CanVeiwPaperSheet })]
        public ActionResult PaperSheet()
        {
            return View();
        }

        // GET: Settings/MIS
        [SiteAuthorize(AccessRights = new[] { SecurityAccessRight.CanVeiwInventory })]
        public ActionResult Inventory()
        {
            return View();
        }
    }
}