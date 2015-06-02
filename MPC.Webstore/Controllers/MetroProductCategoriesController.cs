﻿using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class MetroProductCategoriesController : Controller
    {
        private ICompanyService _myCompanyService;
        // GET: MetroProductCategories
        public MetroProductCategoriesController(ICompanyService _myCompanyService)
        {
            this._myCompanyService=_myCompanyService;
        }
        public ActionResult Index()
        {
            List<ProductCategory> AllCategroies = new List<ProductCategory>();
            //List<ProductCategory> ChildCategories = new List<ProductCategory>();
            AllCategroies = _myCompanyService.GetAllCategories(UserCookieManager.WBStoreId);

            ViewBag.ParentCategory = AllCategroies.Where(i => i.ParentCategoryId == null || i.ParentCategoryId == 0).OrderBy(g => g.DisplayOrder).ToList();

           // ChildCategories=_myCompanyService.GetAllCategories(UserCookieManager.WBStoreId);

            ViewBag.AllChildCategories = AllCategroies.Where(i => i.ParentCategoryId != null || i.ParentCategoryId != 0).OrderBy(g => g.DisplayOrder).ToList();
            return View("PartialViews/MetroProductCategories");
        }
      

   }
    
}