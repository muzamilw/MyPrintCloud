using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
using MPC.Webstore.Areas.WebstoreApi.Models;
using MPC.Webstore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class ContactFormController : Controller
    {
        #region Private

        private readonly ICompanyService _myCompanyService;
        private readonly ICampaignService _myCompainservice;
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ContactFormController(ICompanyService myCompanyService, ICampaignService _myCompainservice)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;
            this._myCompainservice = _myCompainservice;
        }

        #endregion
        // GET: ContactForm
        public ActionResult Index()
        {
            return PartialView("PartialViews/ContactForm");
        }
        [HttpPost]
        public ActionResult Index(RFQContactForm rfqForm)
        {
            
            try
            {
                string smtpUser = null;
                string smtpserver = null;
                string smtpPassword = null;
                string fromName = null;
                string fromEmail = null;
                
                MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

                ViewBag.Organisation = StoreBaseResopnse.StoreDetaultAddress;
                string MesgBody = "";
                if (StoreBaseResopnse.Organisation != null)
                {
                    //organisationResponse
                    smtpUser = StoreBaseResopnse.Organisation.SmtpUserName == null ? "" : StoreBaseResopnse.Organisation.SmtpUserName;
                    smtpserver = StoreBaseResopnse.Organisation.SmtpServer;
                    smtpPassword = StoreBaseResopnse.Organisation.SmtpPassword;
                    fromName = StoreBaseResopnse.Organisation.OrganisationName;
                    fromEmail = StoreBaseResopnse.Organisation.Email;

                }

                string StoreName = string.Empty;

                SystemUser salesManager = _myCompanyService.GetSystemUserById(StoreBaseResopnse.Company.SalesAndOrderManagerId1.Value);

                StoreName = StoreBaseResopnse.StoreDetaultAddress.AddressName;


                MesgBody += Utils.GetKeyValueFromResourceFile("ltrlDear", UserCookieManager.WBStoreId, "Dear") + salesManager.FullName + ",<br>";
                MesgBody += Utils.GetKeyValueFromResourceFile("ltrlinqsub", UserCookieManager.WBStoreId, "An enquiry has been submitted to you with the details:") + "<br>";
                MesgBody += Utils.GetKeyValueFromResourceFile("lblContactFormFullName", UserCookieManager.WBStoreId, "Name: ") + rfqForm.FullName + "<br>";
                MesgBody += Utils.GetKeyValueFromResourceFile("lblContactFormMobile", UserCookieManager.WBStoreId, "Mobile: ") + rfqForm.Mobile + "<br>";
                MesgBody += Utils.GetKeyValueFromResourceFile("lblContactFormQuantity", UserCookieManager.WBStoreId, "Quantity: ") + rfqForm.Quantity + "<br>";
                MesgBody += Utils.GetKeyValueFromResourceFile("lblContactFormEmail", UserCookieManager.WBStoreId, "Email: ") + rfqForm.Email + "<br>";
                MesgBody += Utils.GetKeyValueFromResourceFile("lblContactFormMessage", UserCookieManager.WBStoreId, "Message: ") + rfqForm.Message + "<br>";
              
                bool result = _myCompainservice.AddMsgToTblQueue(salesManager.Email, "", salesManager.FullName, MesgBody, fromName, fromEmail, smtpUser, smtpPassword, smtpserver, " Contact enquiry from " + StoreName, null, 0);

                if (result)
                {
                    rfqForm.FullName = "";
                    rfqForm.Mobile = "";
                    rfqForm.Quantity = "";
                    rfqForm.Email = "";
                    rfqForm.Message = "";
                    ViewBag.Message = Utils.GetKeyValueFromResourceFile("ltrlsubmitt", UserCookieManager.WBStoreId, "Thank you for submitting a request. Someone will contact you shortly.");
                }
                else
                {
                    ViewBag.Message = "An error occured. Please try again.";
                }
              
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return PartialView("PartialViews/ContactForm");
        }
    }
}