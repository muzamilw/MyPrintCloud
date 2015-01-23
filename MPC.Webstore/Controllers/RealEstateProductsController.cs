using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using MPC.Webstore.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class RealEstateProductsController : Controller
    {
        private readonly IItemService _IItemService;

        public RealEstateProductsController(IItemService IItemService)
        {
            this._IItemService = IItemService;      
        }
        // GET: RealEstateProducts
        public ActionResult Index(int listingId)
        {
            List<usp_GetRealEstateProducts_Result> lstRealEstateProducts = _IItemService.GetRealEstateProductsByCompanyID(UserCookieManager.StoreId);

            ViewData["RealEstateProducts"] = lstRealEstateProducts;

            return View("PartialViews/RealEstateProducts", listingId);
        }
    }
}