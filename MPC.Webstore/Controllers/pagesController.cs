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
        public ActionResult Index(string PageID)
        {
            if (PageID.Contains("www."))
            {
                TempData["ErrorMessage"] = "Your url is invalid.";
                TempData["InvalidUrl"] = Request.Url.AbsoluteUri;
                Response.Redirect("/Error");
                return null;
            }
            else 
            {
                CmsPage SPage = _myCompanyService.getPageByID(Convert.ToInt64(PageID));

                return PartialView("PartialViews/pages", SPage);
            }
            
        }
    }
}