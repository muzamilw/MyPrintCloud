using System;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Supplier Api Controller For Inventory
    /// </summary>
    public class SupplierForInventoryController : ApiController
    {
        #region Private

        private readonly IInventoryService inventoryService;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SupplierForInventoryController(IInventoryService inventoryService)
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
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewInventory })]
        [CompressFilterAttribute]
        public SupplierSearchResponseForInventory Get([FromUri] SupplierRequestModelForInventory request)
        {
            return inventoryService.LoadSuppliers((request)).CreateFrom();
        }
        #endregion
    }
}