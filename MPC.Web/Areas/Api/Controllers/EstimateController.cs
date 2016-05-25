using System;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class EstimateController : ApiController
    {
       #region Private

        private readonly IOrderService orderService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public EstimateController(IOrderService orderService)
        {
            if (orderService == null)
            {
                throw new ArgumentNullException("orderService");
            }
            this.orderService = orderService;
        }

        #endregion

        #region Public
        /// <summary>
        /// Get All Orders
        /// </summary>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewEstimating })]
        [CompressFilterAttribute]
        public GetOrdersResponse Get([FromUri] GetOrdersRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return orderService.GetOrdersForEstimates(request).CreateFrom();
        }
        #endregion
    }
}