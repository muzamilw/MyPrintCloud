using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
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
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        public RealEstateProductsController(IItemService IItemService, IWebstoreClaimsHelperService _myClaimHelper)
        {
            this._IItemService = IItemService;
            this._myClaimHelper = _myClaimHelper;
        }
        // GET: RealEstateProducts
        public ActionResult Index(string listingId)
        {

            long Lid = Convert.ToInt64(listingId);
            List<usp_GetRealEstateProducts_Result> lstRealEstateProducts = _IItemService.GetRealEstateProductsByCompanyID(UserCookieManager.WBStoreId);

            ViewData["RealEstateProducts"] = lstRealEstateProducts;
            ViewBag.ListingID = Lid;
            return View("PartialViews/RealEstateProducts");
        }
        public ActionResult CloneRealEstateItem(long id,long PropertyId)
        {
            ItemCloneResult cloneObject = _IItemService.CloneItemAndLoadDesigner(id,(StoreMode)UserCookieManager.WEBStoreMode, UserCookieManager.WEBOrderId, _myClaimHelper.loginContactID(), _myClaimHelper.loginContactCompanyID(), UserCookieManager.TemporaryCompanyId, UserCookieManager.WEBOrganisationID, PropertyId);
            UserCookieManager.TemporaryCompanyId = cloneObject.TemporaryCustomerId;
            UserCookieManager.WEBOrderId = cloneObject.OrderId;
            Response.Redirect(cloneObject.RedirectUrl);
            return null;
        }
    }
}