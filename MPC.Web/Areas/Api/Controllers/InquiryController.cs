using System;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.WebBase.Mvc;
using Inquiry = MPC.MIS.Areas.Api.Models.Inquiry;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class InquiryController : ApiController
    {
        #region Private

        private readonly IInquiryService inquiryService;

        #endregion

        #region Constructor
        public InquiryController(IInquiryService inquiryService)
        {
            this.inquiryService = inquiryService;
        }
        #endregion

        #region Public
        /// <summary>
        /// Get Inquiry By Id
        /// </summary>
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrder })]
        [CompressFilterAttribute]
        public Inquiry Get(int id)
        {
            if (id <= 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return inquiryService.GetInquiryById(id).CreateFrom();
            return null;
        }
        /// <summary>
        /// Get All Inquiries
        /// </summary>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrder })]
        [CompressFilterAttribute]
        public GetInquiriesResponse Get([FromUri] GetInquiryRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return inquiryService.GetAll(request).CreateFrom();
        }


        /// <summary>
        /// Post
        /// </summary>
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrder })]
        [CompressFilterAttribute]
        public Inquiry Post(Inquiry request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }
            if (request.InquiryId == 0)
            {
                return inquiryService.Update(request.CreateFrom()).CreateFrom();
            }
            return inquiryService.Update(request.CreateFrom()).CreateFrom();
        }
        #endregion
    }
}