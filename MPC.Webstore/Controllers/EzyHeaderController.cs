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
    public class EzyHeaderController : Controller
    {
        // GET: EzyHeader
         #region Private

        private readonly ICompanyService _myCompanyService;

        private readonly IWebstoreClaimsHelperService _myClaimHelper;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public EzyHeaderController(ICompanyService myCompanyService, IWebstoreClaimsHelperService myClaimHelper)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;
            this._myClaimHelper = myClaimHelper;
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



            return PartialView("PartialViews/Header", model);
        }
    }
}