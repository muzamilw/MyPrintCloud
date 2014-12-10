using System;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Supplier Base Api Controller
    /// </summary>
    public class SupplierBaseController : ApiController
    {
        #region Private

        private readonly IInventoryService inventoryService;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SupplierBaseController(IInventoryService inventoryService)
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
        public SupplierBaseResponse Get()
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return inventoryService.GetSupplierBaseData().CreateFrom();
        }
        #endregion
    }
}