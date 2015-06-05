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
    public class InquiryProgressController : ApiController
    {
        #region Private

        private readonly IInquiryService inquiryService;
        private readonly IOrderService orderService;

        #endregion

        #region Constructor
        public InquiryProgressController(IInquiryService inquiryService, IOrderService orderService)
        {
            this.inquiryService = inquiryService;
            this.orderService = orderService;
        }

        #endregion

        #region Public
        /// <summary>
        /// Get Inquiry By Id
        /// </summary>
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrder })]
        [CompressFilterAttribute]
        public Estimate Get([FromUri] ProgressInquiryToEstimateParams request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }
            //Update Inquiry Status
            inquiryService.ProgressInquiryToEstimate(request.InquiryId);
            //Create Estimate

            Estimate estimate = new Estimate { CompanyId = request.CompanyId, ContactId = request.ContactId, EnquiryId = request.InquiryId, IsEstimate = true, StatusId = 1, EstimateName = request.Title };
            return orderService.SaveOrder(estimate.CreateFrom()).CreateFrom();
        }
        #endregion
    }
}