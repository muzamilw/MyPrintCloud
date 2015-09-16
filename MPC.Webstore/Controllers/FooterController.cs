using MPC.Interfaces.WebStoreServices;
using MPC.Webstore.Common;
using MPC.Webstore.ModelMappers;
using MPC.Webstore.Models;
using MPC.Webstore.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using MPC.Models.Common;
using MPC.Webstore.ResponseModels;
using MPC.Webstore.ModelMappers;
using MPC.Models.ResponseModels;

namespace MPC.Webstore.Controllers
{
    public class FooterController : Controller
    {
        #region Private

        private readonly ICompanyService _myCompanyService;
        private readonly IWebstoreClaimsHelperService _webstoreAuthorizationChecker;

        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public FooterController(ICompanyService myCompanyService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;
        }

        #endregion
        // GET: Footer
        public ActionResult Index()
        {
            MPC.Models.DomainModels.Company model = null;

            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            if (StoreBaseResopnse.Company != null)
            {
                model = StoreBaseResopnse.Company;
            }

            ViewBag.TermAndCondition = null;
            ViewBag.PrivacyPolicy = null;

            if (StoreBaseResopnse.SecondaryPages != null) 
            {
                if (StoreBaseResopnse.SecondaryPages.Where(p => p.PageTitle.Contains("Terms & Conditions") && p.isUserDefined == true && p.isEnabled == true).Count() > 0) 
                {
                    ViewBag.TermAndCondition = StoreBaseResopnse.SecondaryPages.Where(p => p.PageTitle.Contains("Terms & Conditions") && p.isUserDefined == true && p.isEnabled == true).FirstOrDefault();
                }

                if (StoreBaseResopnse.SecondaryPages.Where(p => p.PageTitle.Contains("Privacy Policy") && p.isUserDefined == true && p.isEnabled == true).Count() > 0)
                {
                    ViewBag.PrivacyPolicy = StoreBaseResopnse.SecondaryPages.Where(p => p.PageTitle.Contains("Privacy Policy") && p.isUserDefined == true && p.isEnabled == true).FirstOrDefault();
                }
            }

            StoreBaseResopnse = null;
           
            return PartialView("PartialViews/Footer", model);
        }


    }
}