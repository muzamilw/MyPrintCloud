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

            DiscountVoucher Voucher = _ItemService.GetFreeShippingDiscountVoucherByStoreId(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
            double OrderTotalPrice = 0;
            if (Voucher != null)
            {
                if (Voucher.IsOrderPriceRequirement.HasValue && Voucher.IsOrderPriceRequirement.Value == true)
                {
                    if (Voucher.MinRequiredOrderPrice.HasValue && Voucher.MinRequiredOrderPrice.Value > 0)
                    {
                        OrderTotalPrice = Voucher.MinRequiredOrderPrice ?? 0;
                        
                    }

                    if (Voucher.MaxRequiredOrderPrice.HasValue && Voucher.MaxRequiredOrderPrice.Value > 0 && OrderTotalPrice == 0)
                    {
                        OrderTotalPrice = Voucher.MaxRequiredOrderPrice ?? 0;
                        
                    }
                }
            }

            ViewBag.OrderTotal = StoreBaseResopnse.Currency + OrderTotalPrice;

            Voucher = _ItemService.GetOrderDiscountPercentageVoucherByStoreId(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
            double OrderPercentage = 0;
            double OrderPercentageAmount = 0;
            if (Voucher != null)
            {
                OrderPercentage = Voucher.DiscountRate;
                if (Voucher.IsOrderPriceRequirement.HasValue && Voucher.IsOrderPriceRequirement.Value == true)
                {
                    if (Voucher.MinRequiredOrderPrice.HasValue && Voucher.MinRequiredOrderPrice.Value > 0)
                    {
                        OrderPercentageAmount = Voucher.MinRequiredOrderPrice ?? 0;
                    }

                    if (Voucher.MaxRequiredOrderPrice.HasValue && Voucher.MaxRequiredOrderPrice.Value > 0 && OrderPercentageAmount == 0)
                    {
                        OrderPercentageAmount = Voucher.MaxRequiredOrderPrice ?? 0;
                    }
                }
            }

            ViewBag.OrderPercentageTotal = StoreBaseResopnse.Currency + OrderPercentageAmount;
            ViewBag.OrderPercentage = OrderPercentage + "%";

            if(StoreBaseResopnse.StoreDetaultAddress != null)
            {
                ViewBag.Country = StoreBaseResopnse.StoreDetaultAddress.Country;
            }

            return View("PartialViews/WhyShopUs");
        }
    }
}