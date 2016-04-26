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
    public class BubbleLoginBarController : Controller
    {
        // GET: BubbleLoginBar
        private readonly IWebstoreClaimsHelperService _webstoreclaimHelper;

        private readonly IItemService _itemService;
        private readonly ICompanyService _myCompanyService;
        private readonly IUserManagerService _usermanagerService;
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        public BubbleLoginBarController(IWebstoreClaimsHelperService _webstoreclaimHelper, IItemService _itemService, ICompanyService _myCompanyService, IUserManagerService _usermanagerService, IWebstoreClaimsHelperService _myClaimHelper)
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
            this._myClaimHelper = _myClaimHelper;
        }
        public ActionResult Index()
        {
            MPC.Models.DomainModels.Company model = null;
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
            if (_webstoreclaimHelper.loginContactID() > 0)
            {
                ViewBag.IsLogin = 1;
            }
            else
            {
                ViewBag.IsLogin = 0;
            }
            ViewBag.email = SystemUser.Email;
            ViewBag.Phone = StoreBaseResopnse.Company.PhoneNo;
            ViewBag.CompanyName = StoreBaseResopnse.Company.Name;

            if (StoreBaseResopnse.Company != null)
            {
                model = StoreBaseResopnse.Company;
            }

            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp && _webstoreclaimHelper.loginContactID() == 0)
            {
                ViewBag.DefaultUrl = "/Login";
            }
            else
            {
                ViewBag.DefaultUrl = "/";
            }



            //bubbleheader

          

            var categories = _myCompanyService.GetAllCategories(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
            List<ProductCategory> parentCategories = categories.Where(p => p.ParentCategoryId == null || p.ParentCategoryId == 0).OrderBy(s => s.DisplayOrder).ToList();
            //if (parentCategories.Count > 5)
            //{
            //    ViewData["ParentCats"] = parentCategories.Take(5).ToList();
            //    ViewBag.SeeMore = 1;
            //}
            //else
            //{
                ViewData["ParentCats"] = parentCategories.ToList();
          //  }
            ViewData["SubCats"] = categories.Where(p => p.ParentCategoryId != null || p.ParentCategoryId != 0).OrderBy(s => s.DisplayOrder).ToList();
            ViewBag.AboutUs = null;
            if (StoreBaseResopnse.SecondaryPages != null)
            {
                if (StoreBaseResopnse.SecondaryPages.Where(p => p.PageTitle.Contains("About Us") && p.isUserDefined == true && p.isEnabled == true).Count() > 0)
                {
                    ViewBag.AboutUs = StoreBaseResopnse.SecondaryPages.Where(p => p.PageTitle.Contains("About Us") && p.isUserDefined == true && p.isEnabled == true).FirstOrDefault();
                }

            }
            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp && _myClaimHelper.loginContactID() == 0)
            {
                ViewBag.DefaultUrl = "/Login";
            }
            else
            {
                ViewBag.DefaultUrl = "/";
            }

            return PartialView("PartialViews/BubbleLoginBar", model);
        }
    }
}