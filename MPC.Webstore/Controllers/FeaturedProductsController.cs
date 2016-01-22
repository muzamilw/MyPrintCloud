using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
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
    public class FeaturedProductsController : Controller
    {
        // GET: FeaturedProducts
        private readonly IItemService _itemService;
        private readonly ICompanyService _companyService;
        public FeaturedProductsController(IItemService _itemService, ICompanyService _companyService)
        {
            this._itemService = _itemService;
            this._companyService = _companyService;
        }

        public ActionResult Index()
        {
            List<Item> featuredProducts = _itemService.GetProductsWithDisplaySettings(ProductWidget.FeaturedProducts, UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
            MyCompanyDomainBaseReponse StoreBaseResopnse = _companyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            ViewBag.CurrencySymbol = _companyService.GetCurrencySymbolById(Convert.ToInt64(StoreBaseResopnse.Organisation.CurrencyId));
            return PartialView("PartialViews/FeaturedProducts", featuredProducts);
        }
    }
}