using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class RequestQuoteController : Controller
    {
        // GET: RequestQuote
        private readonly ICompanyService _myCompanyService;
        private readonly IWebstoreClaimsHelperService _webstoreAuthorizationChecker;
        private readonly ICampaignService _campaignService;
        private readonly IUserManagerService _usermanagerService;
        private readonly IItemService _ItemService;
        private readonly ICompanyContactRepository _companyContact;


        public RequestQuoteController(ICompanyService myCompanyService, IWebstoreClaimsHelperService webstoreAuthorizationChecker, ICampaignService _campaignService, IUserManagerService _usermanagerService
            , IItemService ItemService, ICompanyContactRepository _companyContact)
        {

            this._myCompanyService = myCompanyService;
            this._webstoreAuthorizationChecker = webstoreAuthorizationChecker;
            this._campaignService = _campaignService;
            this._usermanagerService = _usermanagerService;
            this._ItemService = ItemService;
            this._companyContact = _companyContact;
        }
        public ActionResult Index()
        {
            RequestQuote Quote = new RequestQuote();
            if (_webstoreAuthorizationChecker.loginContactID() > 0)
            {
                ViewBag.LoginStatus = true;
            }

            return View("PartialViews/RequestQuote", Quote);
            
        }
        [HttpPost]
        public ActionResult Index(RequestQuote Model, HttpPostedFileBase uploadFile, string hfNoOfRec)
        {
             string CacheKeyName = "CompanyBaseResponse";
                    ObjectCache cache = MemoryCache.Default;

             MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
            Inquiry NewInqury = new Inquiry();

            NewInqury.Title = Model.Title;

            if (_webstoreAuthorizationChecker.loginContactID() > 0)
            {
                NewInqury.ContactId = _webstoreAuthorizationChecker.loginContactID();
                NewInqury.CompanyId = (int)_webstoreAuthorizationChecker.loginContactCompanyID();

            }
            else
            {
               
                CompanyContact Contact = new CompanyContact();
                Contact.FirstName = Model.FirstName;
                Contact.LastName = Model.LastName;
                Contact.Email = Model.Email;
                Contact.Mobile = Model.Mobile;
                Contact.Password = "password";
                Campaign RegistrationCampaign = _campaignService.GetCampaignRecordByEmailEvent((int)Events.Registration, StoreBaseResopnse.Company.OrganisationId ?? 0, UserCookieManager.WBStoreId);

                long Customer = _myCompanyService.CreateCustomer(Model.FirstName, false, false, CompanyTypes.SalesCustomer, string.Empty, 0, StoreBaseResopnse.Company.CompanyId , Contact);


                if (Customer > 0)
                {
                   
                    MPC.Models.DomainModels.Company loginUserCompany = _myCompanyService.GetCompanyByCompanyID(StoreBaseResopnse.Company.OrganisationId ?? 0);
                    CompanyContact UserContact = _myCompanyService.GetContactByID(_webstoreAuthorizationChecker.loginContactID());
                    CampaignEmailParams cep = new CampaignEmailParams();

                    Campaign RegistrationCampaignn = _campaignService.GetCampaignRecordByEmailEvent((int)Events.RequestAQuote, StoreBaseResopnse.Company.OrganisationId ?? 0, UserCookieManager.WBStoreId);
                    cep.ContactId = NewInqury.ContactId;

                    cep.OrganisationId = 1;
                    cep.AddressId = (int)NewInqury.CompanyId;
                    cep.SalesManagerContactID = _webstoreAuthorizationChecker.loginContactID();
                    cep.StoreId = UserCookieManager.WBStoreId;
                    cep.CompanyId = UserCookieManager.WBStoreId;

                    SystemUser EmailOFSM = _usermanagerService.GetSalesManagerDataByID(StoreBaseResopnse.Company.SalesAndOrderManagerId1.Value);
                    
                    if (UserCookieManager.WEBStoreMode == (int) StoreMode.Retail)
                    {
                        _campaignService.SendEmailToSalesManager((int)Events.NewQuoteToSalesManager, (int)NewInqury.ContactId, (int)NewInqury.CompanyId, 0, UserCookieManager.WEBOrganisationID, 0, StoreMode.Retail, UserCookieManager.WBStoreId, EmailOFSM);

                    }
                    else
                    {
                        _campaignService.SendEmailToSalesManager((int)Events.NewQuoteToSalesManager, (int)NewInqury.ContactId, (int)NewInqury.CompanyId, 0, UserCookieManager.WEBOrganisationID, 0, StoreMode.Corp, UserCookieManager.WBStoreId, EmailOFSM);

                    }
                    

                    _campaignService.emailBodyGenerator(RegistrationCampaignn, cep, UserContact, StoreMode.Retail, (int)loginUserCompany.OrganisationId, "", "", "", EmailOFSM.Email, "", "", null, "");

                }
            }
            NewInqury.CreatedDate = DateTime.Now;
            NewInqury.IsDirectInquiry = false;
            NewInqury.FlagId = null;
            NewInqury.SourceId = 30;

            int iMaxFileSize = 2097152;

            long result = _ItemService.AddInquiryAndItems(NewInqury, FillItems(Model, Convert.ToInt32(hfNoOfRec)));

            long InquiryId = result;
            if (Request != null)
            {
                if (Request.ContentLength < iMaxFileSize)
                {
                    FillAttachments(result, uploadFile);
                }
                else
                { 
                   
                }
            }
            if (result > 0)
            {

                MPC.Models.DomainModels.Company loginUserCompany = _myCompanyService.GetCompanyByCompanyID(_webstoreAuthorizationChecker.loginContactCompanyID());

                CompanyContact UserContact = _myCompanyService.GetContactByID(_webstoreAuthorizationChecker.loginContactID());
                CampaignEmailParams cep = new CampaignEmailParams();

                Campaign RegistrationCampaign = _campaignService.GetCampaignRecordByEmailEvent((int)Events.RequestAQuote, StoreBaseResopnse.Company.OrganisationId ?? 0, UserCookieManager.WBStoreId);
                cep.ContactId = NewInqury.ContactId;

                cep.OrganisationId = 1;
                cep.AddressId = (int)NewInqury.CompanyId;
                cep.SalesManagerContactID = _webstoreAuthorizationChecker.loginContactID();
                cep.StoreId = UserCookieManager.WBStoreId;
                cep.CompanyId = UserCookieManager.WBStoreId;

                SystemUser EmailOFSM = _usermanagerService.GetSalesManagerDataByID(loginUserCompany.SalesAndOrderManagerId1.Value);
                
                if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                {
                   
                    long MID = _companyContact.GetContactIdByRole(_webstoreAuthorizationChecker.loginContactCompanyID(), (int)Roles.Manager);
                    cep.CorporateManagerID = MID;
                    int ManagerID = (int)MID;

                    _campaignService.SendEmailToSalesManager((int)Events.NewQuoteToSalesManager, (int)NewInqury.ContactId, (int)NewInqury.CompanyId, 0, UserCookieManager.WEBOrganisationID, ManagerID, StoreMode.Corp, UserCookieManager.WBStoreId, EmailOFSM);
                }
                else
                {

                    _campaignService.SendEmailToSalesManager((int)Events.NewQuoteToSalesManager, (int)NewInqury.ContactId, (int)NewInqury.CompanyId, 0, UserCookieManager.WEBOrganisationID, 0, StoreMode.Retail, UserCookieManager.WBStoreId, EmailOFSM);

                }
                
                _campaignService.emailBodyGenerator(RegistrationCampaign, cep, UserContact, StoreMode.Retail, (int)loginUserCompany.OrganisationId, "", "", "", EmailOFSM.Email, "", "", null, "");
            }
            return View("PartialViews/RequestQuote", Model);
        }

        private List<InquiryItem> FillItems(RequestQuote Model, int hfNoOfRec)
        {
            List<InquiryItem> listOfInquiries = new List<InquiryItem>();
            DateTime requideddate = DateTime.Now;
            if (hfNoOfRec > 0)
            {
                int numOfrec = Convert.ToInt32(hfNoOfRec);

                if (numOfrec >= 1)
                {
                    InquiryItem item1 = new InquiryItem();

                    item1.Title = Model.InquiryItemTitle1;
                    item1.Notes = Model.InquiryItemNotes1;

                    item1.DeliveryDate = Convert.ToDateTime(Model.InquiryItemDeliveryDate1, CultureInfo.InvariantCulture);

                    requideddate = item1.DeliveryDate;

                    listOfInquiries.Add(item1);
                }

                if (numOfrec >= 2)
                {
                    InquiryItem item1 = new InquiryItem();

                    item1.Title = Model.InquiryItemTitle1;
                    item1.Notes = Model.InquiryItemNotes1;

                    item1.DeliveryDate = Convert.ToDateTime(Model.InquiryItemDeliveryDate1, CultureInfo.InvariantCulture);
                    listOfInquiries.Add(item1);

                    InquiryItem item2 = new InquiryItem();

                    item2.Title = Model.InquiryItemTitle2;
                    item2.Notes = Model.InquiryItemNotes2;
                    item2.DeliveryDate = Convert.ToDateTime(Model.InquiryItemDeliveryDate2, CultureInfo.InvariantCulture);

                    if (requideddate > item2.DeliveryDate)
                        requideddate = item2.DeliveryDate;

                       listOfInquiries.Add(item2);
                }

                if (numOfrec >= 3)
                {
                    InquiryItem item1 = new InquiryItem();

                    item1.Title = Model.InquiryItemTitle1;
                    item1.Notes = Model.InquiryItemNotes1;

                    item1.DeliveryDate = Convert.ToDateTime(Model.InquiryItemDeliveryDate1, CultureInfo.InvariantCulture);
                    listOfInquiries.Add(item1);

                    InquiryItem item2 = new InquiryItem();

                    item2.Title = Model.InquiryItemTitle2;
                    item2.Notes = Model.InquiryItemNotes2;
                    item2.DeliveryDate = Convert.ToDateTime(Model.InquiryItemDeliveryDate2, CultureInfo.InvariantCulture);

                    listOfInquiries.Add(item2);


                    InquiryItem item3 = new InquiryItem();

                    item3.Title = Model.InquiryItemTitle3;
                    item3.Notes = Model.InquiryItemNotes3;
                    item3.DeliveryDate = Convert.ToDateTime(Model.InquiryItemDeliveryDate3, CultureInfo.InvariantCulture);

                    if (requideddate > item3.DeliveryDate)
                        requideddate = item3.DeliveryDate;

                       listOfInquiries.Add(item3);
                }

            }
            return listOfInquiries;
        }

        private void FillAttachments(long inquiryID, HttpPostedFileBase Request)
        {
            
            if (Request != null)
            {
                List<InquiryAttachment> listOfAttachment = new List<InquiryAttachment>();
                string folderPath = "/mpc_content/Attachments/" + "/" + UserCookieManager.WEBOrganisationID + "/" + UserCookieManager.WBStoreId + "/" + inquiryID + "";
                string virtualFolderPth = string.Empty;
                
                virtualFolderPth = @Server.MapPath(folderPath);
                if (!System.IO.Directory.Exists(virtualFolderPth))
                    System.IO.Directory.CreateDirectory(virtualFolderPth);

                //for (int i = 0; i < Request.Count; i++)
                //{
                //HttpPostedFile postedFile = Request;

                string fileName = string.Format("{0}{1}", Guid.NewGuid().ToString(), Path.GetFileName(Request.FileName));

                InquiryAttachment inquiryAttachment = new InquiryAttachment();
                inquiryAttachment.OrignalFileName = Path.GetFileName(Request.FileName);
                inquiryAttachment.Extension = Path.GetExtension(Request.FileName);
                inquiryAttachment.AttachmentPath = "/" + folderPath + fileName;
                inquiryAttachment.InquiryId = Convert.ToInt32(inquiryID);
                listOfAttachment.Add(inquiryAttachment);

                Request.SaveAs(virtualFolderPth + fileName);
                // }

                _ItemService.AddInquiryAttachments(listOfAttachment);
            }


        }

        private Inquiry AddInquiry(Prefix prefix)
        {

            // Get order prefix and update the order next number
            //  tbl_prefixes prefix = PrefixManager.GetDefaultPrefix(context);
            Inquiry inquiry = new Inquiry();

            inquiry.InquiryCode = prefix.EnquiryPrefix + "-001-" + prefix.EnquiryNext.ToString();
            prefix.EnquiryNext = prefix.EnquiryNext + 1;

            return inquiry;

        }

    }
}