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
            return View();
        }

        public ActionResult PurchaseOrders()
        {

            return View();

        }
    }
}