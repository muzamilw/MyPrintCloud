using System;
using System.IO;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.Models.Common;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Controllers
{
    [SiteAuthorize(MisRoles = new[] { SecurityRoles.Admin }, AccessRights = new[] { SecurityAccessRight.CanViewDashboard })]
    public class DashboardController : Controller
    {
        [Dependency]
        public IClaimsSecurityService ClaimsSecurityService { get; set; }

       [SiteAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewDashboard })]
        public ActionResult Index()
        {
            Boolean istrial = false;
            int count = 0;
            ClaimsSecurityService.GetTrialUserClaims(ref istrial,ref count);
            ViewBag.Istrial = istrial;
            ViewBag.TrialCount = count;
            return View();
        }

       [System.Web.Http.AcceptVerbs("POST")]
       [HttpPost]
       [AllowAnonymous]
       public void PostFromZapier()
       {

           StreamReader reader = new StreamReader(HttpContext.Request.GetBufferedInputStream());
           string scont = reader.ReadToEndAsync().Result;
           
           //return Request.CreateResponse(HttpStatusCode.OK, _companyContactService.GetStoreContactForZapier(265512), formatter);
       }

        

    }
}
