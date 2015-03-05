using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Webstore.Common;
using MPC.Webstore.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Webstore.ModelMappers;
using System.Runtime.Caching;

namespace MPC.Webstore.Controllers
{
    public class ReceiptController : Controller
    {
        private readonly IOrderService _OrderService;
        private readonly ICompanyService _myCompanyService;

        public ReceiptController(IOrderService OrderService, ICompanyService myCompanyService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            if (OrderService == null)
            {
                throw new ArgumentNullException("OrderService");
            }
            this._myCompanyService = myCompanyService;
            this._OrderService = OrderService;
         }
        // GET: Receipt
        public ActionResult Index(string OrderId)
        {
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;

            //MyCompanyDomainBaseResponse baseResponseOrganisation = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromOrganisation();
            //MyCompanyDomainBaseResponse baseResponseCompany = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCompany();
            //MyCompanyDomainBaseResponse baseResponseCurrency = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCurrency();
            MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.StoreId];



            if (StoreBaseResopnse.Company.ShowPrices ?? true)
            {
                ViewBag.IsShowPrices = true;
                //do nothing because pricing are already visible.
            }
            else
            {
                ViewBag.IsShowPrices = false;
                //  cntRightPricing1.Visible = false;
            }
            if (!string.IsNullOrEmpty(StoreBaseResopnse.Currency))
            {
                ViewBag.Currency = StoreBaseResopnse.Currency;
            }
            else
            {
                ViewBag.Currency = "";
            }

            ViewBag.TaxLabel = StoreBaseResopnse.Company.TaxLabel;
           OrderDetail order =  _OrderService.GetOrderReceipt(Convert.ToInt64(OrderId));

           ViewBag.Organisation = StoreBaseResopnse.Organisation;

           return View("PartialViews/Receipt", order);
        }

        
    }
}