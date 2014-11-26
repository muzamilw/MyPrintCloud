using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.ModelMappers;
using MPC.MIS.Models;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using Models = MPC.MIS.Models;
using DomainModels = MPC.Models.DomainModels;

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
        public ResponseModels.StockCategoryResponse Get([FromUri] StockCategoryRequestModel request)
        {
            IEnumerable<Models.StockCategory> stockCategories= null;
            stockCategories = stockCategoryService.GetAll(request).Select(x=>x.CreateFrom()).ToList();
            var categories = stockCategories as IList<StockCategory> ?? stockCategories.ToList();
            return new ResponseModels.StockCategoryResponse
            {
                StockCategories = categories,
                RowCount = categories.Count()
            };
        }

        /// <summary>
        /// Update Stock Categories
        /// </summary>
        /// <param name="stockCategory"></param>
        /// <returns></returns>
        public StockCategory Put(StockCategory stockCategory)
        {
            if (ModelState.IsValid)
            {
                return stockCategoryService.Add(stockCategory.CreateFrom()).CreateFrom();
            }
            throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
        }

        /// <summary>
        /// Create New stock Category
        /// </summary>
        /// <param name="stockCategory"></param>
        /// <returns></returns>
        public StockCategory Post(StockCategory stockCategory)
        {
            if (ModelState.IsValid)
            {
                return stockCategoryService.Update(stockCategory.CreateFrom()).CreateFrom();
            }
            throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
        }

        /// <summary>
        /// Delete Stock Category
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(StockCategoryRequestModel model)
        {
            if (ModelState.IsValid)
            {
                return stockCategoryService.Delete(model.StockCategoryId);
            }
            throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
        }

        #endregion

    }
}