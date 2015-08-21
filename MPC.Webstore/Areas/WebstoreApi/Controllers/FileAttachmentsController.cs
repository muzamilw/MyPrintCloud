using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
using MPC.Webstore.Areas.DesignerApi.Controllers;
using MPC.Webstore.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MPC.Webstore.Areas.WebstoreApi.Controllers
{
    public class FileAttachmentsController : ApiController
    {
        private readonly IWebstoreClaimsHelperService _webstoreAuthorizationChecker;
        private readonly IItemService _ItemService;
        private readonly ICompanyService _companyService;
        private readonly ICampaignService _campaignService;
        private readonly IUserManagerService _usermanagerService;
        private readonly ICompanyContactRepository _companyContact;
        public FileAttachmentsController(IWebstoreClaimsHelperService _webstoreAuthorizationChecker, IItemService _ItemService, ICompanyService _companyService, ICampaignService _campaignService, IUserManagerService _usermanagerService, ICompanyContactRepository _companyContact)
        {
          
            this._companyService = _companyService;
            this._webstoreAuthorizationChecker = _webstoreAuthorizationChecker;
            this._campaignService = _campaignService;
            this._usermanagerService = _usermanagerService;
            this._companyContact = _companyContact;
        
        }
        public async Task PostAsync(string FirstName, string LastName, string Email, string Mobile, string Title, string InquiryItemTitle1, string InquiryItemNotes1, string InquiryItemDeliveryDate1, string InquiryItemTitle2, string InquiryItemNotes2, string InquiryItemDeliveryDate2, string InquiryItemTitle3, string InquiryItemNotes3, string InquiryItemDeliveryDate3, string hfNoOfRec)
        {
            if (Request.Content.IsMimeMultipartContent())
            {
                string uploadPath = HttpContext.Current.Server.MapPath("~/UploadedFile");

                MyStreamProvider streamProvider = new MyStreamProvider(uploadPath);

                await Request.Content.ReadAsMultipartAsync(streamProvider);

                //foreach (var file in streamProvider.FileData)
                //{
                //    FileInfo fi = new FileInfo(file.LocalFileName);
                //    messages.Add("File uploaded as " + fi.FullName + " (" + fi.Length + " bytes)");
                //}

                //return messages;
            }
      
            var httpPostedFile = HttpContext.Current.Request.Files["UploadedFile"];
            Inquiry NewInqury = new Inquiry();

            NewInqury.Title = Title;

            if (_webstoreAuthorizationChecker.loginContactID() > 0)
            {
                NewInqury.ContactId = _webstoreAuthorizationChecker.loginContactID();
                NewInqury.CompanyId = (int)_webstoreAuthorizationChecker.loginContactCompanyID();

            }
            else
            {
                if (_companyContact.GetContactByEmailID(Email) != null)
                {
                    return;
                }
                CompanyContact Contact = new CompanyContact();
                Contact.FirstName = FirstName;
                Contact.LastName = LastName;
                Contact.Email = Email;
                Contact.Mobile = Mobile;
                Contact.Password = "password";
                Campaign RegistrationCampaign = _campaignService.GetCampaignRecordByEmailEvent((int)Events.Registration, UserCookieManager.WEBOrganisationID, UserCookieManager.WBStoreId);

                long Customer = 0;// _companyService.CreateCustomer(FirstName, false, false, CompanyTypes.SalesCustomer, string.Empty, 0, StoreId,Contact);

                if (Customer > 0)
                {

                    MPC.Models.DomainModels.Company loginUserCompany = _companyService.GetCompanyByCompanyID(UserCookieManager.WEBOrganisationID);
                    CompanyContact UserContact = _companyService.GetContactByID(_webstoreAuthorizationChecker.loginContactID());
                    CampaignEmailParams cep = new CampaignEmailParams();

                    Campaign RegistrationCampaignn = _campaignService.GetCampaignRecordByEmailEvent((int)Events.RequestAQuote, UserCookieManager.WEBOrganisationID, UserCookieManager.WBStoreId);
                    cep.ContactId = NewInqury.ContactId;

                    cep.OrganisationId = 1;
                    cep.AddressId = (int)NewInqury.CompanyId;
                    cep.SalesManagerContactID = _webstoreAuthorizationChecker.loginContactID();
                    cep.StoreId = UserCookieManager.WBStoreId;

                    SystemUser EmailOFSM = _usermanagerService.GetSalesManagerDataByID(loginUserCompany.SalesAndOrderManagerId1.Value);

                    if (UserCookieManager.WEBStoreMode == (int)StoreMode.Retail)
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
            long result = _ItemService.AddInquiryAndItems(NewInqury, FillItems(InquiryItemDeliveryDate1, InquiryItemDeliveryDate2, InquiryItemDeliveryDate3, InquiryItemTitle1, InquiryItemNotes1, InquiryItemTitle2, InquiryItemNotes2, InquiryItemTitle3, InquiryItemNotes3, Convert.ToInt32(hfNoOfRec)));
            long InquiryId = result;

            if (Request != null)
            {
                if (HttpContext.Current.Request.ContentLength < iMaxFileSize)
                {
                    if (Request.Content.IsMimeMultipartContent())
                    {
                        string uploadPath = HttpContext.Current.Server.MapPath("~/UploadedFile");

                        MyStreamProvider streamProvider = new MyStreamProvider(uploadPath);

                        await Request.Content.ReadAsMultipartAsync(streamProvider);
                        FillAttachments(result, streamProvider);
                    }

                }
                else { 
                
                }
            }
            if (result > 0)
            {

                MPC.Models.DomainModels.Company loginUserCompany = _companyService.GetCompanyByCompanyID(_webstoreAuthorizationChecker.loginContactCompanyID());
                CompanyContact UserContact = _companyService.GetContactByID(_webstoreAuthorizationChecker.loginContactID());
                CampaignEmailParams cep = new CampaignEmailParams();

                Campaign RegistrationCampaign = _campaignService.GetCampaignRecordByEmailEvent((int)Events.RequestAQuote, UserCookieManager.WEBOrganisationID, UserCookieManager.WBStoreId);
                cep.ContactId = NewInqury.ContactId;

                cep.OrganisationId = 1;
                cep.AddressId = (int)NewInqury.CompanyId;
                cep.SalesManagerContactID = _webstoreAuthorizationChecker.loginContactID();
                cep.StoreId = UserCookieManager.WBStoreId;
                Company GetCompany = _companyService.GetCompanyByCompanyID(UserCookieManager.WBStoreId);
                //string CacheKeyName = "CompanyBaseResponse";
                //ObjectCache cache = MemoryCache.Default;
                //MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
                MyCompanyDomainBaseReponse StoreBaseResopnse = _companyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

                SystemUser EmailOFSM = _usermanagerService.GetSalesManagerDataByID(StoreBaseResopnse.Company.SalesAndOrderManagerId1.Value);

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

        }
        private void FillAttachments(long inquiryID, MyStreamProvider Obj)
        {
            if (Request != null)
            {
                List<InquiryAttachment> listOfAttachment = new List<InquiryAttachment>();
                string folderPath = "/mpc_content/Attachments/" + "/" + UserCookieManager.WEBOrganisationID + "/" + UserCookieManager.WBStoreId + "/" + inquiryID + "";
                string virtualFolderPth = string.Empty;

                virtualFolderPth = HttpContext.Current.Server.MapPath(folderPath);
                if (!System.IO.Directory.Exists(virtualFolderPth))
                    System.IO.Directory.CreateDirectory(virtualFolderPth);

                foreach (var file in Obj.FileData)
                {
                 //   HttpPostedFile postedFile = HttpContext.Current.Request.Files[i];
                    FileInfo fi = new FileInfo(file.LocalFileName);

                    string fileName = string.Format("{0}{1}", Guid.NewGuid().ToString(), Path.GetFileName(fi.FullName));

                    InquiryAttachment inquiryAttachment = new InquiryAttachment();
                    //inquiryAttachment.OrignalFileName = Path.GetFileName(Request.FileName);
                    //inquiryAttachment.Extension = Path.GetExtension(Request.FileName);
                    //inquiryAttachment.AttachmentPath = "/" + folderPath + fileName;
                    //inquiryAttachment.InquiryId = Convert.ToInt32(inquiryID);
                    //listOfAttachment.Add(inquiryAttachment);
                    //Request.SaveAs(virtualFolderPth + fileName);

                    _ItemService.AddInquiryAttachments(listOfAttachment);
                }
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
        private List<InquiryItem> FillItems(string InquiryItemDeliveryDate1, string InquiryItemDeliveryDate2, string InquiryItemDeliveryDate3, string InquiryItemTitle1, string InquiryItemNotes1, string InquiryItemTitle2, string InquiryItemNotes2, string InquiryItemTitle3, string InquiryItemNotes3, int hfNoOfRec)
        {
            List<InquiryItem> listOfInquiries = new List<InquiryItem>();
            DateTime requideddate = DateTime.Now;
            if (hfNoOfRec > 0)
            {
                int numOfrec = Convert.ToInt32(hfNoOfRec);

                if (numOfrec >= 1)
                {
                    InquiryItem item1 = new InquiryItem();

                    item1.Title = InquiryItemTitle1;
                    item1.Notes = InquiryItemNotes1;

                    item1.DeliveryDate = Convert.ToDateTime(InquiryItemDeliveryDate1, CultureInfo.InvariantCulture);

                    requideddate = item1.DeliveryDate;

                    listOfInquiries.Add(item1);
                }

                if (numOfrec >= 2)
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

                if (numOfrec >= 3)
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

    }
}
