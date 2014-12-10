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
        
       #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SignUpController(ICompanyService myCompanyService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;
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
                if (_myCompanyService.GetContactByEmail(model.Email) != null)
                {
                   ViewBag.Message = string.Format("You indicated you are a new customer, but an account already exists with the e-mail {0}", model.Email);
                    return View();
                }
                //else if (hfisRegWithTwitter.Value == "1")
                //{
                //    if (_myCompanyService.GetContactByFirstName(model.FirstName) != null)
                //    {
                //        ViewBag.Message = string.Format("You indicated you are a new customer, but an account already exists with the e-mail {0}", txtEmail.Text);
                //        return View();
                //    }
                //    else
                //    {
                //        SetRegisterCustomer(model);

                //    }
                //}
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

            long storeId = Convert.ToInt64(Session["storeId"]);

            MyCompanyDomainBaseResponse baseResponse = _myCompanyService.GetBaseData(storeId).CreateFromCompany();

            if (baseResponse.Company.IsCustomer == (int)StoreMode.Retail)
            {
                CompanyID = _myCompanyService.CreateContact(contact, contact.FirstName + " " + contact.LastName, 0, (int)ContactCompanyTypes.SalesCustomer, TwitterScreenName);

                if (CompanyID > 0)
                {
                    //SessionParameters.LoginCompany.CompanyId = contact.CompanyId;
                    //SessionParameters.LoginContact.ContactId = contact.ContactId;
                    //SessionParameters.LoginCompany = _myCompanyService.GetCompanyByCompanyID(contact.CompanyId);
                    //SessionParameters.LoginContact = _myCompanyService.GetContactByID(contact.ContactId);

                    //Campaign RegistrationCampaign = _campaignService.GetCampaignRecordByEmailEvent((int)Events.Registration);
                    //// work for email to sale manager
                    //isContactCreate = true;

                    //cep.SalesManagerContactID =  SessionParameters.LoginContact.ContactId; // this is only dummy data these variables replaced with organization values 
                    //cep.StoreID = SessionParameters.LoginCompany.OrganisationId ?? 0;
                    //cep.AddressID = SessionParameters.LoginCompany.CompanyId;

                    //SystemUser EmailOFSM = _userManagerService.GetSalesManagerDataByID(Convert.ToInt32(SessionParameters.LoginCompany.OrganisationId));
                    //emailmgr.emailBodyGenerator(RegistrationCampaign, SessionParameters.CompanySite, cep, SessionParameters.CustomerContact, StoreMode.Retail, "", "", "", EmailOFSM.Email, "", "", null, "");
                    //emailmgr.SendEmailToSalesManager((int)EmailEvents.NewRegistrationToSalesManager, CurrentUser.ContactID, CurrentUser.ContactCompanyID, 0, 0, SessionParameters.CompanySite, 0, 0, StoreMode.Retail);
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

                //cep.ContactID = (int)CorpContact.ContactId;
                //cep.ContactCompanyID = (int)SessionParameters.LoginCompany.CompanyId;
                //cep.SalesManagerContactID = CorpContact.ContactId; // this is only dummy data these variables replaced with organization values 
                //cep.StoreID = SessionParameters.LoginCompany.CompanyId;
                //cep.AddressID = SessionParameters.LoginCompany.CompanyId;
                //Campaign RegistrationCampaign = emailmgr.GetCampaignRecordByEmailEvent((int)EmailEvents.CorpUserRegistration);
                //emailmgr.emailBodyGenerator(RegistrationCampaign, SessionParameters.CompanySite, cep, CorpContact, StoreMode.Corp, "", "", "", EmailOFSM.Email, "", "", null, "", null, "", "", null, "", "", "", 0, "", 0);
                //emailmgr.SendPendingCorporateUserRegistrationEmailToAdmins(CorpContact.ContactID, SessionParameters.ContactCompany.ContactCompanyID);
               
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