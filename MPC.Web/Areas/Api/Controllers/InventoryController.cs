using System;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Inventory API Controller
    /// </summary>
    public class InventoryController : ApiController
    {
        #region Private

        private readonly IInventoryService inventoryService;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public InventoryController(IInventoryService inventoryService)
        {
            if (inventoryService == null)
            {
                throw new ArgumentNullException("inventoryService");
            }
            this.inventoryService = inventoryService;
        }
        #endregion

        #region Public
        // GET api/<controller>
        public InventorySearchResponse Get([FromUri] InventorySearchRequestModel request)
        {
            return inventoryService.LoadStockItems((request)).CreateFrom();
        }


        /// <summary>
        /// Get Stock Item By Id
        /// </summary>
        public StockItem Get(int id)
        {
            if (id <= 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return inventoryService.GetById(id).CreateFromDetail();
        }
        /// <summary>
        /// Add/Update a Inventory
        /// </summary>
        [ApiException]
        public StockItemForListView Post(StockItem stockItem)
        {
            if (stockItem == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return inventoryService.SaveInevntory(stockItem.CreateFrom()).CreateFrom();
        }

        /// <summary>
        /// Delete Stock Item
        /// </summary>
        [ApiException]
        public void Delete(StockItem stockItem)
        {
            if (stockItem == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            inventoryService.DeleteInvenotry(stockItem.StockItemId);
        }
        #endregion
    }
}