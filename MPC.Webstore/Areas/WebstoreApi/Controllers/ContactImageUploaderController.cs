using MPC.Common;
using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
using MPC.Webstore.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
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
        private readonly IListingService listingService;
        private readonly MPC.Implementation.MISServices.CompanyService myCompanyService;
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        private readonly IOrderService _orderService;
        public ContactImageUploaderController(IItemService ItemService, IOrderService _orderService, ICompanyService companyService, IWebstoreClaimsHelperService _webstoreAuthorizationChecker, ICampaignService _campaignService, IUserManagerService _usermanagerService, ICompanyContactRepository _companyContact, IListingService listingService)
        {
            
            this._ItemService = ItemService;
            this._orderService = _orderService;
            this._companyService = companyService;
            this._webstoreAuthorizationChecker = _webstoreAuthorizationChecker;
            this._campaignService = _campaignService;
            this._usermanagerService = _usermanagerService;
            this._companyContact = _companyContact;
            this.listingService = listingService;
           
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
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage UpdateDataForSystemUser(long? CreditLimit, int ContactRoleId, string Email, string Fax, string FirstName, string HomeTel1, bool? isWebAccess, bool? isPlaceOrder, bool? IsPayByPersonalCreditCard, bool? IsPricingshown, string JobTitle, string LastName, string Mobile, string Notes, int QuestionId, string SecretAnswer, long TerritoryId, long AddressId, long ShippingAddressId, string Password, long ContactId, bool IsSymoblicPhoneNumber, bool IsSymobolicDirectLine, bool IsSymbolicMobile, bool CheckEmail)
        {
            try
            {
                string Message = string.Empty;
                CompanyContact ExistingContact = _companyService.GetCorporateContactByEmail(Email, UserCookieManager.WEBOrganisationID, UserCookieManager.WBStoreId);
                if (CheckEmail == true)
                {
                    if (ExistingContact != null)
                    {
                        var formatter = new JsonMediaTypeFormatter();
                        var json = formatter.SerializerSettings;
                        json.Formatting = Newtonsoft.Json.Formatting.Indented;
                        json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        Message = Utils.GetKeyValueFromResourceFile("ltrlExistsContact", UserCookieManager.WBStoreId, "Sorry, There is already Exits a contact with this Email");

                        return Request.CreateResponse(HttpStatusCode.OK, Message, formatter);
                    }
                    else
                    {
                        Message = "Ok";
                        var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                        CompanyContact con = new CompanyContact();
                        con.FirstName = FirstName;
                        con.LastName = LastName;
                        con.ContactId = ContactId;
                        con.image = UpdateImage(httpPostedFile);
                        con.CreditLimit = CreditLimit;
                        con.ContactRoleId = ContactRoleId;
                        con.Email = Email;
                        if (IsSymoblicPhoneNumber == true)
                        {
                            con.FAX = "+" + Fax;
                        }
                        else
                        {
                            con.FAX = Fax;

                        }
                        con.FirstName = FirstName;
                        if (IsSymobolicDirectLine == true)
                        {
                            con.HomeTel1 = "+" + HomeTel1;
                        }
                        else
                        {
                            con.HomeTel1 = HomeTel1;
                        }
                        con.isWebAccess = isWebAccess;
                        con.isArchived = false;
                        con.isPlaceOrder = isPlaceOrder;
                        con.IsPayByPersonalCreditCard = IsPayByPersonalCreditCard;
                        con.IsPricingshown = IsPricingshown;
                        con.JobTitle = JobTitle;
                        if (IsSymbolicMobile == true)
                        {
                            con.Mobile = "+" + Mobile;
                        }
                        else
                        {
                            con.Mobile = Mobile;
                        }
                        con.Notes = Notes;
                        con.QuestionId = QuestionId;
                        con.SecretAnswer = SecretAnswer;
                        con.TerritoryId = TerritoryId;
                        con.AddressId = AddressId;
                        con.ShippingAddressId = ShippingAddressId;
                        con.Password = Password;
                        con.SecretQuestion = QuestionId.ToString();
                        con.OrganisationId = UserCookieManager.WEBOrganisationID;
                        _companyService.UpdateDataSystemUser(con);
                    }
                }
                else
                {
                    Message = "Ok";
                    var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                    CompanyContact con = new CompanyContact();
                    con.FirstName = FirstName;
                    con.LastName = LastName;
                    con.ContactId = ContactId;
                    con.image = UpdateImage(httpPostedFile);
                    con.CreditLimit = CreditLimit;
                    con.ContactRoleId = ContactRoleId;
                    con.Email = Email;
                    if (IsSymoblicPhoneNumber == true)
                    {
                        con.FAX = "+" + Fax;
                    }
                    else
                    {
                        con.FAX = Fax;

                    }
                    con.FirstName = FirstName;
                    if (IsSymobolicDirectLine == true)
                    {
                        con.HomeTel1 = "+" + HomeTel1;
                    }
                    else
                    {
                        con.HomeTel1 = HomeTel1;
                    }
                    con.isWebAccess = isWebAccess;
                    con.isArchived = false;
                    con.isPlaceOrder = isPlaceOrder;
                    con.IsPayByPersonalCreditCard = IsPayByPersonalCreditCard;
                    con.IsPricingshown = IsPricingshown;
                    con.JobTitle = JobTitle;
                    if (IsSymbolicMobile == true)
                    {
                        con.Mobile = "+" + Mobile;
                    }
                    else
                    {
                        con.Mobile = Mobile;
                    }
                    con.Notes = Notes;
                    con.QuestionId = QuestionId;
                    con.SecretAnswer = SecretAnswer;
                    con.TerritoryId = TerritoryId;
                    con.AddressId = AddressId;
                    con.ShippingAddressId = ShippingAddressId;
                    con.Password = Password;
                    con.SecretQuestion = QuestionId.ToString();
                    con.OrganisationId = UserCookieManager.WEBOrganisationID;
                    _companyService.UpdateDataSystemUser(con);
                }
                var formatter1 = new JsonMediaTypeFormatter();
                var json1 = formatter1.SerializerSettings;
                json1.Formatting = Newtonsoft.Json.Formatting.Indented;
                json1.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                return Request.CreateResponse(HttpStatusCode.OK, Message, formatter1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage AddDataForSystemUser(long? CreditLimit, int? ContactRoleId, string Email, string Fax, string FirstName, string HomeTel1, bool? isWebAccess, bool? isPlaceOrder, bool? IsPayByPersonalCreditCard, bool? IsPricingshown, string JobTitle, string LastName, string Mobile, string Notes, int? QuestionId, string SecretAnswer, long? TerritoryId, long AddressId, long? ShippingAddressId, string Password, long ContactId, bool IsSymoblicPhoneNumber, bool IsSymobolicDirectLine, bool IsSymbolicMobile)
        {
            
            string Message = string.Empty;
            
           CompanyContact ExistingContact= _companyService.GetCorporateContactByEmail(Email, UserCookieManager.WEBOrganisationID, UserCookieManager.WBStoreId);
            var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
            if (ExistingContact!= null)
            {
                var formatter = new JsonMediaTypeFormatter();
                var json = formatter.SerializerSettings;
                json.Formatting = Newtonsoft.Json.Formatting.Indented;
                json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                Message = Utils.GetKeyValueFromResourceFile("ltrlExistsContact", UserCookieManager.WBStoreId, "Sorry, There is already Exits a contact with this Email");

                return Request.CreateResponse(HttpStatusCode.OK, Message, formatter);
            }
            else
            {
                Message = "Ok";
                CompanyContact NewContact = new CompanyContact();
                NewContact.CompanyId = UserCookieManager.WBStoreId;
                NewContact.OrganisationId = UserCookieManager.WEBOrganisationID;
                NewContact.isWebAccess = true;
                NewContact.image = UpdateImage(httpPostedFile);
                NewContact.CreditLimit = CreditLimit;
                NewContact.ContactRoleId = ContactRoleId;
                NewContact.Email = Email;
                if (IsSymoblicPhoneNumber == true)
                {
                    NewContact.FAX = "+" + Fax;
                }
                else
                {
                    NewContact.FAX = Fax;
                }
                
                NewContact.FirstName = FirstName;
                if (IsSymobolicDirectLine == true)
                {
                    NewContact.HomeTel1 = "+" + HomeTel1;
                }
                else
                {
                    NewContact.HomeTel1 = HomeTel1;
                }
                NewContact.isWebAccess = isWebAccess;
                NewContact.isArchived = false;
                NewContact.isPlaceOrder = isPlaceOrder;
                NewContact.IsPayByPersonalCreditCard = IsPayByPersonalCreditCard;
                NewContact.IsPricingshown = IsPricingshown;
                NewContact.JobTitle = JobTitle;
                NewContact.LastName = LastName;
                if (IsSymbolicMobile == true)
                {
                    NewContact.Mobile = "+" + Mobile;
                }
                else
                {
                    NewContact.Mobile = "+" + Mobile;
                }
                NewContact.Notes = Notes;
                NewContact.QuestionId = QuestionId;
                NewContact.SecretAnswer = SecretAnswer;
                NewContact.TerritoryId = TerritoryId;
                NewContact.AddressId = AddressId;
                NewContact.ShippingAddressId = ShippingAddressId;
                NewContact.Password = Password;
                
                _companyService.AddDataSystemUser(NewContact);
                if (isWebAccess == true)
                {
                    MyCompanyDomainBaseReponse StoreBaseResopnse = _companyService.GetStoreCachedObject(UserCookieManager.WBStoreId);
                    CampaignEmailParams cep = new CampaignEmailParams();
                    SystemUser EmailOFSM = _usermanagerService.GetSalesManagerDataByID(StoreBaseResopnse.Company.SalesAndOrderManagerId1.Value);


                    cep.ContactId = NewContact.ContactId;
                    cep.CompanyId = UserCookieManager.WBStoreId;
                    cep.SalesManagerContactID = NewContact.ContactId;

                    if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                    {
                        cep.AddressId = UserCookieManager.WBStoreId;
                        cep.StoreId = UserCookieManager.WBStoreId;
                    }
                    else
                    {
                        cep.AddressId = UserCookieManager.WBStoreId;
                        cep.StoreId = UserCookieManager.WBStoreId;
                    }


                    Campaign SuccessCampaign = _campaignService.GetCampaignRecordByEmailEvent((int)Events.CorpUserSuccessfulRegistration, UserCookieManager.WEBOrganisationID, UserCookieManager.WBStoreId);
                    _campaignService.emailBodyGenerator(SuccessCampaign, cep, NewContact, StoreMode.Corp, (int)UserCookieManager.WEBOrganisationID, "", "", "", EmailOFSM.Email, "", "", null, "");

                }
                var formatterr = new JsonMediaTypeFormatter();
                var jsons = formatterr.SerializerSettings;
                jsons.Formatting = Newtonsoft.Json.Formatting.Indented;
                jsons.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                return Request.CreateResponse(HttpStatusCode.OK, Message, formatterr);
            }
        }

      [HttpPost]
        public void SaveListing(string obj)
        {
            ListingProperty listingProperty = JsonConvert.DeserializeObject<ListingProperty>(obj);
            var result = listingService.UpdateListingData(listingProperty);
            //var formatter = new JsonMediaTypeFormatter();
            //var json = formatter.SerializerSettings;
            //json.Formatting = Newtonsoft.Json.Formatting.Indented;
            //json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //return Request.CreateResponse(HttpStatusCode.OK, result, formatter);
        }
      [HttpPost]
      public void AddImagesForListings(long ListingId)
      {
          //  string destinationPath = HostingEnvironment.MapPath("~/StoredImages/RealEstateImages/" + contactCompanyId + "\\" + newlyAddedListing + "\\" + item.ImageID);

             string folderPath = "~/MPC_Content/Stores/" + UserCookieManager.WBStoreId + "/" + ListingId;
                            string drURL = System.Web.HttpContext.Current.Server.MapPath(folderPath);
         
                string virtualFolderPth = drURL;
                            //first download image locally
                            if (!System.IO.Directory.Exists((drURL)))
                                System.IO.Directory.CreateDirectory(drURL);
                   
                           

                            for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                            {
                               
                                HttpPostedFile postedFile = HttpContext.Current.Request.Files["ListingImage" + i];
                                string ImageName = postedFile.FileName;

                                ListingImage tbl_listingImage = new ListingImage();
                                tbl_listingImage.ListingId = ListingId;
                                //tbl_listingImage.ClientImageId = item.ImageID;
                                tbl_listingImage.ImageURL = "/MPC_Content/Stores/" + UserCookieManager.WBStoreId + "/" + ListingId + "/" + ImageName;
                                //tbl_listingImage.ImageOrder = item.ImageOrder;
                                _companyService.ListingImage(tbl_listingImage);
                                //if (!String.IsNullOrEmpty(item.LastMod))
                                //    //tbl_listingImage.LastMode = Convert.ToDateTime(item.LastMode, new System.Globalization.CultureInfo("en-AU"));
                                //    tbl_listingImage.LastMode = DateTime.Parse(item.LastMod, culture, System.Globalization.DateTimeStyles.AssumeLocal);

                                if (!System.IO.Directory.Exists(virtualFolderPth))
                                {
                                    System.IO.Directory.CreateDirectory(virtualFolderPth);
                                }
                                if (tbl_listingImage.ImageURL != null || tbl_listingImage.ImageURL != "")
                                {
                                    RemovePreviousFile(tbl_listingImage.ImageURL);
                                }
                                var fileName = Path.GetFileName(postedFile.FileName);
                                postedFile.SaveAs(virtualFolderPth + "/" + fileName);
                            }
                            
      }
      [HttpPost]
      public void AddFolderData(string FolderName, string Description,long ParentFolderId )
      {
          var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
          Folder NewFolder = new Folder();
          NewFolder.FolderName = FolderName;
          NewFolder.Description = Description;
          
          NewFolder.ParentFolderId = ParentFolderId;
          NewFolder.CompanyId = UserCookieManager.WBStoreId;
          NewFolder.OrganisationId = UserCookieManager.WEBOrganisationID;
          long FolderId= _companyService.AddFolder(NewFolder);
          if (FolderId > 0)
          {
              Folder UpdateImage = new Folder();
              UpdateImage.ImagePath = UpdateFolderImage(httpPostedFile, FolderId,false);
              UpdateImage.FolderId = FolderId;
              _companyService.UpdateImage(UpdateImage);
          }
      }
      [HttpPost]
      public HttpResponseMessage SaveAsset(string AssetName, string Description, string Keywords, long? FolderId, int? Quantity, double? Price)
      {
          string message = string.Empty;
          var httpPostedFile = HttpContext.Current.Request.Files["UploadedImageAsset"];
          Asset Asset = new Asset();
          Asset.AssetName = AssetName;
          Asset.Description = Description; 
          Asset.Keywords = Keywords;
          Asset.FolderId = FolderId;
          Asset.Price = Price;
          Asset.Quantity = Quantity;
          Asset.CompanyId = UserCookieManager.WBStoreId;
          long AsseetId = _companyService.AddAsset(Asset);
          Asset UpdatedAsset = new Asset();

          UpdatedAsset.ImagePath = UpdateAssetImage(httpPostedFile, AsseetId,false);
          UpdatedAsset.AssetId = AsseetId;
          _companyService.UpdateAssetImage(UpdatedAsset);
          string Message = AsseetId.ToString();
          var formatterr = new JsonMediaTypeFormatter();
          var jsons = formatterr.SerializerSettings;
          jsons.Formatting = Newtonsoft.Json.Formatting.Indented;
          jsons.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
          return Request.CreateResponse(HttpStatusCode.OK, Message, formatterr);
      }
      private string UpdateFolderImage(HttpPostedFile Request,long FolderID,bool flag)
      {
          string ImagePath = string.Empty;
        //  CompanyContact contact = _companyService.GetContactByID(_webstoreAuthorizationChecker.loginContactID());
          if (Request != null)
          {
              string folderPath = "/mpc_content/DigitalAssets" + "/" + UserCookieManager.WEBOrganisationID + "/" + UserCookieManager.WBStoreId + "/Folders/" + FolderID + "";
              string virtualFolderPth = string.Empty;

              // virtualFolderPth = @Server.MapPath(folderPath);
              //  virtualFolderPth = Request.MapPath(folderPath);
              virtualFolderPth = HttpContext.Current.Server.MapPath(folderPath);
              /// virtualFolderPth = System.Web.Http.HttpServer.
              if (!System.IO.Directory.Exists(virtualFolderPth))
              {
                  System.IO.Directory.CreateDirectory(virtualFolderPth);
              }
              if (flag)
              {
                  Folder folder = _companyService.GetFolderByFolderId(FolderID);
                  if (folder.ImagePath != null || folder.ImagePath != "")
                  {
                      RemovePreviousFile(folder.ImagePath);
                  }
              }
              var fileName = Path.GetFileName(Request.FileName);
              Request.SaveAs(virtualFolderPth + "/" + fileName);
              ImagePath = folderPath + "/" + fileName;
          }
          else
          {
              ImagePath = string.Empty;
          }

          return ImagePath;
      }

      private string UpdateAssetImage(HttpPostedFile Request, long Assetid,bool flag)
      {
          string ImagePath = string.Empty;
          //  CompanyContact contact = _companyService.GetContactByID(_webstoreAuthorizationChecker.loginContactID());
          if (Request != null)
          {
              string folderPath = "/mpc_content/DigitalAssets" + "/" + UserCookieManager.WEBOrganisationID + "/" + UserCookieManager.WBStoreId + "/Assets/" + Assetid + "";
              string virtualFolderPth = string.Empty;

              // virtualFolderPth = @Server.MapPath(folderPath);
              //  virtualFolderPth = Request.MapPath(folderPath);
              virtualFolderPth = HttpContext.Current.Server.MapPath(folderPath);
              /// virtualFolderPth = System.Web.Http.HttpServer.
              if (!System.IO.Directory.Exists(virtualFolderPth))
              {
                  System.IO.Directory.CreateDirectory(virtualFolderPth);
              }
              if (flag)
              {
                  Asset GetAsset = _companyService.GetAsset(Assetid);
                  if (GetAsset.ImagePath != null&&GetAsset.ImagePath != "")
                  {
                      RemovePreviousFile(GetAsset.ImagePath);
                  }
              }
              var fileName = Path.GetFileName(Request.FileName);
              Request.SaveAs(virtualFolderPth + "/" + fileName);
              ImagePath = folderPath + "/" + fileName;
          }
          else
          {
              ImagePath = string.Empty;
          }

          return ImagePath;
      }
      [HttpPost]
      public void AddAssetAttachments( long AssetID)
      {
          if (HttpContext.Current.Request != null)
          {
              List<AssetItem> listOfAttachment = new List<AssetItem>();
              string folderPath = "mpc_content/DigitalAssets/" + UserCookieManager.WEBOrganisationID + "/" + UserCookieManager.WBStoreId + "/" + AssetID + "/";
              string virtualFolderPth = string.Empty;

              string folderPathToMap = "/" + folderPath;

              virtualFolderPth = HttpContext.Current.Server.MapPath(folderPathToMap);
              if (!System.IO.Directory.Exists(virtualFolderPth))
                  System.IO.Directory.CreateDirectory(virtualFolderPth);

              for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
              {
                  //HttpPostedFile postedFile = HttpContext.Current.Request.Files[i];
                  HttpPostedFile postedFile = HttpContext.Current.Request.Files["file" + i];
                  string fileName = string.Format("{0}{1}", i, Path.GetFileName(postedFile.FileName));
                  AssetItem Item = new AssetItem();
                  Item.FileUrl = folderPath + fileName;
                  Item.AssetId = AssetID;
                  listOfAttachment.Add(Item);
                  string filevirtualpath = virtualFolderPth + "/" + fileName;
                  postedFile.SaveAs(virtualFolderPth + "/" + fileName);
              }
                 _companyService.AddAssetItems(listOfAttachment);
          }
      
      }
      [HttpPost]
      public void UpdateAsset(string AssetName, string Description, string Keywords, long? FolderId, int? Quantity, double? Price,long AssetId,string RemovedItemsIDs)
      {
          var httpPostedFile = HttpContext.Current.Request.Files["UploadedImageAsset"];

          List<AssetItem> List = new List<AssetItem>();
          if (RemovedItemsIDs != null && RemovedItemsIDs != string.Empty)
          {
              string[] words = RemovedItemsIDs.Split('/');
              foreach (var i in words)
              {
                  AssetItem model = new AssetItem();
                  if (i != string.Empty)
                  {
                      model.AssetItemId = Convert.ToInt64(i);
                      List.Add(model);
                  }
              }
              foreach (var item in List)
              {
                  string PathUrl = _companyService.AssetItemFilePath(item.AssetItemId);
                  if (PathUrl != null && PathUrl != "")
                  {
                      RemovePreviousFile(PathUrl);
                  }
              }
              _companyService.RemoveAssetItems(List);

              
          }
          Asset Asset = new Asset();
          Asset.AssetId = AssetId;
          Asset.AssetName = AssetName;
          Asset.Description =Description;
          Asset.FolderId = FolderId;
          if (httpPostedFile != null)
          {
              Asset.ImagePath = UpdateAssetImage(httpPostedFile, AssetId, true);
          }
          Asset.Keywords = Keywords;
          Asset.Price = Price;
          Asset.Quantity = Quantity;
          _companyService.UpdateAsset(Asset);
      }
       [HttpPost]
      public void UpdateFolder(string FolderName, string Description,long FolderId)
      { 
          var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
          Folder NewFolder = new Folder();
          NewFolder.FolderName = FolderName;
          NewFolder.Description = Description;
          NewFolder.FolderId = FolderId;
         // NewFolder.ParentFolderId = ParentFolderId;
          if (httpPostedFile != null)
          {
              NewFolder.ImagePath = UpdateFolderImage(httpPostedFile, FolderId, true);
          }

          _companyService.UpdateFolder(NewFolder);
      }
       //[System.Web.Http.AcceptVerbs("GET", "POST")]
       //[System.Web.Http.HttpGet]
       //public HttpResponseMessage GetFolderIntellisenseData(string prefixText)
       //{
       //    List<Folder> FolderList = _companyService.GetAllFolders(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
       //    List<Folder> FilterFolderList = FolderList.Where(i => i.FolderName.Contains(prefixText)).OrderBy(ad => ad.FolderName).ToList();
       //    var formatterr = new JsonMediaTypeFormatter();
       //    var jsons = formatterr.SerializerSettings;
       //    jsons.Formatting = Newtonsoft.Json.Formatting.Indented;
       //    jsons.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
       //    return Request.CreateResponse(HttpStatusCode.OK, FilterFolderList, formatterr);
       //}
      [HttpGet]
      public HttpResponseMessage GetAssetByAssetID(long AssetId)
      {
          AssetDeposit obj = new AssetDeposit();
          obj.Asset=_companyService.GetAsset(AssetId);
          obj.ListItems = _companyService.GetAssetItemsByAssetID(AssetId);
          obj.AssetFolder = _companyService.GetFolderByFolderId(Convert.ToInt64(obj.Asset.FolderId));
          var formatterr = new JsonMediaTypeFormatter();
          var jsons = formatterr.SerializerSettings;
          jsons.Formatting = Newtonsoft.Json.Formatting.Indented;
          jsons.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
          return Request.CreateResponse(HttpStatusCode.OK, obj, formatterr);
      }
      [HttpPost]
      public void DeleteAsset(long AssetID)
      {
          _companyService.DeleteAsset(AssetID);
      }
      [System.Web.Http.AcceptVerbs("GET", "POST")]
      [System.Web.Http.HttpGet]
      public HttpResponseMessage GetFolderByFolderId(long folderId)
      {
          Folder folder = _companyService.GetFolderByFolderId(folderId);
          var formatterr = new JsonMediaTypeFormatter();
          var jsons = formatterr.SerializerSettings;
          jsons.Formatting = Newtonsoft.Json.Formatting.Indented;
          jsons.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
          return Request.CreateResponse(HttpStatusCode.OK, folder, formatterr);
      }
      [HttpPost]
      public void DeleteFolder( long folderID)
      {

          _companyService.DeleteFolder(folderID);
      }
    }
    public class AssetDeposit
    {
        public Asset Asset { get; set;}
        public Folder AssetFolder { get; set; }
        public List<AssetItem> ListItems { get; set;}
    }
}
