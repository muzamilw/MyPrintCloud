using System;
using System.Web.Mvc;
using MPC.Interfaces.WebStoreServices;
using MPC.Webstore.ResponseModels;
using MPC.Webstore.Common;
using MPC.Webstore.ModelMappers;
using MPC.Webstore.Models;
using MPC.Models.DomainModels;

namespace MPC.Webstore.Controllers
{
    public class ContactUsController : Controller
    {
        #region Private

        private readonly ICompanyService _myCompanyService;

        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ContactUsController(ICompanyService myCompanyService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;
        }

        #endregion
        // GET: ContactUs
        public ActionResult Index()
        {
            MyCompanyDomainBaseResponse baseResponse = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromOrganisation();

            ViewBag.Organisation = baseResponse.Organisation;
            string country = baseResponse.Organisation.Country != null
                                                                ? baseResponse.Organisation.Country.CountryName
                                                                : string.Empty;
            string state = baseResponse.Organisation.State != null
                ? baseResponse.Organisation.State.StateName
                : string.Empty;
            string MapInfoWindow = baseResponse.Organisation.OrganisationName + "<br>" + baseResponse.Organisation.Address1 + baseResponse.Organisation.Address2 + "<br>" + baseResponse.Organisation.City + "," + state + "," + baseResponse.Organisation.ZipCode;
            ViewBag.googleMapScript = @"<script> var isGeoCode = true; var addressline = '" + baseResponse.Organisation.Address1 + "," + baseResponse.Organisation.Address2 + "," + baseResponse.Organisation.City + "," + country + "," + baseResponse.Organisation.ZipCode + "';var info='" + MapInfoWindow + "';</script>";
            return PartialView("PartialViews/ContactUs");
        }

        [HttpPost]
        public ActionResult Index(ContactViewModel model)
        {
            try
            {
                string smtpUser = null;
                string smtpserver = null;
                string smtpPassword = null;
                string fromName = null;
                string fromEmail = null;
                MyCompanyDomainBaseResponse organisationResponse = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromOrganisation();
                MyCompanyDomainBaseResponse companyResponse = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCompany();
                ViewBag.Organisation = organisationResponse.Organisation;
                string MesgBody = "";
                if (organisationResponse.Organisation != null)
                {
                    smtpUser = organisationResponse.Organisation.SmtpUserName;
                    smtpserver = organisationResponse.Organisation.SmtpServer;
                    smtpPassword = organisationResponse.Organisation.SmtpPassword;
                    fromName = organisationResponse.Organisation.OrganisationName;
                    fromEmail = organisationResponse.Organisation.Email;
                }


                string StoreName = string.Empty;

                SystemUser salesManager = _myCompanyService.GetSystemUserById(Convert.ToInt64(companyResponse.Company.SalesAndOrderManagerId1));

                StoreName = organisationResponse.Organisation.OrganisationName;


                MesgBody += "Dear " + salesManager.FullName + ",<br>";
                MesgBody += "An enquiry has been submitted to you with the details:<br>";
                MesgBody += "Name: " + model.YourName + "<br>";
                MesgBody += "Company Name: " + model.CompanyName + "<br>";
                MesgBody += "Store Name: " + StoreName + "<br>";
                MesgBody += "Email: " + model.Email + "<br>";
                MesgBody += "Nature of Enquiry: General <br>";
                MesgBody += "Enquiry: " + model.YourEnquiry + "<br>";
                // bool result = trué; //EmailManager.AddMsgToTblQueue(salesManager.Email, "", salesManager.FullName, MesgBody, fromName, fromEmail, smtpUser, smtpPassword, smtpserver, ddlEnqiryNature.SelectedItem.Text + " Contact enquiry from " + StoreName, null, 0);

                if (true)
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
            }
            catch (Exception ex)
            {

            }

            return PartialView("PartialViews/ContactUs");
        }
    }
}