using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;

namespace MPC.Webstore.Controllers
{
    
    public class FeaturedProductCarousalController : Controller
    {
         #region Private

        private readonly IItemService _itemService;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public FeaturedProductCarousalController(IItemService itemService)
        {
            if (itemService == null)
            {
                throw new ArgumentNullException("itemService");
            }
            this._itemService = itemService;
        }

        #endregion
        // GET: FeaturedProductCarousal
        public ActionResult Index()
        {
            List<Item> featuredProducts = _itemService.GetProductsWithDisplaySettings(ProductWidget.FeaturedProducts, UserCookieManager.StoreId, UserCookieManager.OrganisationID);

            ViewBag.ProductsCount = featuredProducts.Count;
            return PartialView("PartialViews/FeaturedProductCarousal", featuredProducts);
         
        }
    }
}