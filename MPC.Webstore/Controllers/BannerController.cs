using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;
using MPC.Interfaces.WebStoreServices;
using MPC.Webstore.ModelMappers;
using MPC.Webstore.Models;
using MPC.Webstore.ResponseModels;

namespace MPC.Webstore.Controllers
{

    public class BannerController : Controller
    {
         #region Private

        private readonly ICompanyService _myCompanyService;
        private readonly IWebstoreClaimsHelperService _webstoreAuthorizationChecker;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public BannerController(ICompanyService myCompanyService, IWebstoreClaimsHelperService webstoreAuthorizationChecker)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            if (webstoreAuthorizationChecker == null)
            {
                throw new ArgumentNullException("webstoreAuthorizationChecker");
            }
            this._myCompanyService = myCompanyService;
            this._webstoreAuthorizationChecker = webstoreAuthorizationChecker;
        }

        #endregion
     
        public ActionResult Index()
        {
            long storeId = Convert.ToInt64(Session["storeId"]);
            long organisationId = Convert.ToInt64(Session["organizationId"]);

            MyCompanyDomainBaseResponse baseResponse = _myCompanyService.GetStoreFromCache(_webstoreAuthorizationChecker.CompanyId()).CreateFromBanner();

            return PartialView("PartialViews/Banner", baseResponse.Banners);
        }
    }
}