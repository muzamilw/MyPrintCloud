using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MPC.Webstore.Areas.WebstoreApi.Controllers
{
    public class ContactImageUploaderController : ApiController
    {
        #region Private

        private readonly IWebstoreClaimsHelperService _webstoreAuthorizationChecker;
        private readonly IItemService _ItemService;
        private readonly ICompanyService _companyService;
        private readonly ICampaignService _campaignService;
        private readonly IUserManagerService _usermanagerService;
        private readonly ICompanyContactRepository _companyContact;
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        private readonly IOrderService _orderService;
        public ContactImageUploaderController(IItemService ItemService, IOrderService _orderService, ICompanyService companyService, IWebstoreClaimsHelperService _webstoreAuthorizationChecker, ICampaignService _campaignService, IUserManagerService _usermanagerService, ICompanyContactRepository _companyContact)
        {
            
            this._ItemService = ItemService;
            this._orderService = _orderService;
            this._companyService = companyService;
            this._webstoreAuthorizationChecker = _webstoreAuthorizationChecker;
            this._campaignService = _campaignService;
            this._usermanagerService = _usermanagerService;
            this._companyContact = _companyContact;
        }

        #endregion

        [HttpPost]
        public void UploadImage(string FirstName, string LastName, string Email, string JobTitle, string HomeTel1, string Mobile, string FAX, string CompanyName, string quickWebsite, string POBoxAddress, string CorporateUnit, string OfficeTradingName, string ContractorName, string BPayCRN, string ABN, string ACN, string AdditionalField1, string AdditionalField2, string AdditionalField3, string AdditionalField4, string AdditionalField5, bool IsEmailSubscription, bool IsNewsLetterSubscription)
        {
         
            //Get the uploaded image from the Files collection
            try
            {
                var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
            
                bool result = false;
                CompanyContact UpdateContact = new CompanyContact();
                UpdateContact.FirstName = FirstName;
                UpdateContact.LastName = LastName;
                UpdateContact.Email = Email;
                UpdateContact.JobTitle = JobTitle;
                UpdateContact.HomeTel1 = HomeTel1;
                UpdateContact.Mobile = Mobile;
                UpdateContact.FAX = FAX;
                UpdateContact.quickWebsite = quickWebsite;
                UpdateContact.image = UpdateImage(httpPostedFile);
                UpdateContact.ContactId = _webstoreAuthorizationChecker.loginContactID();
                UpdateContact.IsEmailSubscription = IsEmailSubscription;
                UpdateContact.IsNewsLetterSubscription = IsNewsLetterSubscription;
                if (UserCookieManager.WEBStoreMode == (int)StoreMode.Retail)
                {
                    Company Company = _companyService.GetCompanyByCompanyID(_webstoreAuthorizationChecker.loginContactCompanyID());
                    if (Company != null)
                    {
                        Company.Name = CompanyName;
                        Company.CompanyId = _webstoreAuthorizationChecker.loginContactCompanyID();
                        result = _companyService.UpdateCompanyName(Company);
                    }
                    result = _companyService.UpdateCompanyContactForRetail(UpdateContact);
                }
                else
                {
                    UpdateContact.POBoxAddress = POBoxAddress;
                    UpdateContact.CorporateUnit = CorporateUnit;
                    UpdateContact.OfficeTradingName = OfficeTradingName;
                    UpdateContact.ContractorName = ContractorName;
                    UpdateContact.BPayCRN = BPayCRN;
                    UpdateContact.ABN = ABN;
                    UpdateContact.ACN = ACN;
                    UpdateContact.AdditionalField1 = AdditionalField1;
                    UpdateContact.AdditionalField2 = AdditionalField2;
                    UpdateContact.AdditionalField3 = AdditionalField3;
                    UpdateContact.AdditionalField4 = AdditionalField4;
                    UpdateContact.AdditionalField5 = AdditionalField5;
                    UpdateContact.ContactId = _webstoreAuthorizationChecker.loginContactID();
                    result = _companyService.UpdateCompanyContactForCorporate(UpdateContact);
                }
              
                UserCookieManager.WEBContactFirstName = FirstName;

                UserCookieManager.WEBContactLastName = LastName;

                //JsonSerializerSettings jSettings = new Newtonsoft.Json.JsonSerializerSettings();
                //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = jSettings;

                //return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            catch (Exception ex)
            {
                throw ex;
            }

           
        }

        private string UpdateImage(HttpPostedFile Request)
        {
            string ImagePath = string.Empty;
            CompanyContact contact = _companyService.GetContactByID(_webstoreAuthorizationChecker.loginContactID());
            if (Request != null)
            {
                string folderPath = "/mpc_content/Assets" + "/" + UserCookieManager.WEBOrganisationID + "/" + UserCookieManager.WBStoreId + "/Contacts/" + contact.ContactId + "";
                string virtualFolderPth = string.Empty;

                // virtualFolderPth = @Server.MapPath(folderPath);
                //  virtualFolderPth = Request.MapPath(folderPath);
                virtualFolderPth = HttpContext.Current.Server.MapPath(folderPath);
                /// virtualFolderPth = System.Web.Http.HttpServer.
                if (!System.IO.Directory.Exists(virtualFolderPth))
                {
                    System.IO.Directory.CreateDirectory(virtualFolderPth);
                }
                if (contact.image != null || contact.image != "")
                {
                    RemovePreviousFile(contact.image);
                }
                var fileName = Path.GetFileName(Request.FileName);
                Request.SaveAs(virtualFolderPth + "/" + fileName);
                ImagePath = folderPath + "/" + fileName;
            }
            else
            {
                ImagePath = contact.image;
            }

            return ImagePath;
        }
        [HttpPost]
        public HttpPostedFile ImageFile()
        {
            HttpPostedFile file = null;
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                // Get the uploaded image from the Files collection
                var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                if (httpPostedFile != null)
                {
                    file = httpPostedFile;
                }

            }
            return file;
        }

        private void RemovePreviousFile(string previousFileToremove)
        {
            if (!string.IsNullOrEmpty(previousFileToremove))
            {
                string ServerPath = HttpContext.Current.Server.MapPath(previousFileToremove);
                if (System.IO.File.Exists(ServerPath))
                {
                    Utils.DeleteFile(ServerPath);
                }
            }
        }

    }
}
