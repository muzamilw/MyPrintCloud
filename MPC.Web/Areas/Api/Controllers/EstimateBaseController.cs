using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class EstimateBaseController : ApiController
    {
        #region Private
        private readonly IOrderService orderService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public EstimateBaseController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Base Data for Estimate
        /// </summary>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrder })]
        [CompressFilterAttribute]
        public OrderBaseResponse Get()
        {
            return orderService.GetBaseDataForEstimate().CreateFrom();
        }

        #endregion
    }
}