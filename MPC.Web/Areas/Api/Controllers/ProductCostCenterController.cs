using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.WebBase.Mvc;
using MPC.Models.RequestModels;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Activity Detail API Controller
    /// </summary>
    public class ProductCostCenterController : ApiController
    {
        #region Private

        private readonly ICostCentersService costCentersService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ProductCostCenterController(ICostCentersService costCentersService)
        {
            this.costCentersService = costCentersService;
        }

        #endregion

        #region Public


        /// <summary>
        /// Get Activity Detail By ID
        /// </summary>
        [CompressFilterAttribute]
        public CostCentreResponseModel Get([FromUri]GetCostCentresRequest requestModel)
        {
            return  costCentersService.GetAllForOrderProduct(requestModel).CreateFromForProducts();
        }

        #endregion
    }
}