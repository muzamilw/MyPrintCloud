using MPC.Interfaces.WebStoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using System.Collections.Generic;
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

            List<CompanyContact> contacts = null;
            if (_myClaimHelper.loginContactRoleID() == (int)Roles.Manager)
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
            ViewBag.LoginContactRoleID = _myClaimHelper.loginContactRoleID();
            return View("PartialViews/UserManager");

        }
        private List<CompanyContactRole> BindContactRoles()
        {
            List<CompanyContactRole> roles = null;
            if (_myClaimHelper.loginContactRoleID() ==(int)Roles.Manager)
            {
                roles = _mycompanyservice.GetContactRolesExceptAdmin((int)Roles.Adminstrator);
            }
            else
            {
                roles = _mycompanyservice.GetAllContactRoles();
            }
            return roles;
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
        private JsonResult LoadTerritories()
        {
           List <CompanyTerritory> cmpterritory = _mycompanyservice.GetAllCompanyTerritories(UserCookieManager.WEBOrganisationID).ToList();
           return Json(cmpterritory,JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        private JsonResult LoadQuestions()
        {
           List<RegistrationQuestion> questions = _mycompanyservice.GetAllQuestions().ToList();
           return Json(questions, JsonRequestBehavior.AllowGet);
        }
    }
}