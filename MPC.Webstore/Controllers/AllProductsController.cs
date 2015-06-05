using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Interfaces.WebStoreServices;
using MPC.Webstore.Common;

namespace MPC.Webstore.Controllers
{
    public class AllProductsController : Controller
    {
        #region Private

        private readonly ICompanyService _myCompanyService;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public AllProductsController(ICompanyService myCompanyService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;
        }

        #endregion
        // GET: AllProducts
        public ActionResult Index()
        {

            var model = _myCompanyService.GetAllCategories(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
            ViewData["ParentCats"] = model.Where(p => p.ParentCategoryId == null || p.ParentCategoryId == 0).OrderBy(s => s.DisplayOrder).ToList();
            ViewData["SubCats"] = model.Where(p => p.ParentCategoryId != null || p.ParentCategoryId != 0).OrderBy(s => s.DisplayOrder).ToList();
            return View("PartialViews/AllProducts");
        }

  
    }
}