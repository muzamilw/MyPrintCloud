using MPC.Interfaces.WebStoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;

namespace MPC.Webstore.Controllers
{
    public class UserManagerController : Controller
    {
        // GET: UserManager
        private readonly ICompanyService _mycompanyservice;
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        public UserManagerController(ICompanyService _mycompanyservice, IWebstoreClaimsHelperService _myClaimHelper)
        {
            this._mycompanyservice = _mycompanyservice;
            this._myClaimHelper = _myClaimHelper;
        }
        public ActionResult Index()
        {

           //List <CompanyTerritory> cmpterritory = _mycompanyservice.GetAllCompanyTerritories(UserCookieManager.WEBOrganisationID).ToList();
           // List<RegistrationQuestion> questions = _mycompanyservice.GetAllQuestions().ToList();
            long loginID = _myClaimHelper.loginContactID();
            List<CompanyContact> contacts = null;
            if (_myClaimHelper.loginContactRoleID() == (int)Roles.Manager)
            {
                contacts = _mycompanyservice.GetContactsByTerritory(UserCookieManager.WBStoreId, _myClaimHelper.loginContactTerritoryID());
            }
            else
            {
                contacts = _mycompanyservice.GetContactsByTerritory(UserCookieManager.WBStoreId, 0);
                foreach (var conta in contacts)
                {
                    var a = conta.isPlaceOrder.HasValue;
                }
            }
            
            ViewBag.Contacts = contacts;
            ViewBag.TotalRecords = contacts.Count.ToString() + " matches found";
            if (contacts.Count == 0 || contacts == null)
            {
                TempData["HeaderStatus"] = false;
            }
            else
            {
                TempData["HeaderStatus"] = true;
            }
            ViewBag.totalcount = contacts.Count;
            ViewBag.LoginContactRoleID = _myClaimHelper.loginContactRoleID();
            return View("PartialViews/UserManager", contacts);

        }
  
        private void SearchedData(string textt)
        {
            List<CompanyContact> contacts = null;
            
            if (true) // User Search
            {
                
                if (_myClaimHelper.loginContactRoleID() == (int)Roles.Manager)
                {
                    contacts = _mycompanyservice.GetSearched_Contacts(UserCookieManager.WBStoreId, textt,_myClaimHelper.loginContactTerritoryID());
                }
                else
                {
                    contacts = _mycompanyservice.GetSearched_Contacts(UserCookieManager.WBStoreId, textt,0);
                }
                //recCount = contacts.Count;
                //lblmatchingrecord.Text = recCount.ToString() + " matches found";
                //pagedData.DataSource = contacts;
                ViewBag.Contacts = contacts;
                ViewBag.TotalRecords = contacts.Count.ToString() + " matches found";
                if (contacts.Count == 0 || contacts == null)
                {
                    TempData["HeaderStatus"] = false;
                }
                else
                {
                    TempData["HeaderStatus"] = true;
                }
                ViewBag.totalcount = contacts.Count;
            }
            else // default load
            {
                if(_myClaimHelper.loginContactRoleID() == (int)Roles.Manager)
                {
                    contacts = _mycompanyservice.GetContactsByTerritory(UserCookieManager.WBStoreId, _myClaimHelper.loginContactTerritoryID());
                }
                else
                {
                    contacts = _mycompanyservice.GetContactsByTerritory(UserCookieManager.WBStoreId, 0);
                }
                ViewBag.Contacts = contacts;
                ViewBag.TotalRecords = contacts.Count.ToString() + " matches found";
                if (contacts.Count == 0 || contacts == null)
                {
                    TempData["HeaderStatus"] = false;
                }
                else
                {
                    TempData["HeaderStatus"] = true;
                }
                ViewBag.totalcount = contacts.Count;
               
            }
        }
       
        [HttpGet]
       
        private JsonResult UserProfileData()
        {
            jsonResponse obj = new jsonResponse();
            obj.CompanyTerritory = _mycompanyservice.GetAllCompanyTerritories(UserCookieManager.WEBOrganisationID).ToList();
            obj.RegistrationQuestions = _mycompanyservice.GetAllQuestions().ToList();
            obj.Addresses = _mycompanyservice.GetAddressesByTerritoryID(_myClaimHelper.loginContactTerritoryID());
            List<CompanyContactRole> roles = null;
            if (_myClaimHelper.loginContactRoleID() == (int)Roles.Manager)
            {
                roles = _mycompanyservice.GetContactRolesExceptAdmin((int)Roles.Adminstrator);
            }
            else
            {
                roles = _mycompanyservice.GetAllContactRoles();
            }
            obj.CompanyContactRoles = roles;
            CompanyContact LoginContact = _mycompanyservice.GetContactByID(_myClaimHelper.loginContactID());
            Address LoginContactAddress = _mycompanyservice.GetAddressByID(LoginContact.AddressId);
            obj.LoginContactAddress = LoginContactAddress;
            obj.SelectedShippingAddress =_mycompanyservice.GetAddressByID(LoginContact.ShippingAddressId??0);
            obj.SelectedBillingAddress = _mycompanyservice.GetAddressByID(LoginContact.AddressId);
            obj.SelectedQuestion=_mycompanyservice.GetSecretQuestionByID(LoginContact.QuestionId??0);
            obj.setSelectedTerritory = _mycompanyservice.GetTerritoryById(_myClaimHelper.loginContactTerritoryID());
            obj.SelectedRole=_mycompanyservice.GetRoleByID(LoginContact.ContactRoleId??0);
            return Json(obj, JsonRequestBehavior.AllowGet);
         }
        [HttpPost]
        private void UpdateRecord(CompanyContact contact)
        {
            _mycompanyservice.UpdateDataSystemUser(contact);
        
        }
        
    }
    public class jsonResponse
    {
       public List<CompanyTerritory> CompanyTerritory;
       public List<RegistrationQuestion> RegistrationQuestions;
       public List<Address> Addresses;
       public List<CompanyContactRole> CompanyContactRoles;
       public Address LoginContactAddress;
       public Address SelectedShippingAddress;
       public Address SelectedBillingAddress;
       public RegistrationQuestion SelectedQuestion;
       public CompanyTerritory setSelectedTerritory;
       public CompanyContactRole SelectedRole;
    }
}