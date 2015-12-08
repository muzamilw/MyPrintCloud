
using MPC.Interfaces.WebStoreServices;
using MPC.Webstore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class RealEstateListingGridController : Controller
    {
        // GET: RealEstateListingGrid
        private readonly ICompanyService _MyCompanyService;
      //  private readonly MPC.Interfaces.WebStoreServices.IWebstoreClaimsHelperService _webstoreAuthorizationChecker;
        private readonly IListingService _ListingService;

        public RealEstateListingGridController(ICompanyService _MyCompanyService, IListingService _ListingService)
        {
            this._MyCompanyService = _MyCompanyService;
            this._ListingService = _ListingService;
        }
        public ActionResult Index()
        {
            ViewBag.lstProperties = _ListingService.GetPropertiesByContactCompanyID(UserCookieManager.WBStoreId);
            ViewBag.AllListingImages = _ListingService.GetAllListingImages();
            return PartialView("PartialViews/RealEstateListingGrid");
        }
    }
}