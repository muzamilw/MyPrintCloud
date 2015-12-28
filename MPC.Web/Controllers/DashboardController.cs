﻿using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
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
           int eventId = 1;
           string param = HttpContext.Request.Url.Query;
           string responsestr = _organizationService.GetActiveOrganisationId(param);
          // responsestr = Temporarily set for local testing
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
           ZapierPostResponse zapierResponse = JsonConvert.DeserializeObject<ZapierPostResponse>(scont);
           if (zapierResponse.Event == "contact_created")
               eventId = 1;
           else if (zapierResponse.Event == "invoice_created")
               eventId = 2;
           _organizationService.UpdateOrganisationZapTargetUrl(organisationId, zapierResponse.subscription_url, eventId);
           
       }
       [System.Web.Http.AcceptVerbs("POST")]
       [HttpPost]
       [AllowAnonymous]
       public void UnsubscribeUrlFromZapier()
       {
           long organisationId = 1;
           int eventId = 1;
           string param = HttpContext.Request.Url.Query;
           string responsestr = _organizationService.GetActiveOrganisationId(param);
           // responsestr = Temporarily set for local testing
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
           ZapierPostResponse zapierResponse = JsonConvert.DeserializeObject<ZapierPostResponse>(scont);
           if (zapierResponse.Event == "contact_created")
               eventId = 1;
           else if (zapierResponse.Event == "invoice_created")
               eventId = 2;
           _organizationService.UnSubscriebZapTargetUrl(organisationId, zapierResponse.subscription_url, eventId);



       }
       [System.Web.Http.AcceptVerbs("POST")]
       [HttpPost]
       [AllowAnonymous]
       public string CreateInvoiceZapAction()
       {
           long organisationId = 1;
           
           string param = HttpContext.Request.Url.Query;
           string responsestr = _organizationService.GetActiveOrganisationId(param);
           // responsestr = Temporarily set for local testing
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
           //ZapierPostResponse zapierResponse = JsonConvert.DeserializeObject<ZapierPostResponse>(scont);

           //var formatter = new JsonMediaTypeFormatter();
           //var json = formatter.SerializerSettings;
           //json.Formatting = Newtonsoft.Json.Formatting.Indented;
           //json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

           //return HttpContext.Request.CreateResponse(HttpStatusCode.OK, _companyContactService.GetContactForZapierPooling(0), formatter);

           return scont;

       }

       [System.Web.Http.AcceptVerbs("POST")]
       [HttpPost]
       [AllowAnonymous]
       public string CreateContactZapAction()
       {
           long organisationId = 1;

           string param = HttpContext.Request.Url.Query;
           string responsestr = _organizationService.GetActiveOrganisationId(param);
           // responsestr = Temporarily set for local testing
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
           //ZapierPostResponse zapierResponse = JsonConvert.DeserializeObject<ZapierPostResponse>(scont);

           //var formatter = new JsonMediaTypeFormatter();
           //var json = formatter.SerializerSettings;
           //json.Formatting = Newtonsoft.Json.Formatting.Indented;
           //json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

           //return HttpContext.Request.CreateResponse(HttpStatusCode.OK, _companyContactService.GetContactForZapierPooling(0), formatter);

           return scont;

       }

    }
}
