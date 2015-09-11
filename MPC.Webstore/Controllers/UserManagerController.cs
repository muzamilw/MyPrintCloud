using MPC.Interfaces.WebStoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using System.Collections;
using System.Text;
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
                //foreach (var conta in contacts)
                //{
                //    var a = conta.isPlaceOrder.HasValue;
                //}
            }
            
            ViewBag.Contacts = contacts;
            ViewBag.TotalRecords = contacts.Count.ToString() +" "+ Utils.GetKeyValueFromResourceFile("lblTotalRecords", UserCookieManager.WBStoreId, Utils.GetKeyValueFromResourceFile("lblTotalRecords", UserCookieManager.WBStoreId, "matches found"));
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
            ViewBag.TempText = null;
            return View("PartialViews/UserManager", contacts);

        }
        [HttpPost]
        public ActionResult Index(string SearchString)
        {
            
            List<CompanyContact> contacts = null;
            if (_myClaimHelper.loginContactRoleID() == (int)Roles.Manager)
            {
                contacts = _mycompanyservice.GetSearched_Contacts(UserCookieManager.WBStoreId, SearchString, _myClaimHelper.loginContactTerritoryID());
            }
            else
            {
                contacts = _mycompanyservice.GetSearched_Contacts(UserCookieManager.WBStoreId, SearchString, 0);
            }

           
            ViewBag.Contacts = contacts;
            ViewBag.TotalRecords = contacts.Count.ToString() +" "+ Utils.GetKeyValueFromResourceFile("lblTotalRecords", UserCookieManager.WBStoreId, "matches found");
            if (contacts.Count == 0 || contacts == null)
            {
                TempData["HeaderStatus"] = false;
            }
            else
            {
                TempData["HeaderStatus"] = true;
            }
            ViewBag.TempText = SearchString;
            ViewBag.LoginContactRoleID = _myClaimHelper.loginContactRoleID();
            return View("PartialViews/UserManager",contacts);
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
                ViewBag.TotalRecords = contacts.Count.ToString() + Utils.GetKeyValueFromResourceFile("lblTotalRecords", UserCookieManager.WBStoreId, "matches found");
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
                ViewBag.TotalRecords = contacts.Count.ToString() + Utils.GetKeyValueFromResourceFile("lblTotalRecords", UserCookieManager.WBStoreId, "matches found");
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


       
        [HttpPost]
        private void UpdateRecord(CompanyContact contact)
        {
            _mycompanyservice.UpdateDataSystemUser(contact);
        
        }
        [HttpGet]
        public JsonResult getAddress(long AddressID)
        {
            jsonResponse obj = new jsonResponse();
            Address Address = _mycompanyservice.GetAddressByID(AddressID);
            obj.Address = Address;
            obj.StateName = _mycompanyservice.GetStateNameById(Address.StateId ?? 0);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAddressesByTerritorID(long TerritoryID)
        {
             List <Address> address = _mycompanyservice.GetAddressesByTerritoryID(TerritoryID);
             return Json(address, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetContactsIntellicence(string prefixText)
        {
            StringBuilder sb = new StringBuilder();
            List<CompanyContact> contacts = null;
            if (_myClaimHelper.loginContactRoleID() == (int)Roles.Manager)
            {
                contacts = _mycompanyservice.GetSearched_Contacts(UserCookieManager.WBStoreId, prefixText, _myClaimHelper.loginContactTerritoryID());
            }
            else
            {
                contacts = _mycompanyservice.GetSearched_Contacts(UserCookieManager.WBStoreId, prefixText, 0);
            }
            foreach (var cont in contacts)
            {
                sb.AppendFormat("{0}:", cont.FirstName);
            }
            //recCount = contacts.Count;
            //lblmatchingrecord.Text = recCount.ToString() + " matches found";
            //pagedData.DataSource = contacts;
            //ViewBag.Contacts = contacts;
            //ViewBag.TotalRecords = contacts.Count.ToString() + " matches found";
            //if (contacts.Count == 0 || contacts == null)
            //{
            //    TempData["HeaderStatus"] = false;
            //}
            //else
            //{
            //    TempData["HeaderStatus"] = true;
            //}

            return Json(sb.ToString(), JsonRequestBehavior.DenyGet);
        }
        
    }
    public class jsonResponse
    {
      
       public Address Address;
       public string StateName;
    }
}