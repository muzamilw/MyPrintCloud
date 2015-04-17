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
    public class PromotionalProductController : Controller
    {
        // GET: PromotionalProduct
        private readonly IItemService _IItemService;
        public PromotionalProductController(IItemService _IItemService)
        {
            this._IItemService = _IItemService;
        }
        public ActionResult Index()
        {
            List<Item> PromotionalProducts = _IItemService.GetProductsWithDisplaySettings(ProductWidget.SpecialProducts, UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);

            return PartialView("PartialViews/PromotionalProduct", PromotionalProducts);
            
        }
    }
}