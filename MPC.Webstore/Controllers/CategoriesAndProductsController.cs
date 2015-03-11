using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Models.DomainModels;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Webstore.Common;

namespace MPC.Webstore.Controllers
{
    public class CategoriesAndProductsController : Controller
    {
          #region Private

        private readonly IItemService _itemService;
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        private readonly IOrderService _orderService;
        private readonly ICompanyService _myCompanyService;
        private readonly IItemService _IItemService;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CategoriesAndProductsController(IItemService itemService, IWebstoreClaimsHelperService myClaimHelper, IOrderService orderService, ICompanyService myCompanyService)
        {
            if (itemService == null)
            {
                throw new ArgumentNullException("itemService");
            }
            this._myClaimHelper = myClaimHelper;
            this._itemService = itemService;
            this._orderService = orderService;
            this._myCompanyService = myCompanyService;
            this._IItemService = itemService;
        }

        #endregion
        // GET: CategoriesAndProducts
        public ActionResult Index()
        {
            List<ProductCategory> categories = _itemService.GetStoreParentCategories(UserCookieManager.StoreId, UserCookieManager.OrganisationID);
            ViewData["Categories"] = categories.Where(p => p.ParentCategoryId == null).ToList();
            ViewData["childCategory"] = categories.Where(p => p.ParentCategoryId != null).ToList();
            ViewData["FeaturedProducts"] = _itemService.GetProductsWithDisplaySettings(ProductWidget.FeaturedProducts, UserCookieManager.StoreId, UserCookieManager.OrganisationID);
            
            return PartialView("PartialViews/CategoriesAndProducts");
         
        }
    }
}