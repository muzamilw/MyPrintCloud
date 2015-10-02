using MPC.Interfaces.WebStoreServices;
using MPC.Models.ResponseModels;
using MPC.Webstore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class RealEstateFooterController : Controller
    {
        // GET: RealEstateFooter
        private readonly ICompanyService _myCompanyService;
        public RealEstateFooterController(ICompanyService _myCompanyService)
        {
            this._myCompanyService = _myCompanyService;
        }
        public ActionResult Index()
        {
            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);
            ViewBag.Company = StoreBaseResopnse.Company;
            return View("PartialViews/RealEstateFooter");
            
        }
    }
}