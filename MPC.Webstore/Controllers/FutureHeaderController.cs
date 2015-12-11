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
    public class FutureHeaderController : Controller
    {
        #region Private

        private readonly ICompanyService _myCompanyService;

        private readonly IWebstoreClaimsHelperService _myClaimHelper;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public FutureHeaderController(ICompanyService myCompanyService, IWebstoreClaimsHelperService myClaimHelper)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;
            this._myClaimHelper = myClaimHelper;
        }

        #endregion
        // GET: FutureHeader
        public ActionResult Index()
        {
            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            var categories = _myCompanyService.GetAllCategories(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
            List<ProductCategory> parentCategories = categories.Where(p => p.ParentCategoryId == null || p.ParentCategoryId == 0).OrderBy(s => s.DisplayOrder).ToList();
            
            if (parentCategories.Count > 5)
            {
                ViewData["ParentCats"] = parentCategories.Take(5).ToList();
                ViewBag.SeeMore = 1;
            }
            else
            {
                ViewData["ParentCats"] = parentCategories.ToList();
            }
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
            return PartialView("PartialViews/FutureHeader", StoreBaseResopnse.Company);
        }
    }
}