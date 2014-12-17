using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using MPC.Webstore.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Webstore.ResponseModels;
using MPC.Webstore.ModelMappers;

namespace MPC.Webstore.Controllers
{
    public class CategoryController : Controller
    {

        #region Private

        private readonly ICompanyService _myCompanyService;
        private readonly IWebstoreClaimsHelperService _myClaimHelper;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CategoryController(ICompanyService myCompanyService,IWebstoreClaimsHelperService myClaimHelper)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
           
            this._myCompanyService = myCompanyService;
            this._myClaimHelper = myClaimHelper;
        }

        #endregion
        // GET: Category
        public ActionResult Index(string name,string id)
        {
            int CategoryID = Convert.ToInt32(id);
            ProductCategory Category = _myCompanyService.GetCategoryById(CategoryID);

            if (Category != null)
            {

               SetPageMEtaTitle(Category.CategoryName, Category.MetaDescription, Category.MetaKeywords, Category.MetaTitle);

                List<ProductCategory> subCategoryList = new List<ProductCategory>();

                if(UserCookieManager.StoreMode == (int)StoreMode.Corp) // corporate case
                {
                    if (_myClaimHelper.loginContactRoleID() == Convert.ToInt32(Roles.Adminstrator))
                    {
                        subCategoryList = _myCompanyService.GetChildCategories(CategoryID);
                    }
                    else
                    {
                        subCategoryList = _myCompanyService.GetAllChildCorporateCatalogByTerritory((int)_myClaimHelper.loginContactCompanyID(),(int) _myClaimHelper.loginContactID(), CategoryID);
                    }

                }
                else // retail case
                {
                    subCategoryList = _myCompanyService.GetChildCategories(CategoryID);
                }

                BindCategoryData(subCategoryList);

                //var productList = null; // pMgr.GetRetailOrCorpPublishedProducts(CategoryID);


                //pnlAllProductTopLevel.Visible = true;

                //if (productList.Count == 1 && subCategoryList.Count == 0 && UserCookieManager.StoreMode != (int)StoreMode.Corp)
                //{

                //}
                //else if (productList.Count >= 1)
                //{
                //    productList = productList.OrderBy(g => g.SortOrder);
                //    ViewData["Products"] = productList;
                //}
            }
            else
            {

            }

            return View("PartialViews/Category",Category);
        }

        private void SetPageMEtaTitle(string CatName, string CatDes, string Keywords, string Title)
        {

            Address DefaultAddress = _myCompanyService.GetDefaultAddressByStoreID(UserCookieManager.StoreId);
            MyCompanyDomainBaseResponse baseResponse = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCompany();
            string[] MetaTags  = _myCompanyService.CreatePageMetaTags(Title, CatDes, Keywords, StoreMode.Retail, baseResponse.Company.Name, DefaultAddress);

            ViewBag.MetaTitle = MetaTags[0];
            ViewBag.MetaKeywords = MetaTags[1];
            ViewBag.MetaDescription = MetaTags[2];
        }
        private void BindCategoryData(List<ProductCategory> productCatList)
        {
            if (productCatList != null)
            {
                if (productCatList.Count > 0)
                {
                    //pnlCatList.Visible = true;
                    //lblCategoryHeader.Visible = true;
                    productCatList = productCatList.OrderBy(c => c.DisplayOrder).ToList();
                   
                }
                else
                {
                  //  pnlCatList.Visible = false;
                }
            }
            ViewData["ProductCategory"] = productCatList;
        }


    }
}