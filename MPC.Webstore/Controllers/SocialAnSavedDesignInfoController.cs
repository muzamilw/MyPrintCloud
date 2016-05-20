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
    public class SocialAnSavedDesignInfoController : Controller
    {
        #region Private

        private readonly ICompanyService _myCompanyService;
        private readonly IWebstoreClaimsHelperService _webstoreAuthorizationChecker;
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        private readonly IItemService _ItemService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SocialAnSavedDesignInfoController(ICompanyService myCompanyService, IWebstoreClaimsHelperService myClaimHelper, IItemService ItemService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;
            this._myClaimHelper = myClaimHelper;
            this._ItemService = ItemService;
        }

        #endregion
        // GET: SocialAnSavedDesignInfo
        public ActionResult Index()
        {
            MPC.Models.DomainModels.Company model = null;

            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            if (StoreBaseResopnse.Company != null)
            {
                model = StoreBaseResopnse.Company;
            }
            StoreBaseResopnse = null;
            if (_myClaimHelper.loginContactID() > 0)
            {
                ViewBag.isUserLoggedIn = true;
                List<SaveDesignView> designs = _ItemService.GetSavedDesigns(_myClaimHelper.loginContactID());

                if (designs != null && designs.Count > 0)
                {
                    ViewBag.CountOfSavedDesign = designs.Count;

                }
                else
                {
                    ViewBag.CountOfSavedDesign = 0;
                }
            }
            else
            {
                ViewBag.isUserLoggedIn = false;
                ViewBag.CountOfSavedDesign = 0;
            }


            return PartialView("PartialViews/SocialAnSavedDesignInfo", model);
        }
    }
}