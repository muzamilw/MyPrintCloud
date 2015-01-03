using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using Microsoft.Owin;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using MPC.WebBase.Mvc;
using PagedList;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class ProductCategoryController : ApiController
    {
        #region Private

        private readonly ICategoryService categoryService;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="categoryService"></param>
        public ProductCategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        #endregion
        #region Public
        /// <summary>
        /// Get Produst category Childs
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ApiException]
        public ProductCategoryResultModel Get(int id)
        {
            if (id <= 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            var categories= categoryService.GetChildCategories(id).Select(x=> x.CreateFrom()).ToList();
            return new ProductCategoryResultModel
            {
                ProductCategories = categories,
                TotalCount = categories.Count()
            };
        }

        [ApiException]
        public ProductCategory Get([FromUri]ProductCategoryRequestModel requestModel)
        {
            if (requestModel.IsProductCategoryEditting)
            {
                return categoryService.GetProductCategoryById(requestModel.ProductCategoryId).CreateFrom();
            }
            return null;
        }
        #endregion
    }
}