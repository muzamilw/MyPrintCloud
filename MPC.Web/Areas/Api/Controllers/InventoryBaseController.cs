using System;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.ModelMappers;
using MPC.MIS.ResponseModels;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Inventory Base API Controller
    /// </summary>
    public class InventoryBaseController : ApiController
    {
        #region Private

        private readonly IInventoryService inventoryService;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public InventoryBaseController(IInventoryService inventoryService)
        {
            if (inventoryService == null)
            {
                throw new ArgumentNullException("inventoryService");
            }
            this.inventoryService = inventoryService;
        }

        #endregion

        #region Public
        /// <summary>
        /// Get Inventory Base Data
        /// </summary>
        public InventoryBaseResponse Get()
        {
           if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return inventoryService.GetBaseData().CreateFrom();
        }
        #endregion
    }
}