using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Interfaces.Data;
using MPC.Models.Common;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Controllers
{
     [SiteAuthorize(MisRoles = new[] { SecurityRoles.Admin }, AccessRights = new[] { SecurityAccessRight.CanViewSettings })]
    public class SettingsController : Controller
    {
        //
        // GET: /Settings/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MyOrganization()
        {
            return View();
        }
        public ActionResult PageNotFound()
        {
            return View();
        }
        public ActionResult MachinesList()
        {
            return View();
        }
        public ActionResult MachinesDetail()
        {
            return View();
        }
        public ActionResult DeliveryCostCentres()
        {
            return View();
        }
        public ActionResult DeliveryAddOnsDetail()
        {
            return View();
        }
        public ActionResult DeliveryCarrier()
        {
            return View();
        }
    }
}
