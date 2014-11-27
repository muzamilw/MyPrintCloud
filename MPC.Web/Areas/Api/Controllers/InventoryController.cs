using System;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.ModelMappers;
using MPC.MIS.ResponseModels;
using MPC.Models.RequestModels;

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

        #endregion
    }
}