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
    public class BubbleAboutUSController : Controller
    {
        // GET: BubbleAboutUS
        private readonly ICompanyService _myCompanyService;
        private readonly IWebstoreClaimsHelperService _webstoreAuthorizationChecker;
        public BubbleAboutUSController(ICompanyService myCompanyService)
        { 
        if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;
        }

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
                if (StoreBaseResopnse.SecondaryPages.Where(p => p.PageTitle.Contains("About Us") && p.isUserDefined == true && p.isEnabled == true).Count() > 0)
                {
                    ViewBag.AboutUS = StoreBaseResopnse.SecondaryPages.Where(p => p.PageTitle.Contains("About Us") && p.isUserDefined == true && p.isEnabled == true).FirstOrDefault();
                }
                
            }

            StoreBaseResopnse = null;
            return PartialView("PartialViews/BubbleAboutUS");
        }
    }
}