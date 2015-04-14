using System.Web.Mvc;
using MPC.Interfaces.Data;
using MPC.Models.Common;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Orders.Controllers
{
    /// <summary>
    /// Orders Home Controller
    /// </summary>
    [SiteAuthorize(MisRoles = new[] { SecurityRoles.Admin }, AccessRights = new[] { SecurityAccessRight.CanViewOrder })]
    public class HomeController : Controller
    {

        #region Private
        #endregion

        #region Constructors
        #endregion

        // GET: Orders/Home
        [SiteAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrder })]
        public ActionResult Index()
        {
            ViewBag.CallingMethod = (string) TempData["CallingMethod"] != "" ? TempData["CallingMethod"] : "0";
            return View();
        }

        public ActionResult PurchaseOrders()
        {

            return View();

        }
        public ActionResult PurchaseOrdersPO()
        {

            return View();

        }
        public ActionResult EstimatesList()
        {
            return View();
        }
      
        public ActionResult PendingOrders()
        {
            TempData["CallingMethod"] = "4";
            return RedirectToAction("Index");
        }
        public ActionResult ConfirmedStarts()
        {
            TempData["CallingMethod"] = "5";
            return RedirectToAction("Index");
        }
        public ActionResult InProduction()
        {
            TempData["CallingMethod"] = "6";
            return RedirectToAction("Index");
        }
        public ActionResult ReadyForShipping()
        {
            TempData["CallingMethod"] = "7";
            return RedirectToAction("Index");
        }
        public ActionResult Invoiced()
        {
            TempData["CallingMethod"] = "8";
            return RedirectToAction("Index");
        }
        public ActionResult CancelledOrders()
        {
            TempData["CallingMethod"] = "9";
            return RedirectToAction("Index");
        }

    }
}