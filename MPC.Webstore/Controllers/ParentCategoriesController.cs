using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Interfaces.WebStoreServices;
using MPC.Webstore.Common;
using MPC.Models.Common;
using MPC.Models.DomainModels;

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
            //List<ProductCategory> lstParentCategories = new List<ProductCategory>();

            //if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
            //{
               
                  
            //        if (SessionParameters.CustomerContact != null && SessionParameters.CustomerContact.ContactRoleID == Convert.ToInt32(Roles.Adminstrator))
            //        {
            //            lstParentCategories = CategoriesManager.GetAllParentCorporateCatalog(SessionParameters.CustomerID);
            //        }
            //        else
            //        {
            //            lstParentCategories = CategoriesManager.GetAllParentCorporateCatalogByTerritory(SessionParameters.CustomerID, SessionParameters.ContactID);
            //        }
               

            //}
            //else
            //{
            //    lstParentCategories = oMngr.GetParentCategories();
             
            //    rptTopLevelCategory.DataSource = lstParentCategories.OrderBy(i => i.DisplayOrder);
            //    //rptTopLevelCategory.DataBind();
            //}
            //var model = _myCompanyService.GetCompanyParentCategoriesById(UserCookieManager.StoreId);
            return PartialView("PartialViews/ParentCategories");
        }
    }
}