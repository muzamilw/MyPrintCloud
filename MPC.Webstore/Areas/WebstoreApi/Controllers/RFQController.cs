using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
using MPC.Webstore.Areas.WebstoreApi.Models;
using MPC.Webstore.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Web;
using System.Web.Http;

namespace MPC.Webstore.Areas.WebstoreApi.Controllers
{
    public class RFQController : ApiController
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
        public RFQController(IItemService ItemService, IOrderService _orderService, ICompanyService companyService, IWebstoreClaimsHelperService _webstoreAuthorizationChecker, ICampaignService _campaignService, IUserManagerService _usermanagerService, ICompanyContactRepository _companyContact)
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
        public HttpResponseMessage UpdateDataRequestQuote(string FirstName, string LastName, string Email, string Mobile, string Title, string InquiryItemTitle1, string InquiryItemNotes1, string InquiryItemDeliveryDate1, string InquiryItemTitle2, string InquiryItemNotes2, string InquiryItemDeliveryDate2, string InquiryItemTitle3, string InquiryItemNotes3, string InquiryItemDeliveryDate3, string hfNoOfRec)
        {
            int count = HttpContext.Current.Request.Files.Count;
            int Contentlength = HttpContext.Current.Request.ContentLength;
            //string CacheKeyName = "CompanyBaseResponse";
            //ObjectCache cache = MemoryCache.Default;

            //MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
            MyCompanyDomainBaseReponse StoreBaseResopnse = _companyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            Inquiry NewInqury = new Inquiry();
            CompanyContact loginUser = null;
            NewInqury.Title = Title;
            NewInqury.OrganisationId = UserCookieManager.WEBOrganisationID;
            if (_webstoreAuthorizationChecker.loginContactID() > 0)
            {
                NewInqury.ContactId = _webstoreAuthorizationChecker.loginContactID();
                NewInqury.CompanyId = (int)_webstoreAuthorizationChecker.loginContactCompanyID();
                loginUser = _companyService.GetContactByID(_webstoreAuthorizationChecker.loginContactID());
            }
            else
            {
                long Customer = 0;
                loginUser = _companyService.GetContactByEmail(Email, UserCookieManager.WEBOrganisationID, UserCookieManager.WBStoreId);

                if (loginUser == null) 
                {
                    CompanyContact Contact = new CompanyContact();
                    Contact.FirstName = FirstName;
                    Contact.LastName = LastName;
                    Contact.Email = Email;
                    Contact.Mobile = Mobile;
                    Contact.Password = "password";
                    Campaign RegistrationCampaign = _campaignService.GetCampaignRecordByEmailEvent((int)Events.Registration, UserCookieManager.WEBOrganisationID, UserCookieManager.WBStoreId);


                    Customer = _companyService.CreateCustomer(FirstName + " " + LastName, false, false, CompanyTypes.SalesCustomer, string.Empty, UserCookieManager.WEBOrganisationID, StoreBaseResopnse.Company.CompanyId, Contact);
                    loginUser = _companyService.GetContactByEmail(Email, UserCookieManager.WEBOrganisationID, UserCookieManager.WBStoreId);
                }


                if (loginUser != null)
                {

                  
                    NewInqury.ContactId = loginUser.ContactId;
                    NewInqury.CompanyId = loginUser.CompanyId;
                    
                    // CompanyContact UserContact = _companyService.GetContactByID(loginUser.ContactId); Commented by Naveed on 20160712 also set loginUser as parameter in emailBodyGenerator function instead of UserContact
                    //CampaignEmailParams cep = new CampaignEmailParams();
                    //Campaign RegistrationCampaignn = _campaignService.GetCampaignRecordByEmailEvent((int)Events.RequestAQuote, UserCookieManager.WEBOrganisationID, UserCookieManager.WBStoreId);
                    //cep.ContactId = NewInqury.ContactId;

                    //cep.OrganisationId = 1;
                    
                    //cep.AddressId = NewInqury.CompanyId??0;
                    //cep.SalesManagerContactID = loginUser.ContactId;
                    //cep.StoreId = UserCookieManager.WBStoreId;
                    //cep.SystemUserId = StoreBaseResopnse.Company.SalesAndOrderManagerId1.Value;
                    //SystemUser EmailOFSM = _usermanagerService.GetSalesManagerDataByID(StoreBaseResopnse.Company.SalesAndOrderManagerId1.Value);
                  
                    //if (UserCookieManager.WEBStoreMode == (int)StoreMode.Retail)
                    //{
                    //    _campaignService.SendEmailToSalesManager((int)Events.NewQuoteToSalesManager, (int)NewInqury.ContactId,NewInqury.CompanyId??0, 0, UserCookieManager.WEBOrganisationID, 0, StoreMode.Retail, UserCookieManager.WBStoreId, EmailOFSM);

                    //}
                    //else
                    //{
                    //    _campaignService.SendEmailToSalesManager((int)Events.NewQuoteToSalesManager, (int)NewInqury.ContactId,NewInqury.CompanyId??0, 0, UserCookieManager.WEBOrganisationID, 0, StoreMode.Corp, UserCookieManager.WBStoreId, EmailOFSM);

                    //}

                    //_campaignService.emailBodyGenerator(RegistrationCampaignn, cep, loginUser, StoreMode.Retail, (int)UserCookieManager.WEBOrganisationID, "", "", "", EmailOFSM.Email, "", "", null, "");

                }
            }

            NewInqury.CreatedDate = DateTime.Now;
            NewInqury.IsDirectInquiry = false;
            NewInqury.FlagId = null;
            NewInqury.SourceId = (int)PipelineSource.WebtoPrintSite;
            NewInqury.SystemUserId = StoreBaseResopnse.Company.SalesAndOrderManagerId1.Value;
            NewInqury.Status = 25; // unproccessed status of inquiry
            int iMaxFileSize = 50000000;
            int InquiryId = _ItemService.AddInquiryAndItems(NewInqury, FillItems(InquiryItemDeliveryDate1, InquiryItemDeliveryDate2, InquiryItemDeliveryDate3, InquiryItemTitle1, InquiryItemNotes1, InquiryItemTitle2, InquiryItemNotes2, InquiryItemTitle3, InquiryItemNotes3, Convert.ToInt32(hfNoOfRec)));
            

            if (Request != null)
            {
                if (HttpContext.Current.Request.ContentLength < iMaxFileSize)
                {
                    FillAttachments(InquiryId);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,"gs");
                }
            }
            if (InquiryId > 0)
            {

               // MPC.Models.DomainModels.Company loginUserCompany = _companyService.GetCompanyByCompanyID(_webstoreAuthorizationChecker.loginContactCompanyID()); Commented by Naveed: as its not being used
                CompanyContact UserContact = loginUser ?? _companyService.GetContactByID(_webstoreAuthorizationChecker.loginContactID()); // Conditional change with loginUser by Naveed: 20160712
                CampaignEmailParams cep = new CampaignEmailParams();

                Campaign RegistrationCampaign = _campaignService.GetCampaignRecordByEmailEvent((int)Events.RequestAQuote, UserCookieManager.WEBOrganisationID, UserCookieManager.WBStoreId);
                cep.ContactId = NewInqury.ContactId;
                cep.InquiryId = InquiryId;
                cep.OrganisationId = 1;
                cep.AddressId = loginUser != null ? loginUser.AddressId : NewInqury.CompanyId ?? 0;
                cep.SalesManagerContactID = loginUser != null ? loginUser.ContactId : 0;
                cep.StoreId = UserCookieManager.WBStoreId;
                cep.CompanyId = UserCookieManager.WBStoreId;
                cep.SystemUserId = StoreBaseResopnse.Company.SalesAndOrderManagerId1.Value;
                //Company GetCompany = _companyService.GetCompanyByCompanyID(UserCookieManager.WBStoreId);Commented by Naveed: as its not being used

                SystemUser EmailOFSM = _usermanagerService.GetSalesManagerDataByID(StoreBaseResopnse.Company.SalesAndOrderManagerId1.Value);
                if (EmailOFSM != null) 
                {
                    if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                    {

                        long MID = _companyContact.GetContactIdByRole(_webstoreAuthorizationChecker.loginContactCompanyID(), (int)Roles.Manager);
                        cep.CorporateManagerID = MID;
                        int ManagerID = (int)MID;

                        _campaignService.SendEmailToSalesManager((int)Events.NewQuoteToSalesManager, (int)NewInqury.ContactId, (int)NewInqury.CompanyId, 0, UserCookieManager.WEBOrganisationID, ManagerID, StoreMode.Corp, UserCookieManager.WBStoreId, EmailOFSM, "", "", InquiryId);
                    }
                    else
                    {

                        _campaignService.SendEmailToSalesManager((int)Events.NewQuoteToSalesManager, (int)NewInqury.ContactId, (int)NewInqury.CompanyId, 0, UserCookieManager.WEBOrganisationID, 0, StoreMode.Retail, UserCookieManager.WBStoreId, EmailOFSM, "", "", InquiryId);

                    }

                    _campaignService.emailBodyGenerator(RegistrationCampaign, cep, UserContact, StoreMode.Retail, (int)UserCookieManager.WEBOrganisationID, "", "", "", EmailOFSM.Email, "", "", null, "");
                }
                
            }

            
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        
      
