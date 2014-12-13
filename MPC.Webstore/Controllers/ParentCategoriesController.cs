using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Interfaces.WebStoreServices;
using MPC.Webstore.Common;

namespace MPC.Webstore.Controllers
{
    public class ParentCategoriesController : Controller
    {
          #region Private

        private readonly ICompanyService _myCompanyService;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ParentCategoriesController(ICompanyService myCompanyService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;
        }

        #endregion
        // GET: ParentCategories
        public ActionResult Index()
        {
            var model = _myCompanyService.GetCompanyParentCategoriesById(UserCookieManager.StoreId);
            return PartialView("PartialViews/ParentCategories", model);
        }
    }
}