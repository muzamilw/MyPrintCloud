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
using MPC.Models.ResponseModels;

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
            //string CacheKeyName = "CompanyBaseResponse";
            //ObjectCache cache = MemoryCache.Default;

            //MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            SetDefaultAddress(StoreBaseResopnse);
            return PartialView("PartialViews/ContactUs");
        }

        [HttpPost]
        public ActionResult Index(ContactViewModel model)
        {
            //string CacheKeyName = "CompanyBaseResponse";
            //ObjectCache cache = MemoryCache.Default;
            try
            {
                string smtpUser = null;
                string smtpserver = null;
                string smtpPassword = null;
                string fromName = null;
                string fromEmail = null;
                //MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
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
                MesgBody += Utils.GetKeyValueFromResourceFile("ltrlnamee", UserCookieManager.WBStoreId, "Name:") + model.YourName + "<br>";
                MesgBody += Utils.GetKeyValueFromResourceFile("ltrlcompanynamee", UserCookieManager.WBStoreId, "Company Name:") + model.CompanyName + "<br>";
                MesgBody += Utils.GetKeyValueFromResourceFile("ltrlStoreNamee", UserCookieManager.WBStoreId, "Store Name:") + StoreName + "<br>";
                MesgBody += Utils.GetKeyValueFromResourceFile("ltrlllEmail", UserCookieManager.WBStoreId, "Email:")
 + model.Email + "<br>";
                MesgBody += Utils.GetKeyValueFromResourceFile("ltrlnaturofinq", UserCookieManager.WBStoreId, "Nature of Enquiry: General:") + "<br>"
;
                MesgBody += Utils.GetKeyValueFromResourceFile("ltrlinqq", UserCookieManager.WBStoreId, "Enquiry:") + model.YourEnquiry + "<br>";
                bool result = _myCompainservice.AddMsgToTblQueue(salesManager.Email, "", salesManager.FullName, MesgBody, fromName, fromEmail, smtpUser, smtpPassword, smtpserver, model.YourEnquiry + " Contact enquiry from " + StoreName, null, 0);

                 if (result)
                {
                    model.YourEnquiry = "";
                    model.YourName = "";
                    model.CompanyName = "";
                    model.Email = "";
                    ViewBag.Message = Utils.GetKeyValueFromResourceFile("ltrlsubmitt", UserCookieManager.WBStoreId, "Thank you for submitting a request. Someone will contact you shortly.");
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
            AddressViewModel oAddress = null;

            if (StoreBaseResopnse.StoreDetaultAddress != null)
            {
                oAddress = new AddressViewModel();
                oAddress.AddressName = StoreBaseResopnse.StoreDetaultAddress.AddressName;
                oAddress.Address1 = StoreBaseResopnse.StoreDetaultAddress.Address1;
                oAddress.Address2 = StoreBaseResopnse.StoreDetaultAddress.Address2;

                oAddress.City = StoreBaseResopnse.StoreDetaultAddress.City;
                oAddress.State = _myCompanyService.GetStateNameById(StoreBaseResopnse.StoreDetaultAddress.StateId ?? 0);
                oAddress.Country = _myCompanyService.GetCountryNameById(StoreBaseResopnse.StoreDetaultAddress.CountryId ?? 0);
                oAddress.ZipCode = StoreBaseResopnse.StoreDetaultAddress.PostCode;

                if (!string.IsNullOrEmpty(StoreBaseResopnse.StoreDetaultAddress.Tel1))
                {
                    oAddress.Tel = "Tel: " + StoreBaseResopnse.StoreDetaultAddress.Tel1;
                }
                if (!string.IsNullOrEmpty(StoreBaseResopnse.StoreDetaultAddress.Fax))
                {
                    oAddress.Fax = "Fax: " + StoreBaseResopnse.StoreDetaultAddress.Fax;
                }
                if (!string.IsNullOrEmpty(StoreBaseResopnse.StoreDetaultAddress.Email))
                {
                    oAddress.Email = StoreBaseResopnse.StoreDetaultAddress.Email;
                }
            }
            if (StoreBaseResopnse.StoreDetaultAddress != null)
            {
              
                string MapInfoWindow = oAddress.AddressName + "<br>" + oAddress.Address1 + oAddress.Address2 + "<br>" + oAddress.City + "," + oAddress.State + "," + oAddress.ZipCode;
                if (StoreBaseResopnse.Company.isShowGoogleMap == 2) // geo code aka zip code lookup 
                {
                    ViewBag.MapImage = null;
                    ViewBag.googleMapScript = @"<script> var isGeoCode = true; var addressline = '" + oAddress.ZipCode + "," + oAddress.City + "," + oAddress.Country + "';var info='" + MapInfoWindow.Replace("'"," ") + "';</script>";
                    //ViewBag.googleMapScript = @"<script> var isGeoCode = true; var addressline = '" + oAddress.Address1 + "," + oAddress.Address2 + "," + oAddress.City + "," + oAddress.Country + "," + oAddress.ZipCode + "';var info='" + MapInfoWindow + "';</script>";
                }
                else if (StoreBaseResopnse.Company.isShowGoogleMap == 1) // geo code aka zip code lookup 
                { // locate by lat long


                    ViewBag.googleMapScript = @"<script> isGeoCode = false; var lat = '" + StoreBaseResopnse.StoreDetaultAddress.GeoLatitude + "'; var long='" + StoreBaseResopnse.StoreDetaultAddress.GeoLongitude + "';var info='" + MapInfoWindow.Replace("'", " ") + "';</script>";
                    ViewBag.MapImage = null;
                }
                else 
                {
                    ViewBag.MapImage = StoreBaseResopnse.Company.MapImageUrl;
                }
                ViewBag.DefaultAddress = oAddress;
            }
            else 
            {
                ViewBag.DefaultAddress = null;
                throw new Exception("Default address not found");
            }
          
        }

    }
}