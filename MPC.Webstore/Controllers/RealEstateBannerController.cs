using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;
using MPC.Interfaces.WebStoreServices;
using MPC.Webstore.Common;
using MPC.Webstore.ModelMappers;
using MPC.Webstore.Models;
using MPC.Webstore.ResponseModels;

namespace MPC.Webstore.Controllers
{
    public class RealEstateBannerController : Controller
    {
        #region Private

        private readonly ICompanyService _myCompanyService;
      
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public RealEstateBannerController(ICompanyService myCompanyService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;
        }

        #endregion

        // GET: RealEstateBanner
        public ActionResult Index()
        {
            MyCompanyDomainBaseResponse baseResponse = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromBanner();

            return View("PartialViews/RealEstateBanner", baseResponse.Banners);
        }
    }
}