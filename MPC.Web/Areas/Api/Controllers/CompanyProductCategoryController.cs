using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Company Product Category API Controller
    /// </summary>
    public class CompanyProductCategoryController : ApiController
    {
        #region Private

        private readonly IItemService itemService;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyProductCategoryController(IItemService itemService)
        {
            if (itemService == null)
            {
                throw new ArgumentNullException("itemService");
            }

            this.itemService = itemService;
        }

        #endregion

        #region Public

        /// <summary>
        /// Get Product Categories for Company
        /// </summary>
        public IEnumerable<ProductCategoryDropDown> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            long? companyId = id > 0 ? id : (long?) null;
            return itemService.GetProductCategoriesForCompany(companyId).CreateFrom();
        }

        #endregion
    }
}