        private void FillAttachments(long inquiryID)
        {
            if (HttpContext.Current.Request != null)
            {
                List<InquiryAttachment> listOfAttachment = new List<InquiryAttachment>();
                string folderPath = "mpc_content/Attachments/" + UserCookieManager.WEBOrganisationID + "/" + UserCookieManager.WBStoreId + "/" + inquiryID + "/";
                string virtualFolderPth = string.Empty;

                string folderPathToMap = "/" + folderPath;

                virtualFolderPth = HttpContext.Current.Server.MapPath(folderPathToMap);
                if (!System.IO.Directory.Exists(virtualFolderPth))
                    System.IO.Directory.CreateDirectory(virtualFolderPth);

                for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                {
                    //HttpPostedFile postedFile = HttpContext.Current.Request.Files[i];
                    HttpPostedFile postedFile = HttpContext.Current.Request.Files["UploadedFile" + i];

                    string fileName = string.Format("{0}{1}", i, Path.GetFileName(postedFile.FileName));

                    InquiryAttachment inquiryAttachment = new InquiryAttachment();
                    inquiryAttachment.OrignalFileName = fileName;
                    inquiryAttachment.Extension = Path.GetExtension(postedFile.FileName);
                    inquiryAttachment.AttachmentPath = "/" + folderPath; //+ fileName;
                    inquiryAttachment.InquiryId = Convert.ToInt32(inquiryID);
                    listOfAttachment.Add(inquiryAttachment);
                    string filevirtualpath = virtualFolderPth + "/" + fileName;
                    postedFile.SaveAs(virtualFolderPth + "/" + fileName);
                }
                _ItemService.AddInquiryAttachments(listOfAttachment);
            }

        }
        private Inquiry AddInquiry(Prefix prefix)
        {
            Inquiry inquiry = new Inquiry();

            inquiry.InquiryCode = prefix.EnquiryPrefix + "-001-" + prefix.EnquiryNext.ToString();
            prefix.EnquiryNext = prefix.EnquiryNext + 1;

            return inquiry;
        }
        private List<InquiryItem> FillItems(string InquiryItemDeliveryDate1, string InquiryItemDeliveryDate2, string InquiryItemDeliveryDate3, string InquiryItemTitle1, string InquiryItemNotes1, string InquiryItemTitle2, string InquiryItemNotes2, string InquiryItemTitle3, string InquiryItemNotes3, int hfNoOfRec)
        {
            List<InquiryItem> listOfInquiries = new List<InquiryItem>();
            DateTime requideddate = DateTime.Now;
            if (hfNoOfRec > 0)
            {
                int numOfrec = Convert.ToInt32(hfNoOfRec);
                if (numOfrec > 3)
                {
                    numOfrec = 3;
                }
                if (numOfrec == 1)
                {
                    InquiryItem item1 = new InquiryItem();

                    item1.Title = InquiryItemTitle1;
                    item1.Notes = InquiryItemNotes1;

                    item1.DeliveryDate = Convert.ToDateTime(InquiryItemDeliveryDate1, CultureInfo.InvariantCulture);

                    requideddate = item1.DeliveryDate;

                    listOfInquiries.Add(item1);
                }

                if (numOfrec == 2)
                {
                    InquiryItem item1 = new InquiryItem();

                    item1.Title = InquiryItemTitle1;
                    item1.Notes = InquiryItemNotes1;

                    item1.DeliveryDate = Convert.ToDateTime(InquiryItemDeliveryDate1, CultureInfo.InvariantCulture);
                    listOfInquiries.Add(item1);

                    InquiryItem item2 = new InquiryItem();

                    item2.Title = InquiryItemTitle2;
                    item2.Notes = InquiryItemNotes2;
                    item2.DeliveryDate = Convert.ToDateTime(InquiryItemDeliveryDate2, CultureInfo.InvariantCulture);

                    if (requideddate > item2.DeliveryDate)
                        requideddate = item2.DeliveryDate;

                    listOfInquiries.Add(item2);
                }

                if (numOfrec == 3)
                {
                    InquiryItem item1 = new InquiryItem();

                    item1.Title = InquiryItemTitle1;
                    item1.Notes = InquiryItemNotes1;

                    item1.DeliveryDate = Convert.ToDateTime(InquiryItemDeliveryDate1, CultureInfo.InvariantCulture);
                    listOfInquiries.Add(item1);

                    InquiryItem item2 = new InquiryItem();

                    item2.Title = InquiryItemTitle2;
                    item2.Notes = InquiryItemNotes2;
                    item2.DeliveryDate = Convert.ToDateTime(InquiryItemDeliveryDate2, CultureInfo.InvariantCulture);

                    listOfInquiries.Add(item2);


                    InquiryItem item3 = new InquiryItem();

                    item3.Title = InquiryItemTitle3;
                    item3.Notes = InquiryItemNotes3;
                    item3.DeliveryDate = Convert.ToDateTime(InquiryItemDeliveryDate3, CultureInfo.InvariantCulture);

                    if (requideddate > item3.DeliveryDate)
                        requideddate = item3.DeliveryDate;

                    listOfInquiries.Add(item3);
                }

            }
            return listOfInquiries;
        }

