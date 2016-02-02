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
    public class BubbleLoginBarController : Controller
    {
        // GET: BubbleLoginBar
        private readonly IWebstoreClaimsHelperService _webstoreclaimHelper;

        private readonly IItemService _itemService;
        private readonly ICompanyService _myCompanyService;
        private readonly IUserManagerService _usermanagerService;
        public BubbleLoginBarController(IWebstoreClaimsHelperService _webstoreclaimHelper, IItemService _itemService, ICompanyService _myCompanyService, IUserManagerService _usermanagerService)
        {
            if (_webstoreclaimHelper == null)
            {
                throw new ArgumentNullException("webstoreClaimHelper");
            }
            this._webstoreclaimHelper = _webstoreclaimHelper;

            if (_itemService == null)
            {
                throw new ArgumentNullException("itemService");
            }
            this._itemService = _itemService;
            this._myCompanyService = _myCompanyService;
            this._usermanagerService = _usermanagerService;
        }
        public ActionResult Index()
        {
            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);
            SystemUser SystemUser = _usermanagerService.GetSalesManagerDataByID(StoreBaseResopnse.Company.SalesAndOrderManagerId1.Value);
            if (_webstoreclaimHelper.isUserLoggedIn())
            {
                ViewBag.isUserLoggedIn = true;
                ViewBag.LoginUserName = UserCookieManager.WEBContactFirstName + " " + UserCookieManager.WEBContactLastName;//Response.Cookies["WEBFirstName"].Value; 
                ViewBag.CartCount = string.Format("{0}", _itemService.GetCartItemsCount(_webstoreclaimHelper.loginContactID(), 0, _webstoreclaimHelper.loginContactCompanyID()).ToString());
            }
            else
            {
                ViewBag.isUserLoggedIn = false;
                ViewBag.LoginUserName = "";
                ViewBag.CartCount = string.Format("{0}", _itemService.GetCartItemsCount(0, UserCookieManager.TemporaryCompanyId, 0).ToString());
            }
            ViewBag.email = SystemUser.Email;
            ViewBag.Phone = StoreBaseResopnse.Company.PhoneNo;
            ViewBag.CompanyName = StoreBaseResopnse.Company.Name;
            return PartialView("PartialViews/BubbleLoginBar");
        }
    }
}