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
    public class PopularProductsController : Controller
    {
        // GET: PopularProducts
        private readonly IItemService _IItemService;
        public PopularProductsController(IItemService _IItemService)
        {
            this._IItemService = _IItemService;
        }
        public ActionResult Index()
        {
            List<Item> PopularProducts = _IItemService.GetProductsWithDisplaySettings(ProductWidget.PopularProducts, UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
            return PartialView("PartialViews/PopularProducts", PopularProducts);
        }
    }
}