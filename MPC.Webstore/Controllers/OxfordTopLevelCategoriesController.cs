﻿using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class OxfordTopLevelCategoriesController : Controller
    {
        private readonly ICompanyService _myCompanyService;
        private readonly IWebstoreClaimsHelperService _myClaimHelper;

        public OxfordTopLevelCategoriesController(ICompanyService _myCompanyService, IWebstoreClaimsHelperService _myClaimHelper)
        {
            this._myCompanyService = _myCompanyService;
            this._myClaimHelper = _myClaimHelper;
        }
        // GET: OxfordTopLevelCategories
        public ActionResult Index()
        {
            GetRaReview();
            List<ProductCategory> lstParentCategories = new List<ProductCategory>();
            List<ProductCategory> AllRetailCat = new List<ProductCategory>();
            MPC.Models.DomainModels.Company model = null;
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;
            //   MyCompanyDomainBaseResponse baseResponse = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCompany();
            MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
            {

                Int64 roleid = _myClaimHelper.loginContactRoleID();

                if (_myClaimHelper.loginContactID() != 0 && roleid == Convert.ToInt32(Roles.Adminstrator))
                {
                    lstParentCategories = _myCompanyService.GetAllParentCorporateCatalog((int)_myClaimHelper.loginContactCompanyID());
                }
                else
                {
                    lstParentCategories = _myCompanyService.GetAllParentCorporateCatalogByTerritory((int)_myClaimHelper.loginContactCompanyID(), (int)_myClaimHelper.loginContactID());
                }

            }
            else
            {

                List<ProductCategory> AllCategroies = new List<ProductCategory>();
                //List<ProductCategory> ChildCategories = new List<ProductCategory>();
                AllCategroies = _myCompanyService.GetAllCategories(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
                //SeablueToCategories
              ViewBag.lstParentCategories = _myCompanyService.GetStoreParentCategories(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID).OrderBy(i=>i.DisplayOrder).ToList();
                //rptRetroPCats
             // ViewBag.AllRetailCat = _myCompanyService.GetAllRetailPublishedCat().Where(i => i.ParentCategoryId == null || i.ParentCategoryId == 0).OrderBy(g => g.DisplayOrder).ToList();
              ViewBag.AllRetailCat = _myCompanyService.GetAllCategories(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID).ToList();
            }
             string  State = _myCompanyService.GetStateNameById(StoreBaseResopnse.StoreDetaultAddress.StateId ?? 0);
             string  Country = _myCompanyService.GetCountryNameById(StoreBaseResopnse.StoreDetaultAddress.CountryId ?? 0);
            ViewBag.companynameInnerText = StoreBaseResopnse.Company.Name;
            ViewBag.addressline1InnerHtml = StoreBaseResopnse.StoreDetaultAddress.Address1 + "<br/>" + StoreBaseResopnse.StoreDetaultAddress.Address2;
            ViewBag.cityandCodeInnerText = StoreBaseResopnse.StoreDetaultAddress.City + " " + StoreBaseResopnse.StoreDetaultAddress.PostCode;
            ViewBag.stateandCountryInnerText = State + " " +Country;
            ViewBag.telnoInnerText = StoreBaseResopnse.StoreDetaultAddress.Tel1;
            ViewBag.emailaddInnerText = StoreBaseResopnse.StoreDetaultAddress.Email;
            return PartialView("PartialViews/OxfordTopLevelCategories");
        }

        public void GetRaReview()
        {
            RaveReview resultOfReviews = _myCompanyService.GetRaveReview();
            if (resultOfReviews != null)
            {
                ViewBag.lblRaveReview = "<br /> " + resultOfReviews.Review + "<br /> Best regards,<br /> &nbsp;";
                ViewBag.lblReviewBy = resultOfReviews.ReviewBy;
            }
            else
            {
                ViewBag.lblRaveReview = "I used Company services for my business cards and I must tell that I am much&nbsp; pleased with the quality of printed cards their prompt and professional service. Good luck to your business.<br />Best regards,<br />&nbsp;";
                ViewBag.lblReviewBy = "Henry Roberts";
            }
        }
    }
}