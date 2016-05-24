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
    public class SliderGalleryController : Controller
    {
         #region Private

        private readonly ICompanyService _myCompanyService;
      
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SliderGalleryController(ICompanyService myCompanyService)
            
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;
        }

        #endregion

        // GET: SliderGallery
        public ActionResult Index()
        {
            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            return PartialView("PartialViews/SliderGallery", StoreBaseResopnse.Banners);
        }
    }
}