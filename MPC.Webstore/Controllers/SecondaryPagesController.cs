using MPC.Interfaces.WebStoreServices;
using MPC.Webstore.ModelMappers;
using MPC.Webstore.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class SecondaryPagesController : Controller
    {
         #region Private

        private readonly ICompanyService _myCompanyService;

        private readonly IWebstoreClaimsHelperService _webstoreAuthorizationChecker;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SecondaryPagesController(ICompanyService myCompanyService, IWebstoreClaimsHelperService webstoreAuthorizationChecker)
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
        // GET: SecondaryPages
        public ActionResult Index()
        {

            MyCompanyDomainBaseResponse baseResponse = _myCompanyService.GetStoreFromCache(_webstoreAuthorizationChecker.CompanyId()).CreateFromSecondaryPages();

            ViewData["PageCategory"] = baseResponse.PageCategories;
            ViewData["CmsPage"] = baseResponse.SecondaryPages;
          
            return PartialView("PartialViews/SecondaryPages");
        }
    }
}