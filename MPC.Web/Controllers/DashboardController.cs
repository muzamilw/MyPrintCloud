using System;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MPC.Interfaces.MISServices;

namespace MPC.MIS.Controllers
{
    public class DashboardController : Controller
    {
        [Dependency]
        public IClaimsSecurityService ClaimsSecurityService { get; set; }

        public ActionResult Index()
        {
            Boolean istrial = false;
            int count = 0;
            ClaimsSecurityService.GetTrialUserClaims(ref istrial,ref count);
            ViewBag.Istrial = istrial;
            ViewBag.TrialCount = count;
            return View();
        }

        

    }
}
