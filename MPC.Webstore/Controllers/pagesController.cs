using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Models.DomainModels;
using MPC.Interfaces.WebStoreServices;

namespace MPC.Webstore.Controllers
{
    public class pagesController : Controller
    {

          #region Private

        private readonly ICompanyService _myCompanyService;


        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public pagesController(ICompanyService myCompanyService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
           
            this._myCompanyService = myCompanyService;
        }

        #endregion
        // GET: SecondaryPages
        public ActionResult Index(long PageID)
        {

            CmsPage SPage = _myCompanyService.getPageByID(PageID);

            return PartialView("PartialViews/pages", SPage);
        }
    }
}