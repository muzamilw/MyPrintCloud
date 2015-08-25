using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using MPC.Webstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Models.Common;
using MPC.Webstore.ResponseModels;
using MPC.Webstore.ModelMappers;
using System.Runtime.Caching;
using MPC.ExceptionHandling;
using MPC.Models.ResponseModels;
namespace MPC.Webstore.Controllers
{
    public class SignUpController : Controller
    {
        private readonly ICompanyService _myCompanyService;
        private readonly ICampaignService _campaignService;
        private readonly IUserManagerService _userManagerService;
        private readonly IWebstoreClaimsHelperService _webstoreAuthorizationChecker;
        private readonly IItemService _ItemService;
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SignUpController(ICompanyService myCompanyService, ICampaignService myCampaignService, IUserManagerService userManagerService
            , IItemService ItemService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;

            this._campaignService = myCampaignService;
            this._userManagerService = userManagerService;
            this._ItemService = ItemService;
        }

        #endregion

        // GET: SignUp///
        public ActionResult Index(string FirstName, string LastName, string Email, string ReturnURL)
        {
            //string CacheKeyName = "CompanyBaseResponse";
            //ObjectCache cache = MemoryCache.Default;


            //MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            if (!string.IsNullOrEmpty(StoreBaseResopnse.Company.facebookAppId) && !string.IsNullOrEmpty(StoreBaseResopnse.Company.facebookAppKey))
            {
                ViewBag.ShowFacebookSignInLink = 1;
            }
            else
            {
                ViewBag.ShowFacebookSignInLink = 0;
            }
            if (!string.IsNullOrEmpty(StoreBaseResopnse.Company.twitterAppId) && !string.IsNullOrEmpty(StoreBaseResopnse.Company.twitterAppKey))
            {
                ViewBag.ShowTwitterSignInLink = 1;
            }
            else
            {
                ViewBag.ShowTwitterSignInLink = 0;
            }


            ViewBag.CompanyName = StoreBaseResopnse.Company.Name;
            if (FirstName != null)
            {
                ViewData["IsSocialSignUp"] = true;

            }
            else
            {
                ViewData["IsSocialSignUp"] = false;

            }
            if (string.IsNullOrEmpty(ReturnURL))
                ViewBag.ReturnURL = "Social";
            else
                ViewBag.ReturnURL = ReturnURL;

            return View("PartialViews/SignUp");
        }

