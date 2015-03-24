using System;
using System.Web.Mvc;
using MPC.Interfaces.WebStoreServices;
using MPC.Webstore.ResponseModels;
using MPC.Webstore.Common;
using MPC.Webstore.ModelMappers;
using MPC.Webstore.Models;
using MPC.Models.DomainModels;
using System.Runtime.Caching;
using System.Collections.Generic;

namespace MPC.Webstore.Controllers
{
    public class ContactUsController : Controller
    {
        #region Private

        private readonly ICompanyService _myCompanyService;
        private readonly ICampaignService _myCompainservice;
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ContactUsController(ICompanyService myCompanyService, ICampaignService _myCompainservice)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;
            this._myCompainservice = _myCompainservice;
        }

        #endregion
        // GET: ContactUs
        public ActionResult Index()
        {
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;

            MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.StoreId];

            SetDefaultAddress(StoreBaseResopnse);
            return PartialView("PartialViews/ContactUs");
        }

        [HttpPost]
        public ActionResult Index(ContactViewModel model)
        {
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;
            try
            {
                string smtpUser = null;
                string smtpserver = null;
                string smtpPassword = null;
                string fromName = null;
                string fromEmail = null;
                MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.StoreId];
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

                SystemUser salesManager = _myCompanyService.GetSystemUserById(Convert.ToInt64(StoreBaseResopnse.Company.SalesAndOrderManagerId1));

                StoreName = StoreBaseResopnse.StoreDetaultAddress.AddressName;


                MesgBody += "Dear " + salesManager.FullName + ",<br>";
                MesgBody += "An enquiry has been submitted to you with the details:<br>";
                MesgBody += "Name: " + model.YourName + "<br>";
                MesgBody += "Company Name: " + model.CompanyName + "<br>";
                MesgBody += "Store Name: " + StoreName + "<br>";
                MesgBody += "Email: " + model.Email + "<br>";
                MesgBody += "Nature of Enquiry: General <br>";
                MesgBody += "Enquiry: " + model.YourEnquiry + "<br>";
                bool result = _myCompainservice.AddMsgToTblQueue(salesManager.Email, "", salesManager.FullName, MesgBody, fromName, fromEmail, smtpUser, smtpPassword, smtpserver, model.YourEnquiry + " Contact enquiry from " + StoreName, null, 0);

                 if (result)
                {
                    model.YourEnquiry = "";
                    model.YourName = "";
                    model.CompanyName = "";
                    model.Email = "";
                    ViewBag.Message = "Thank you for submitting a request. Someone will contact you shortly.";
                }
                else
                {
                    //ViewBag.Message = "An error occured. Please try again.";
                }
                SetDefaultAddress(StoreBaseResopnse);
                
            }
            catch (Exception ex)
            {
                throw ex;
                
            }
            return PartialView("PartialViews/ContactUs");
        }

        private void SetDefaultAddress(MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse) 
        {

            if (StoreBaseResopnse.StoreDetaultAddress != null)
            {
                ViewBag.DefaultAddress = StoreBaseResopnse.StoreDetaultAddress;

                string country = StoreBaseResopnse.StoreDetaultAddress.Country != null
                                                                    ? StoreBaseResopnse.StoreDetaultAddress.Country.CountryName
                                                                    : string.Empty;

                string state = StoreBaseResopnse.StoreDetaultAddress.State != null
                    ? StoreBaseResopnse.StoreDetaultAddress.State.StateName
                    : string.Empty;
                string MapInfoWindow = StoreBaseResopnse.StoreDetaultAddress.AddressName + "<br>" + StoreBaseResopnse.StoreDetaultAddress.Address1 + StoreBaseResopnse.StoreDetaultAddress.Address2 + "<br>" + StoreBaseResopnse.StoreDetaultAddress.City + "," + state + "," + StoreBaseResopnse.StoreDetaultAddress.PostCode;
                ViewBag.googleMapScript = @"<script> var isGeoCode = true; var addressline = '" + StoreBaseResopnse.StoreDetaultAddress.Address1 + "," + StoreBaseResopnse.StoreDetaultAddress.Address2 + "," + StoreBaseResopnse.StoreDetaultAddress.City + "," + country + "," + StoreBaseResopnse.StoreDetaultAddress.PostCode + "';var info='" + MapInfoWindow + "';</script>";
            }
            else 
            {
                ViewBag.DefaultAddress = null;
                throw new Exception("Default address not found");
            }
          
        }

    }
}