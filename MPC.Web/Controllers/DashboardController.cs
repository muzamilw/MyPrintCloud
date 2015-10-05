using System;
using System.IO;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MPC.ExceptionHandling;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.WebBase.Mvc;
using Newtonsoft.Json;

namespace MPC.MIS.Controllers
{
    [SiteAuthorize(MisRoles = new[] { SecurityRoles.Admin }, AccessRights = new[] { SecurityAccessRight.CanViewDashboard })]
    public class DashboardController : Controller
    {

        private readonly IMyOrganizationService _organizationService;
        public DashboardController(IMyOrganizationService organizationService)
        {
            this._organizationService = organizationService;
        }
        
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
       public void TargetUrlFromZapier()
       {
           long organisationId = 1;
           string param = HttpContext.Request.Url.Query;
           string responsestr = _organizationService.GetActiveOrganisationId(param);
           responsestr = "1"; //Temporarily set for local testing
           if (string.IsNullOrEmpty(responsestr) || responsestr == "Fail")
           {
               throw new MPCException("Service Not Authenticated!", organisationId);
           }
           else
           {
               organisationId = Convert.ToInt64(responsestr);
           }
           StreamReader reader = new StreamReader(HttpContext.Request.GetBufferedInputStream());
           string scont = reader.ReadToEndAsync().Result;
           ZapierPostResponse listingProperty = JsonConvert.DeserializeObject<ZapierPostResponse>(scont);
           _organizationService.UpdateOrganisationZapTargetUrl(organisationId, listingProperty.subscription_url, 1);
           
       }
       [System.Web.Http.AcceptVerbs("POST")]
       [HttpPost]
       [AllowAnonymous]
       public void PostInvoiceUrlFromZapier()
       {
           long organisationId = 1;
           StreamReader reader = new StreamReader(HttpContext.Request.GetBufferedInputStream());
           string scont = reader.ReadToEndAsync().Result;
           _organizationService.UpdateOrganisationZapTargetUrl(organisationId, "https://zapier.com/hooks/standard/4PwB04shgS2o8z7vnVguLyXu1SFYboIo/", 2);


       }

        

    }
}