        [HttpPost]
        public ActionResult Index(RegisterViewModel model)
        {
            //string CacheKeyName = "CompanyBaseResponse";
            //ObjectCache cache = MemoryCache.Default;
            //MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            try
            {

                if (ModelState.IsValid)
                {
                    if (model.Password == "Password")
                    {
                        ViewBag.Message = "Please enter Password";
                        return View("PartialViews/SignUp");
                    }
                    string isSocial = Request.Form["hfIsSocial"];
                    string ReturnURL = Request.Form["hfReturnURL"];
                    if (!string.IsNullOrEmpty(StoreBaseResopnse.Company.facebookAppId) && !string.IsNullOrEmpty(StoreBaseResopnse.Company.facebookAppKey))
                    {
                        ViewBag.ShowFacebookSignInLink = 1;
                    }
                    else
                    {
                        ViewBag.ShowFacebookSignInLink = 0;
                    }
                    if (!string.IsNullOrEmpty(StoreBaseResopnse.Company.twitterAppId) && !string.IsNullOrEmpty(StoreBaseResopnse.Company.twitterAppKey))
                    {
                        ViewBag.ShowTwitterSignInLink = 1;
                    }
                    else
                    {
                        ViewBag.ShowTwitterSignInLink = 0;
                    }

                    if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                    {

                        if (_myCompanyService.GetCorporateContactByEmail(model.Email, StoreBaseResopnse.Organisation.OrganisationId, UserCookieManager.WBStoreId) != null)
                        {
                            ViewBag.Message = Utils.GetKeyValueFromResourceFile("ltrlnewcuts", UserCookieManager.WBStoreId, "You indicated that you are a new customer but an account already exist with this email address") + model.Email;

                            return View("PartialViews/SignUp");
                        }
                        else if (isSocial == "1")
                        {
                            if (_myCompanyService.GetContactByFirstName(model.FirstName) != null)
                            {
                                ViewBag.Message = Utils.GetKeyValueFromResourceFile("DefaultShippingAddress", UserCookieManager.WBStoreId) + model.Email;
                                return View();
                            }
                            else
                            {
                                SetRegisterCustomer(model);

                            }
                        }
                        else
                        {

                            SetRegisterCustomer(model);

                        }
                    }
                    else
                    {
                        if (_myCompanyService.GetContactByEmail(model.Email, StoreBaseResopnse.Organisation.OrganisationId, UserCookieManager.WBStoreId) != null)
                        {
                            ViewBag.Message = Utils.GetKeyValueFromResourceFile("ltrlnewcuts", UserCookieManager.WBStoreId, "You indicated that you are a new customer but an account already exist with this email address") + model.Email;

                            return View("PartialViews/SignUp");
                        }
                        else if (isSocial == "1")
                        {
                            if (_myCompanyService.GetContactByFirstName(model.FirstName) != null)
                            {
                                ViewBag.Message = Utils.GetKeyValueFromResourceFile("DefaultShippingAddress", UserCookieManager.WBStoreId) + model.Email;
                                return View();
                            }
                            else
                            {
                                SetRegisterCustomer(model);

                            }
                        }
                        else
                        {

                            SetRegisterCustomer(model);

                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(StoreBaseResopnse.Company.facebookAppId) && !string.IsNullOrEmpty(StoreBaseResopnse.Company.facebookAppKey))
                    {
                        ViewBag.ShowFacebookSignInLink = 1;
                    }
                    else
                    {
                        ViewBag.ShowFacebookSignInLink = 0;
                    }
                    if (!string.IsNullOrEmpty(StoreBaseResopnse.Company.twitterAppId) && !string.IsNullOrEmpty(StoreBaseResopnse.Company.twitterAppKey))
                    {
                        ViewBag.ShowTwitterSignInLink = 1;
                    }
                    else
                    {
                        ViewBag.ShowTwitterSignInLink = 0;
                    }


                }
                return View("PartialViews/SignUp");
            }
            catch (Exception ex)
            {

                throw new MPCException(ex.ToString(), StoreBaseResopnse.Organisation.OrganisationId);
            }



        }

        private void SetRegisterCustomer(RegisterViewModel model)
        {
            //string CacheKeyName = "CompanyBaseResponse";
            //ObjectCache cache = MemoryCache.Default;

            CampaignEmailParams cep = new CampaignEmailParams();

            CompanyContact contact = new CompanyContact();
            string TwitterScreenName = string.Empty;
            Int64 CompanyID = 0;
            long OrganisationId = 0;
            CompanyContact corpContact = new CompanyContact();
            bool isContactCreate = false;
            contact.FirstName = model.FirstName == "First Name" ? "" : model.FirstName;
            contact.LastName = model.LastName == "Last Name" ? "" : model.LastName;
            contact.Email = model.Email;
            contact.Mobile = model.Phone;
            contact.Password = model.Password;

            string isSocial = Request.Form["hfIsSocial"];

            if (isSocial == "1")
                TwitterScreenName = model.FirstName;

           // MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            if (StoreBaseResopnse.Organisation != null)
            {
                OrganisationId = StoreBaseResopnse.Organisation.OrganisationId;
            }




            if (StoreBaseResopnse.Company.IsCustomer == (int)StoreMode.Retail)
            {
                CompanyID = _myCompanyService.CreateCustomer(model.FirstName, true, true, CompanyTypes.SalesCustomer, TwitterScreenName, Convert.ToInt64(StoreBaseResopnse.Company.OrganisationId), StoreBaseResopnse.Company.CompanyId, contact);

                if (CompanyID > 0)
                {

                    MPC.Models.DomainModels.Company loginUserCompany = _myCompanyService.GetCompanyByCompanyID(CompanyID);

                    CompanyContact loginUser = _myCompanyService.GetContactByEmail(model.Email, OrganisationId, UserCookieManager.WBStoreId);

                    UserCookieManager.isRegisterClaims = 1;
                    UserCookieManager.WEBContactFirstName = model.FirstName == "First Name" ? "" : model.FirstName;
                    UserCookieManager.WEBContactLastName = model.LastName == "Last Name" ? "" : model.LastName;
                    UserCookieManager.ContactCanEditProfile = loginUser.CanUserEditProfile ?? false;
                    UserCookieManager.ShowPriceOnWebstore = loginUser.IsPricingshown ?? true;

                    UserCookieManager.WEBEmail = model.Email;

                    Campaign RegistrationCampaign = _campaignService.GetCampaignRecordByEmailEvent((int)Events.Registration, StoreBaseResopnse.Company.OrganisationId ?? 0, UserCookieManager.WBStoreId);

                    // work for email to sale manager

                    isContactCreate = true;

                    long OrderId = _ItemService.PostLoginCustomerAndCardChanges(UserCookieManager.WEBOrderId, loginUserCompany.CompanyId, loginUser.ContactId, UserCookieManager.TemporaryCompanyId, UserCookieManager.WEBOrganisationID);
                    cep.ContactId = loginUser.ContactId;
                    cep.SalesManagerContactID = loginUser.ContactId; // this is only dummy data these variables replaced with organization values 
                    cep.StoreId = UserCookieManager.WBStoreId;
                    cep.CompanyId = UserCookieManager.WBStoreId;

                    Address CompanyDefaultAddress = _myCompanyService.GetDefaultAddressByStoreID(UserCookieManager.WBStoreId);
                    if (CompanyDefaultAddress != null)
                    {
                        cep.AddressId = CompanyDefaultAddress.AddressId;
                    }
                    else
                    {
                        cep.AddressId = 0;
                    }

                    if (StoreBaseResopnse.Company.SalesAndOrderManagerId1 == null)
                    {
                        throw new Exception("Critcal Error, Store Sales Manager is not selected.", null);

                    }
                    else
                    {
                        SystemUser EmailOFSM = _userManagerService.GetSalesManagerDataByID(StoreBaseResopnse.Company.SalesAndOrderManagerId1.Value);

                        _campaignService.emailBodyGenerator(RegistrationCampaign, cep, loginUser, StoreMode.Retail, (int)loginUserCompany.OrganisationId, "", "", "", EmailOFSM.Email, "", "", null, "");

                        _campaignService.SendEmailToSalesManager((int)Events.NewRegistrationToSalesManager, (int)loginUser.ContactId, (int)loginUser.CompanyId, 0, UserCookieManager.WEBOrganisationID, 0, StoreMode.Retail, UserCookieManager.WBStoreId, EmailOFSM);

                    }

                    if (OrderId > 0)
                    {
                        UserCookieManager.TemporaryCompanyId = 0;
                        Response.Redirect("/ShopCart?OrderId=" + OrderId);
                    }
                    else
                    {
                        Response.Redirect("/");
                    }

                }
                else
                {
                    isContactCreate = false;
                }
                StoreBaseResopnse = null;

                if (!string.IsNullOrEmpty(Request.QueryString["ReturnURL"]))
                {
                    if (Url.IsLocalUrl(Request.QueryString["ReturnURL"]))
                    {
                        ControllerContext.HttpContext.Response.Redirect(Request.QueryString["ReturnURL"]);
                    }
                }
                else
                {
                    Response.Redirect("/");
                }

                return;
            }
            else
            {
                CompanyContact CorpContact = _myCompanyService.CreateCorporateContact(StoreBaseResopnse.Company.CompanyId, contact, TwitterScreenName, StoreBaseResopnse.Organisation.OrganisationId);

                UserCookieManager.isRegisterClaims = 1;
                UserCookieManager.WEBContactFirstName = model.FirstName;
                UserCookieManager.WEBContactLastName = model.LastName;
                UserCookieManager.ContactCanEditProfile = CorpContact.CanUserEditProfile ?? false;
                UserCookieManager.ShowPriceOnWebstore = CorpContact.IsPricingshown ?? true;

                UserCookieManager.WEBEmail = model.Email;

                cep.ContactId = CorpContact.ContactId;
                cep.CompanyId = CorpContact.CompanyId;
                cep.SalesManagerContactID = CorpContact.ContactId; // this is only dummy data these variables replaced with organization values 
                cep.StoreId = UserCookieManager.WBStoreId;
                Address CompanyDefaultAddress = _myCompanyService.GetDefaultAddressByStoreID(UserCookieManager.WBStoreId);
                if (CompanyDefaultAddress != null)
                {
                    cep.AddressId = CompanyDefaultAddress.AddressId;
                }
                else
                {
                    cep.AddressId = 0;
                }
                List<CompanyContact> listOfApprovers = _myCompanyService.GetCompanyAdminByCompanyId(UserCookieManager.WBStoreId);
                if (listOfApprovers != null && listOfApprovers.Count > 0)
                {
                    cep.ApprovarID = listOfApprovers.FirstOrDefault().ContactId;
                }
                Campaign RegistrationCampaign = _campaignService.GetCampaignRecordByEmailEvent((int)Events.CorpUserRegistration, StoreBaseResopnse.Company.OrganisationId ?? 0, UserCookieManager.WBStoreId);

                SystemUser EmailOFSM = _userManagerService.GetSalesManagerDataByID(StoreBaseResopnse.Company.SalesAndOrderManagerId1.Value);

                _campaignService.emailBodyGenerator(RegistrationCampaign, cep, CorpContact, StoreMode.Corp, (int)StoreBaseResopnse.Company.OrganisationId, "", "", "", EmailOFSM.Email, "", "", null, "");

                _campaignService.SendPendingCorporateUserRegistrationEmailToAdmins((int)CorpContact.ContactId, (int)UserCookieManager.WBStoreId, (int)StoreBaseResopnse.Company.OrganisationId);
                StoreBaseResopnse = null;
                ViewBag.Message = Utils.GetKeyValueFromResourceFile("ltrlwebacces", UserCookieManager.WBStoreId, "You are successfully registered on store but your account does not have the web access enabled. Please contact your Order Manager.");
                return;
            }

        }

        //public void PostLoginCustomerAndCardChanges(out long replacedWithOrderID)
        //{
        //    //List<ArtWorkAttatchment> orderAllItemsAttatchmentsListToBeRemoved = null;
        //    //List<Templates> clonedTempldateFilesList = null;
        //    //OrderManager ordManager = null;
        //    //int dummyCustomerID = this.GetCustomerIDFromCookie();
        //    //replacedWithOrderID = 0;

        //    //if (dummyCustomerID > 0 && dummyCustomerID != SessionParameters.CustomerID)
        //    //{
        //    //    ordManager = new OrderManager();
        //    //    if (ordManager.UpdateDummyCustomerOrderWithRealCustomer(dummyCustomerID, SessionParameters.CustomerID, SessionParameters.ContactID, out replacedWithOrderID, out orderAllItemsAttatchmentsListToBeRemoved, out clonedTempldateFilesList))
        //    //    {
        //    //        BLL.ProductManager.RemoveItemAttacmentPhysically(orderAllItemsAttatchmentsListToBeRemoved);
        //    //        BLL.ProductManager.RemoveItemTemplateFilesPhysically(clonedTempldateFilesList);
        //    //    }
        //    //}

        //}
    }
}