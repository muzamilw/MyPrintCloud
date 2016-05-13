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
        [SiteAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewPaperSheet })]
        public ActionResult PaperSheet()
        {
            return View();
        }

        // GET: Settings/MIS
        [SiteAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewInventory })]
        public ActionResult Inventory()
        {
            return View();
        }
        // GET: Settings/InventoryCategory
        [SiteAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewInventoryCategory })]
        public ActionResult InventoryCategory()
        {
            return View();
        }

        public ActionResult Prefixes()
        {
            return View();
        }

        public ActionResult CostCenters()
        {
            return View();
        }

        public ActionResult SalesPipeLine()
        { 
        
           return View();
        
        }
        public ActionResult SectionFlags()
        {

            return View();
        
        }
        public ActionResult ReportBanner()
        {

            return View();

        }
        public ActionResult UserRole()
        {

            return View();

        }
        public ActionResult SystemUsers()
        {

            return View();

        }
    }
}