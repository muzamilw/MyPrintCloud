using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using MPC.WebBase.Mvc;


namespace MPC.MIS.Areas.Api.Controllers
{
    public class ProductChildCategoryController : ApiController
    {
        #region Private

        private readonly ICategoryService categoryService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="categoryService"></param>
        public ProductChildCategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        #endregion

        #region Public

        ///// <summary>
        ///// Get Product Categories for Company Including Archiving also
        ///// </summary>
        //[ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore, SecurityAccessRight.CanViewProduct })]
        //[CompressFilterAttribute]
        //public IEnumerable<ProductCategoryDropDown> Get(int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
        //    }

        //    long? companyId = id > 0 ? id : (long?)null;
        //    return itemService.GetProductCategoriesIncludingArchived(companyId).CreateFrom();
        //}
        /// <summary>
        /// Get Produst category Childs
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ApiException]
        [CompressFilterAttribute]
        public ProductCategoryResultModel Get(int id)
        {
            if (id <= 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            var categories = categoryService.GetChildCategoriesIncludingArchive(id).Select(x => x.ListViewModelCreateFrom()).ToList();
            return new ProductCategoryResultModel
            {
                ProductCategories = categories,
                TotalCount = categories.Count()
            };
        }

        #endregion
    }
}