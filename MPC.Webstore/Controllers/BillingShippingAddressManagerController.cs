using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class BillingShippingAddressManagerController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        // GET: BillingShippingAddressManager
        public BillingShippingAddressManagerController(ICompanyService _companyService, IWebstoreClaimsHelperService _myClaimHelper)
        {
            this._companyService = _companyService;
            this._myClaimHelper = _myClaimHelper;
        }
        public ActionResult Index()
        {
            if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
            {
                ViewBag.Address = FilterAddresses();
            }
            else
            {
                ViewBag.Address = _companyService.GetAddressesListByContactCompanyID(_myClaimHelper.loginContactCompanyID());
            }

            ViewBag.CompanyID = _myClaimHelper.loginContactCompanyID();
            return View("PartialViews/BillingShippingAddressManager");
        }

        private List<Address> FilterAddresses()
        {
            List<Address> addresses = new List<Address>();
            List<Address> Manageraddresses = new List<Address>();
            List<CompanyContact> ContactTerritoriesIDs = new List<CompanyContact>();
            List<int> AddressIDs = new List<int>();

            CompanyTerritory Territory = new CompanyTerritory();
            int BillingAddressID = 0;
            int ShippingAddressID = 0;
            //if (SessionParameters.StoreMode == StoreMode.Corp)
            //{
            Company Company = _companyService.GetCompanyByCompanyID(_myClaimHelper.loginContactCompanyID());
            if (Company.isStoreModePrivate == true)
            {

                if (_myClaimHelper.loginContactRoleID() == (int)Roles.Adminstrator)
                {
                    addresses = _companyService.GetAddressesListByContactCompanyID(_myClaimHelper.loginContactCompanyID());
                    return addresses;
                }
                else if (_myClaimHelper.loginContactRoleID()== (int)Roles.Manager)
                {
                    {
                        CompanyContact contact=_companyService.GetContactByID(_myClaimHelper.loginContactID());
                        //Territory = TerritoryManager.GetTerritoryById(context, (int)SessionParameters.CustomerContact.TerritoryID);
                       
                        Territory = _companyService.GetTerritoryById((int)contact.TerritoryId);
                    }
                    if (Territory != null)
                    {

                        //addresses = AddressManager.GetAddressesByTerritoryID(Territory.TerritoryID);
                        addresses = _companyService.GetAddressesByTerritoryID(Territory.TerritoryId);

                    }
                    return addresses;
                }
                else if (_myClaimHelper.loginContactRoleID() == (int)Roles.User)
                {

                    //addresses = AddressManager.GetAdressesByContactID(SessionParameters.CustomerContact.ContactID);
                    addresses = _companyService.GetAdressesByContactID(_myClaimHelper.loginContactID());
                    return addresses;

                }
                else
                {
                    return null;
                }
            }
            else
            {

                addresses = _companyService.GetAddressesListByContactCompanyID(_myClaimHelper.loginContactCompanyID());
                return addresses;
            }
        }

        [HttpPost]
        public ActionResult Index(string SearchString, string btnsearch, string btnReset)
        {
            if (btnsearch != null)
            {
                if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                {
                    List<Address> RefinedAddresses = FilterAddresses();
                    ViewBag.Address = RefinedAddresses.Where(w => w.CompanyId == _myClaimHelper.loginContactCompanyID() && w.AddressName.Contains(SearchString.Trim()) && (w.isArchived == null || w.isArchived.Value == false)).ToList();
                }
                else
                {
                    ViewBag.Address = _companyService.GetsearchedAddress(_myClaimHelper.loginContactCompanyID(), SearchString);
                }

            }
            else
            {
            
            
            }
            return View("PartialViews/BillingShippingAddressManager");
        }
        [HttpPost]
        public JsonResult FillAddresses(long AddressID)
        {
            Address Address = _companyService.GetAddressByID(AddressID);
            return Json(Address, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public void UpdateAddress(Address Address)
        {
            _companyService.UpdateBillingShippingAdd(Address);
        }
        [HttpPost]
        public void AddNewAddress(Address Address)
        {
            Address.CompanyId = _myClaimHelper.loginContactCompanyID();
           _companyService.AddAddBillingShippingAdd(Address);
        }
        [HttpGet]
        public JsonResult LoadCountriesList()
        {
         JsonResponse obj = new JsonResponse();
          obj.Country=_companyService.GetAllCountries();
          return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult LoadAllStates()
        {
            JsonResponse obj = new JsonResponse();
            obj.State=_companyService.GetAllStates();
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult LoadStatesByCountryID(long CountryId)
        {
            JsonResponse obj = new JsonResponse();
            obj.State = _companyService.GetCountryStates(CountryId);
            return Json(obj, JsonRequestBehavior.AllowGet);
           
        }
    }
    public class JsonResponse
    {
        public List<State> State;
        public List<Country> Country;
    
    }
}