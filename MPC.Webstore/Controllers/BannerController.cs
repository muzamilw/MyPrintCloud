using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;

namespace MPC.Webstore.Controllers
{
    public class BannerController : Controller
    {
        #region Private

        private readonly ICompanyBannerSetService _bannerService;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public BannerController(ICompanyBannerSetService bannerService)
        {
            if (bannerService == null)
            {
                throw new ArgumentNullException("bannerService");
            }
            this._bannerService = bannerService;
        }

        #endregion
        // GET: News
        public ActionResult Index()
        {
            var model = Session["store"] as Company;
            if (model != null)
            {
                var bannerSet = _bannerService.GetCompanyBannersById(model.CompanyId, model.OrganisationId ?? 0);

                ViewBag.sets = bannerSet;
            }
            return PartialView("PartialViews/Banner");
        }
    }
}