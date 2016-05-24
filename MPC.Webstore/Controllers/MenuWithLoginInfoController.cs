using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.ResponseModels;
using MPC.Webstore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class MenuWithLoginInfoController : Controller
    {
       #region Private

        private readonly ICompanyService _myCompanyService;

        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        private readonly IItemService _itemService;
        
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MenuWithLoginInfoController(ICompanyService myCompanyService, IWebstoreClaimsHelperService myClaimHelper
            , IItemService itemService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;
            this._myClaimHelper = myClaimHelper;
            this._itemService = itemService;
        }

        #endregion

        public ActionResult Index()
        {
            MPC.Models.DomainModels.Company model = null;
          
            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);


            if (StoreBaseResopnse.Company != null)
            {
                model = StoreBaseResopnse.Company;
            }

            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp && _myClaimHelper.loginContactID() == 0)
            {
                ViewBag.DefaultUrl = "/Login";
            }
            else
            {
                ViewBag.DefaultUrl = "/";
            }

            if (_myClaimHelper.loginContactID() > 0)
            {
                ViewBag.isUserLoggedIn = true;
                ViewBag.LoginUserName = UserCookieManager.WEBContactFirstName + " " + UserCookieManager.WEBContactLastName;//Response.Cookies["WEBFirstName"].Value; 
                ViewBag.CartCount = string.Format("{0}", _itemService.GetCartItemsCount(_myClaimHelper.loginContactID(), 0, _myClaimHelper.loginContactCompanyID()).ToString());

            }
            else
            {
                ViewBag.isUserLoggedIn = false;
                ViewBag.LoginUserName = "";
                ViewBag.CartCount = string.Format("{0}", _itemService.GetCartItemsCount(0, UserCookieManager.TemporaryCompanyId, 0).ToString());
            }

            return PartialView("PartialViews/MenuWithLoginInfo", model);
        }
    }
}