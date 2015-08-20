using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using System.Runtime.Caching;
using MPC.Models.Common;
using MPC.Models.ResponseModels;
namespace MPC.Webstore.Controllers
{
    public class PersonalDetailAndOrderPolicyController : Controller
    {
        private readonly ICompanyService _myCompanyService;
        private readonly IWebstoreClaimsHelperService _webstoreAuthorizationChecker;
        private readonly ICompanyContactRepository _companyContact;
        private readonly IOrderService _OrderService;
        // GET: PersonalDetailAndOrderPolicy

        public PersonalDetailAndOrderPolicyController(ICompanyService _myCompanyService, IWebstoreClaimsHelperService _webstoreAuthorizationChecker, ICompanyContactRepository _companyContact, IOrderService _OrderService)
        {
            this._myCompanyService = _myCompanyService;
            this._webstoreAuthorizationChecker = _webstoreAuthorizationChecker;
            this._companyContact = _companyContact;
            this._OrderService = _OrderService;
        }
        public ActionResult Index()
        {
            //string CacheKeyName = "CompanyBaseResponse";
            //ObjectCache cache = MemoryCache.Default;
            //MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            CompanyContact Contact = _myCompanyService.GetContactByID(_webstoreAuthorizationChecker.loginContactID());      

            Estimate LastOrder = _OrderService.GetLastOrderByContactId(_webstoreAuthorizationChecker.loginContactID());
            Company CorpCompany = _myCompanyService.GetCompanyByCompanyID(_webstoreAuthorizationChecker.loginContactCompanyID());

            CompanyTerritory CompTerritory = _myCompanyService.GetCcompanyByTerritoryID(_webstoreAuthorizationChecker.loginContactCompanyID());
            ViewBag.CurrencySymbol = StoreBaseResopnse.Currency;
            ViewBag.CorpCompany = CorpCompany;
            ViewBag.CompTerritory = CompTerritory;
            ViewBag.Order = LastOrder;
            if (StoreBaseResopnse.Company != null)
            {
                ViewData["Company"] = StoreBaseResopnse.Company;
            }

            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp && _webstoreAuthorizationChecker.loginContactID() == 0)
            {
                ViewBag.DefaultUrl = "/Login";
            }
            else
            {
                ViewBag.DefaultUrl = "/";
            }

            if (Contact != null)
            {
                return View("PartialViews/PersonalDetailAndOrderPolicy",Contact);
            }
            else
            {

                return View("PartialViews/PersonalDetailAndOrderPolicy",Contact);
            }
        }

        
        public ActionResult SaveOrderPolicy(string id)
        {
            Company CorpCompany = _myCompanyService.GetCompanyByCompanyID(_webstoreAuthorizationChecker.loginContactCompanyID());
            CorpCompany.CorporateOrderingPolicy = id;
           _myCompanyService.UpdateCompanyOrderingPolicy(CorpCompany);
            return View();
        }
    }
}