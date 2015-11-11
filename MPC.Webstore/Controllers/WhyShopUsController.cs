using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
using MPC.Webstore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class WhyShopUsController : Controller
    {
        
        private readonly IItemService _ItemService;
        private readonly ICompanyService _myCompanyService;

        public WhyShopUsController(ICompanyService myCompanyService, IItemService ItemService)
        {
           
            this._myCompanyService = myCompanyService;
            this._ItemService = ItemService;
        }
        // GET: WhyShopUs
        public ActionResult Index()
        {
            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            DiscountVoucher FreeShippingVoucher = _ItemService.GetFreeShippingDiscountVoucherByStoreId(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
            double OrderTotalPrice = 0;
            if (FreeShippingVoucher != null)
            {
                if (FreeShippingVoucher.IsOrderPriceRequirement.HasValue && FreeShippingVoucher.IsOrderPriceRequirement.Value == true)
                {
                    if (FreeShippingVoucher.MinRequiredOrderPrice.HasValue && FreeShippingVoucher.MinRequiredOrderPrice.Value > 0)
                    {
                        OrderTotalPrice = FreeShippingVoucher.MinRequiredOrderPrice ?? 0;
                        
                    }

                    if (FreeShippingVoucher.MaxRequiredOrderPrice.HasValue && FreeShippingVoucher.MaxRequiredOrderPrice.Value > 0 && OrderTotalPrice == 0)
                    {
                        OrderTotalPrice = FreeShippingVoucher.MaxRequiredOrderPrice ?? 0;
                        
                    }
                }
            }
            ViewBag.OrderTotal = StoreBaseResopnse.Currency + OrderTotalPrice;
            if(StoreBaseResopnse.StoreDetaultAddress != null)
            {
                ViewBag.Country = StoreBaseResopnse.StoreDetaultAddress.Country;
            }
           
            return View();
        }
    }
}