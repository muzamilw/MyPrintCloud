using MPC.Interfaces.WebStoreServices;
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

        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SocialAnSavedDesignInfoController(ICompanyService myCompanyService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;
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

            return PartialView("PartialViews/SocialAnSavedDesignInfo", model);
        }
    }
}