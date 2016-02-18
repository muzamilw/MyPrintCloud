using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
using MPC.Webstore.Common;
using MPC.Webstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class SystemPageBannerController : Controller
    {
        // GET: SystemPageBanner
           #region Private

        private readonly ICompanyService _myCompanyService;


        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SystemPageBannerController(ICompanyService myCompanyService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
           
            this._myCompanyService = myCompanyService;
        }

        #endregion
        public ActionResult Index()
        {
            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            long PageId =   Convert.ToInt64(TempData["systemPageId"]);

            MPC.Models.Common.CmsPageModel Page = StoreBaseResopnse.SystemPages.Where(p => p.PageId == PageId).FirstOrDefault();


            return View("PartialViews/SystemPageBanner", Page);
        }
    }
}