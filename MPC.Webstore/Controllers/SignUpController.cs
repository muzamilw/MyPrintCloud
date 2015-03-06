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
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;


            MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.StoreId];

            
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
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;
            MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.StoreId];

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
          
                    if (_myCompanyService.GetContactByEmail(model.Email, StoreBaseResopnse.Organisation.OrganisationId) != null)
                    {
                        ViewBag.Message = "You indicated that you are a new customer but an account already exist with this email address " + model.Email;
                        return View("PartialViews/SignUp");
                    }
                    else if (isSocial == "1")
                    {
                        if (_myCompanyService.GetContactByFirstName(model.FirstName) != null)
                        {
                            ViewBag.Message = Utils.GetKeyValueFromResourceFile("DefaultShippingAddress", UserCookieManager.StoreId) + model.Email;
                            return View();
                        }
                        else
                        {
                            SetRegisterCustomer(model);
                            if (ReturnURL == "Social")
                                return RedirectToAction("Index", "Home");
                            else
                            {

                                ControllerContext.HttpContext.Response.Redirect(ReturnURL);
                                return null;
                            }
                        }
                    }
                    else
                    {

                        SetRegisterCustomer(model);
                        if (string.IsNullOrEmpty(model.ReturnURL))
                            return RedirectToAction("Index", "Home");
                        else
                        {
                            ControllerContext.HttpContext.Response.Redirect(model.ReturnURL);
                            return null;
                        }
                    }
                }
                else
                {
                    return View("PartialViews/SignUp");
                }
            }
            catch (Exception ex)
            {

                throw new MPCException(ex.ToString(), StoreBaseResopnse.Organisation.OrganisationId);
            }

           

        }

        private void SetRegisterCustomer(RegisterViewModel model)
        {
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;

            CampaignEmailParams cep = new CampaignEmailParams();

            CompanyContact contact = new CompanyContact();
            string TwitterScreenName = string.Empty;
            Int64 CompanyID = 0;
            long OID = 0;
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

            MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.StoreId];

            if (StoreBaseResopnse.Organisation != null)
            {
                OID = StoreBaseResopnse.Organisation.OrganisationId;
            }




            if (StoreBaseResopnse.Company.IsCustomer == (int)StoreMode.Retail)
            {
                CompanyID = _myCompanyService.CreateCustomer(model.FirstName, true, true, CompanyTypes.SalesCustomer, TwitterScreenName, Convert.ToInt64(StoreBaseResopnse.Company.OrganisationId), contact);

                if (CompanyID > 0)
                {

                    MPC.Models.DomainModels.Company loginUserCompany = _myCompanyService.GetCompanyByCompanyID(CompanyID);

                    CompanyContact loginUser = _myCompanyService.GetContactByEmail(model.Email,OID);

                   // cep.StoreID = company.StoreId ?? 0;


                    UserCookieManager.isRegisterClaims = 1;
                    UserCookieManager.ContactFirstName = model.FirstName == "First Name" ? "" : model.FirstName;
                    UserCookieManager.ContactLastName = model.LastName == "Last Name" ? "" : model.LastName;
                    UserCookieManager.ContactCanEditProfile = loginUser.CanUserEditProfile ?? false;
                    UserCookieManager.ShowPriceOnWebstore = loginUser.IsPricingshown ?? true;

                    UserCookieManager.Email = model.Email;

                    Campaign RegistrationCampaign = _campaignService.GetCampaignRecordByEmailEvent((int)Events.Registration);

                    // work for email to sale manager

                    isContactCreate = true;

                    long OrderId = _ItemService.PostLoginCustomerAndCardChanges(0, loginUserCompany.CompanyId, loginUser.ContactId, UserCookieManager.TemporaryCompanyId, UserCookieManager.OrganisationID);

                    cep.SalesManagerContactID = loginUser.ContactId; // this is only dummy data these variables replaced with organization values 
                    cep.StoreID = loginUser.CompanyId;
                    cep.AddressID = loginUser.CompanyId;

                    SystemUser EmailOFSM = _userManagerService.GetSalesManagerDataByID(loginUserCompany.SalesAndOrderManagerId1.Value);



                    _campaignService.emailBodyGenerator(RegistrationCampaign, cep, loginUser, StoreMode.Retail, (int)loginUserCompany.OrganisationId, "", "", "", EmailOFSM.Email, "", "", null, "");

                    _campaignService.SendEmailToSalesManager((int)Events.NewRegistrationToSalesManager, (int)loginUser.ContactId, (int)loginUser.CompanyId, 0, UserCookieManager.OrganisationID, 0, StoreMode.Retail, UserCookieManager.StoreId, EmailOFSM);

                    if (OrderId > 0)
                    {
                        UserCookieManager.TemporaryCompanyId = 0;
                        Response.Redirect("/ShopCart/" + OrderId);
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

            }
            else
            {
                int cid = (int)StoreBaseResopnse.Company.CompanyId;

                CompanyContact CorpContact = _myCompanyService.CreateCorporateContact(cid, contact, TwitterScreenName);

                UserCookieManager.isRegisterClaims = 1;
                UserCookieManager.ContactFirstName = model.FirstName;
                UserCookieManager.ContactLastName = model.LastName;
                UserCookieManager.ContactCanEditProfile = CorpContact.CanUserEditProfile ?? false;
                UserCookieManager.ShowPriceOnWebstore = CorpContact.IsPricingshown ?? true;

                UserCookieManager.Email = model.Email;

                cep.ContactId = (int)CorpContact.ContactId;
                cep.CompanyId = (int)CorpContact.CompanyId;
                cep.SalesManagerContactID = CorpContact.ContactId; // this is only dummy data these variables replaced with organization values 
                cep.StoreID = CorpContact.CompanyId;
                cep.AddressID = CorpContact.CompanyId;

                Campaign RegistrationCampaign = _campaignService.GetCampaignRecordByEmailEvent((int)Events.CorpUserRegistration);

                SystemUser EmailOFSM = _userManagerService.GetSalesManagerDataByID(StoreBaseResopnse.Company.SalesAndOrderManagerId1.Value);

                _campaignService.emailBodyGenerator(RegistrationCampaign, cep, CorpContact, StoreMode.Corp, (int)StoreBaseResopnse.Company.OrganisationId, "", "", "", EmailOFSM.Email, "", "", null, "");

                int OrganisationId = (int)StoreBaseResopnse.Company.OrganisationId;
                _campaignService.SendPendingCorporateUserRegistrationEmailToAdmins((int)CorpContact.ContactId, (int)corpContact.CompanyId, OrganisationId);

            }
            StoreBaseResopnse = null;
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