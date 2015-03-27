using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Supplier Api Controller
    /// </summary>
    public class SupplierController : ApiController
    {
        #region Private

        private readonly IInventoryService inventoryService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SupplierController(IInventoryService inventoryService)
        {
            this.inventoryService = inventoryService;
        }

        #endregion
        /// <summary>
        /// Add/Update Company
        /// </summary>
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewInventory })]
        public SupplierForInventory Post(Company company)
        {
            if (company == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return inventoryService.SaveSupplier(company.CreateFromSupplier()).CreateFromForInventory();
        }
    }
}