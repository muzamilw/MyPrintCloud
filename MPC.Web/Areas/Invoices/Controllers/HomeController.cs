using System.Web.Mvc;
using MPC.Interfaces.Data;
using MPC.Models.Common;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Invoices.Controllers
{
    [SiteAuthorize(MisRoles = new[] { SecurityRoles.Admin }, AccessRights = new[] { SecurityAccessRight.CanViewInvoicing })]
    public class HomeController : Controller
    {
        // GET: Invoices/Home
        public ActionResult Index()
        {
            ViewBag.CallingMethod = (string)TempData["CallingMethod"] != "" ? TempData["CallingMethod"] : "0";
            return View();
        }

        public ActionResult AwaitingInvoices()
        {
            TempData["CallingMethod"] = "1";
            return RedirectToAction("Index");
        }
        public ActionResult PostedInvoices()
        {
            TempData["CallingMethod"] = "2";
            return RedirectToAction("Index");
        }
        public ActionResult CancelledInvoices()
        {
            TempData["CallingMethod"] = "3";
            return RedirectToAction("Index");
        }
        public ActionResult ArchivedInvoices()
        {
            TempData["CallingMethod"] = "4";
            return RedirectToAction("Index");
        }
        public ActionResult ProformaInvoices()
        {
            TempData["CallingMethod"] = "5";
            return RedirectToAction("Index");
        }
       
    }
}