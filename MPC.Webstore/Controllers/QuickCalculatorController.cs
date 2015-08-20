using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
using MPC.Webstore.Common;
using MPC.Webstore.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class QuickCalculatorController : Controller
    {
        // GET: QuickCalculator
        private readonly IItemRepository _itemRepository;
        private readonly ICompanyService _myCompanyService;
        private readonly IWebstoreClaimsHelperService _webstoreAuthorizationChecker;
        public QuickCalculatorController(IItemRepository _itemRepository, IWebstoreClaimsHelperService _webstoreAuthorizationChecker
            , ICompanyService myCompanyService)
        {
            this._itemRepository = _itemRepository;
            this._webstoreAuthorizationChecker = _webstoreAuthorizationChecker;
            this._myCompanyService = myCompanyService;
        }
        public ActionResult Index()
        {
            //string CacheKeyName = "CompanyBaseResponse";
            //ObjectCache cache = MemoryCache.Default;
            //MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Retail)
            {
                if (StoreBaseResopnse.Company.isIncludeVAT == true)
                {
                    ViewBag.VAT = "inc.VAT ";
                }
                else
                {
                    ViewBag.VAT = string.Empty;
                }
            }
            ViewBag.StoreMode = UserCookieManager.WEBStoreMode;
            ViewBag.CurrencySymbol = StoreBaseResopnse.Currency;
            ViewBag.BCID = ConfigurationSettings.AppSettings["BizCardCategory"].ToString();
            ViewBag.TaxRate = StoreBaseResopnse.Company.TaxRate;
            return PartialView("PartialViews/QuickCalculator");
        }
        [HttpGet]
        public JsonResult GetAllProducts(string cID,string mode)
        {
            var Products = _itemRepository.GetAllRetailDisplayProductsQuickCalc(UserCookieManager.WBStoreId).OrderBy(p=>p.SortOrder);
            //foreach (var item in Products)
            // {

            //     Dictionary<string, string> pagParams = new Dictionary<string, string>();

            //     pagParams.Add(ParameterName.CATEGORY_ID, item.ProductCategoryID.ToString());
            //     pagParams.Add(ParameterName.ITEM_ID, item.ItemID.ToString());

            //     if (item.IsFinishedGoods == true)
            //     {
            //         pagParams.Add(ParameterName.Mode, ConstantsValues.UploadDesign);

            //         item.NavPath = Utils.BuildQueryStringForFinishedGood("pd", item.ProductName, pagParams);
            //     }
            //     else
            //     {
            //         item.NavPath = Utils.BuilQueryStringProduct("p", item.ProductName, pagParams);
            //     }


            //     if (item.ProductCategoryID == Convert.ToInt32(ConfigurationManager.AppSettings["BizCardCategory"]))
            //     {
            //         item.IsSelectedBizCard = 1;
            //     }
            //     else
            //     {
            //         item.IsSelectedBizCard = 0;
            //     }

            // }
            return Json(Products, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetQuantityPrises(string cID, string mode)
        {
            var QuantityPrizes = _itemRepository.GetRetailProductsPriceMatrix(UserCookieManager.WBStoreId);
            return Json(QuantityPrizes, JsonRequestBehavior.AllowGet);
            
        }
    }
}