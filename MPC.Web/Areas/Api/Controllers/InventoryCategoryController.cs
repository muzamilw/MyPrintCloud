using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.ExceptionHandling;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class InventoryCategoryController : ApiController
    {
        #region Private

        private readonly IStockCategoryService stockCategoryService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="stockCategoryService"></param>
        public InventoryCategoryController(IStockCategoryService stockCategoryService)
        {
            this.stockCategoryService = stockCategoryService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get All Stock Categories
        /// </summary>
        /// <returns></returns>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewInventoryCategory })]
        [CompressFilterAttribute]
        public StockCategoryResponse Get([FromUri] StockCategoryRequestModel request)
        {
            
            var result = stockCategoryService.GetAll(request);
            return new StockCategoryResponse
            {
                StockCategories = result.StockCategories.Select(x => x.CreateFrom()).ToList(),
                RowCount = result.RowCount
            };
        }

        /// <summary>
        /// Update stock Category
        /// </summary>
        /// <param name="stockCategory"></param>
        /// <returns></returns>
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewInventoryCategory })]
        [CompressFilterAttribute]
        public StockCategory Post(StockCategory stockCategory)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if(stockCategory.CategoryId <= 0)
                    {
                        return stockCategoryService.Add(stockCategory.CreateFrom()).CreateFrom();
                    }
                    else 
                    {
                        return stockCategoryService.Update(stockCategory.CreateFrom()).CreateFrom();
                    }
                    
                }
                catch (Exception exception)
                {
                    throw new MPCException(exception.Message, 0); 
                }
                
            }
            throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
        }

        /// <summary>
        /// Delete Stock Category
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewInventoryCategory })]
        [CompressFilterAttribute]
        public bool Delete(StockCategoryRequestModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return stockCategoryService.Delete(model.StockCategoryId);
                }
                catch (Exception exception)
                {
                    throw new MPCException(exception.Message, 0);
                }
                
            }
            throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
        }

        #endregion

    }
}