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
    public class BubbleSavePercentController : Controller
    {
        // GET: BubbleSavePercent
        private readonly IItemService _ItemService;
        private readonly ICompanyService _myCompanyService;

        public BubbleSavePercentController(IItemService _ItemService, ICompanyService _myCompanyService)
        {
            this._ItemService = _ItemService;
            this._myCompanyService = _myCompanyService;
        }

        public ActionResult Index()
        {
            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            DiscountVoucher Voucher = _ItemService.GetFreeShippingDiscountVoucherByStoreId(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
            double OrderTotalPrice = 0;
            if (Voucher != null)
            {

                 OrderTotalPrice = Voucher.DiscountRate;

            }

            ViewBag.OrderPercentage = OrderTotalPrice + "%";
            return PartialView("PartialViews/BubbleSavePercent");
        }
    }
}