        [HttpPost]
        public string SubmitRFQForm(RFQContactForm rfqForm)
        {
            string Message = "";
            try
            {
                string smtpUser = null;
                string smtpserver = null;
                string smtpPassword = null;
                string fromName = null;
                string fromEmail = null;

                MyCompanyDomainBaseReponse StoreBaseResopnse = _companyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

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

                SystemUser salesManager = _companyService.GetSystemUserById(StoreBaseResopnse.Company.SalesAndOrderManagerId1.Value);

                StoreName = StoreBaseResopnse.StoreDetaultAddress.AddressName;


                MesgBody += Utils.GetKeyValueFromResourceFile("ltrlDear", UserCookieManager.WBStoreId, "Dear") + salesManager.FullName + ",<br>";
                MesgBody += Utils.GetKeyValueFromResourceFile("ltrlinqsub", UserCookieManager.WBStoreId, "An enquiry has been submitted to you with the details:") + "<br>";
                MesgBody += Utils.GetKeyValueFromResourceFile("lblContactFormFullName", UserCookieManager.WBStoreId, "Name: ") + rfqForm.FullName + "<br>";
                MesgBody += Utils.GetKeyValueFromResourceFile("lblContactFormMobile", UserCookieManager.WBStoreId, "Mobile: ") + rfqForm.Mobile + "<br>";
                MesgBody += Utils.GetKeyValueFromResourceFile("lblContactFormQuantity", UserCookieManager.WBStoreId, "Quantity: ") + rfqForm.Quantity + "<br>";
                MesgBody += Utils.GetKeyValueFromResourceFile("lblContactFormEmail", UserCookieManager.WBStoreId, "Email: ") + rfqForm.Email + "<br>";
                MesgBody += Utils.GetKeyValueFromResourceFile("lblContactFormMessage", UserCookieManager.WBStoreId, "Message: ") + rfqForm.Message + "<br>";

                bool result = _campaignService.AddMsgToTblQueue(salesManager.Email, "", salesManager.FullName, MesgBody, fromName, fromEmail, smtpUser, smtpPassword, smtpserver, " Contact enquiry from " + StoreName, null, 0, StoreBaseResopnse.Organisation.OrganisationId);

                if (result)
                {
                    rfqForm.FullName = "";
                    rfqForm.Mobile = "";
                    rfqForm.Quantity = "";
                    rfqForm.Email = "";
                    rfqForm.Message = "";
                    Message = Utils.GetKeyValueFromResourceFile("ltrlsubmitt", UserCookieManager.WBStoreId, "Thank you for submitting a request. Someone will contact you shortly.");
                }
                else
                {
                    Message = "An error occured. Please try again.";
                }

            }
            catch (Exception ex)
            {
                throw ex;

            }
            return Message;
        }
    }
}
