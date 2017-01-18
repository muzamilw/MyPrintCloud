using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Webstore.Common;

namespace MPC.Webstore.Controllers
{
    public class RaveReviewController : Controller
    {
        private readonly ICompanyService _myCompanyService;
        // GET: RaveReview
        public RaveReviewController(ICompanyService _myCompanyService)
        {
            this._myCompanyService = _myCompanyService;
        }
        public ActionResult Index()
        {
             ViewBag.RaveReview = _myCompanyService.GetRaveReview(UserCookieManager.WBStoreId);
           // RaveReview view = _myCompanyService.GetRaveReview();
           // string gg = ViewBag.RaveReview.Review;
             
             return PartialView("PartialViews/RaveReview");
        }
    }
}