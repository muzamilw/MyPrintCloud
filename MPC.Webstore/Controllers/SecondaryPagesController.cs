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

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SecondaryPagesController(ICompanyService myCompanyService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;
        }

        #endregion
        // GET: SecondaryPages
        public ActionResult Index()
        {
            long storeId = Convert.ToInt64(Session["storeId"]);

            MyCompanyDomainBaseResponse baseResponse = _myCompanyService.GetBaseData(storeId).CreateFromSecondaryPages();

            ViewData["PageCategory"] = baseResponse.PageCategories;
            ViewData["CmsPage"] = baseResponse.SystemPages;
          
            return PartialView("PartialViews/SecondaryPages");
        }
    }
}