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
    public class ProgressEstimateController : ApiController
    {
         #region Private

        private readonly IOrderService orderService;

        #endregion
        #region Constructor
        public ProgressEstimateController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        #endregion
        #region Public
        /// <summary>
        /// Update Estimate and Order Fields On Progress 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilter]
        public bool Get([FromUri] ProgressEstimateRequestModel request)
        {
           return orderService.ProgressEstimateToOrder(request);
        }

        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrder })]
        [CompressFilterAttribute]
        public Estimate Post(Estimate request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return orderService.CloneOrder(request.CreateFrom()).CreateFrom();
        }
        #endregion
    }
}