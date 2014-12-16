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
namespace MPC.Webstore.Controllers
{
    public class SignUpController : Controller
    {
        private readonly ICompanyService _myCompanyService;
        private readonly ICampaignService _campaignService;
        private readonly IUserManagerService _userManagerService;
        private readonly IWebstoreClaimsHelperService _webstoreAuthorizationChecker;

       #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SignUpController(ICompanyService myCompanyService,ICampaignService myCampaignService, IUserManagerService userManagerService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;
        
            this._campaignService = myCampaignService;
            this._userManagerService = userManagerService;
        }

        #endregion

        // GET: SignUp
        public ActionResult Index(string FirstName, string LastName, string Email)
        {
            if (FirstName != null)
            {
                ViewData["IsSocialSignUp"] = true;
               
            }else
            {
                ViewData["IsSocialSignUp"] = false;
                
            }
            return View("PartialViews/SignUp");
        }

        [HttpPost]
        public ActionResult Index(RegisterViewModel model)
        {
            

            if (ModelState.IsValid)
            {
                string isSocial = Request.Form["hfIsSocial"];
                if (_myCompanyService.GetContactByEmail(model.Email) != null)
                {
                   ViewBag.Message = string.Format("You indicated you are a new customer, but an account already exists with the e-mail {0}", model.Email);
                   return View("PartialViews/SignUp");
                }
                else if (isSocial == "1")
                {
                    if (_myCompanyService.GetContactByFirstName(model.FirstName) != null)
                    {
                        ViewBag.Message = string.Format("You indicated you are a new customer, but an account already exists with the e-mail {0}",model.Email);
                        return View();
                    }
                    else
                    {
                        SetRegisterCustomer(model);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    SetRegisterCustomer(model);

                    return RedirectToAction("Index", "Home");
                    
                }
            }
            else
            {
                return View("PartialViews/Login");
            }

        }

        private void SetRegisterCustomer(RegisterViewModel model)
        {
            CampaignEmailParams cep = new CampaignEmailParams();
            CompanyContact contact = new CompanyContact();
            string TwitterScreenName = string.Empty;
            Int64 CompanyID = 0;
            CompanyContact corpContact = new CompanyContact();
            bool isContactCreate = false;
            contact.FirstName = model.FirstName;
            contact.LastName = model.LastName;
            contact.Email = model.Email;
            contact.Mobile = model.Phone;
            contact.Password = model.Password;

            string isSocial = Request.Form["hfIsSocial"];

            if (isSocial == "1")
                TwitterScreenName = model.FirstName;

         //   long storeId = Convert.ToInt64(Session["storeId"]);

            MyCompanyDomainBaseResponse baseResponse = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCompany();
           
            MPC.Models.DomainModels.Organisation organisation = new MPC.Models.DomainModels.Organisation();

            organisation = null;

            if (baseResponse.Company.IsCustomer == (int)StoreMode.Retail)
            {
                CompanyID = _myCompanyService.CreateContact(contact, contact.FirstName + " " + contact.LastName, 0, (int)ContactCompanyTypes.SalesCustomer, TwitterScreenName);

                if (CompanyID > 0)
                {
                    //SessionParameters.LoginCompany.CompanyId = contact.CompanyId;
                    //SessionParameters.LoginContact.ContactId = contact.ContactId;
                    //SessionParameters.LoginCompany = _myCompanyService.GetCompanyByCompanyID(contact.CompanyId);
                    //SessionParameters.LoginContact = _myCompanyService.GetContactByID(contact.ContactId);
                    MPC.Models.DomainModels.Company company = _myCompanyService.GetCompanyByCompanyID(CompanyID);
                    MPC.Models.DomainModels.CompanyContact companyContact = _myCompanyService.GetContactByID(contact.ContactId);

                    Campaign RegistrationCampaign = _campaignService.GetCampaignRecordByEmailEvent((int)Events.Registration);

                    // work for email to sale manager
                    isContactCreate = true;

                    cep.SalesManagerContactID = companyContact.ContactId; // this is only dummy data these variables replaced with organization values 
                    cep.StoreID = company.OrganisationId ?? 0;
                    cep.AddressID = company.CompanyId;

                    SystemUser EmailOFSM = _userManagerService.GetSalesManagerDataByID(Convert.ToInt32(company.SalesAndOrderManagerId1));


                  
                    _campaignService.emailBodyGenerator(RegistrationCampaign, organisation, cep, companyContact, StoreMode.Retail,(int)company.OrganisationId, "", "", "", EmailOFSM.Email, "", "", null, "");
                    
                    //void SendEmailToSalesManager(int Event, int ContactId, int CompanyId, int brokerid, int OrderId,Organisation ServerSettings, int BrokerAdminContactID, int CorporateManagerID, StoreMode Mode,Company company,SystemUser SaleManager,int ItemID, string NameOfBrokerComp = "", string MarketingBreifMesgSummry = "", int RFQId = 0);



                    _campaignService.SendEmailToSalesManager((int)Events.NewRegistrationToSalesManager, (int)companyContact.ContactId, (int)companyContact.CompanyId, 0, 0, organisation, 0, 0, StoreMode.Retail, company, EmailOFSM);
                  
                   
                    
                    // SetFormAuthDetails();
                   // PostLoginCustomerAndCardChanges(out replacedWithOrderID);
                    //if (!string.IsNullOrWhiteSpace(PageParameters.RetUrl))
                    //{
                    //    Response.Redirect(this.GetReturnUrl(replacedWithOrderID, PageParameters.RetUrl), false);
                    //}
                    //if (SessionParameters.IsUserAdmin == true)
                    //{
                    //    SessionParameters.IsUserAdmin = false;
                    //}
                    //else if (this.GetShopCartItemsCount() > 0)
                    //{
                    //    Response.Redirect("~/PinkCardShopCart.aspx", false);
                    //}
                    //else
                    //{
                       // Response.Redirect("DashBoard.aspx", false);
                        
                  //  }
                }
                else
                {

                    isContactCreate = false;
                }

            }
            else
            {
                int cid = (int)baseResponse.Company.CompanyId;
                CompanyContact CorpContact = _myCompanyService.CreateCorporateContact(cid, contact, TwitterScreenName);
                 
                cep.ContactId = (int)CorpContact.ContactId;
                cep.CompanyId = (int)CorpContact.CompanyId;
                cep.SalesManagerContactID = CorpContact.ContactId; // this is only dummy data these variables replaced with organization values 
                cep.StoreID = CorpContact.CompanyId;
                cep.AddressID = CorpContact.CompanyId;
                Campaign RegistrationCampaign = _campaignService.GetCampaignRecordByEmailEvent((int)Events.CorpUserRegistration);
                SystemUser EmailOFSM = _userManagerService.GetSalesManagerDataByID(Convert.ToInt32(baseResponse.Company.SalesAndOrderManagerId1));

               // _campaignService.emailBodyGenerator(RegistrationCampaign, organisation, cep, companyContact, StoreMode.Retail, (int)company.OrganisationId, "", "", "", EmailOFSM.Email, "", "", null, "");

                _campaignService.emailBodyGenerator(RegistrationCampaign, organisation, cep, CorpContact, StoreMode.Corp, (int)baseResponse.Company.OrganisationId, "", "","", EmailOFSM.Email , "", "", null, "");

                int OrganisationId = (int)baseResponse.Company.OrganisationId;
                _campaignService.SendPendingCorporateUserRegistrationEmailToAdmins((int)CorpContact.ContactId, (int)corpContact.CompanyId, OrganisationId);
               
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