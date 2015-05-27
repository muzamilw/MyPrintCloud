using System;
using System.Collections.Generic;
using System.Linq;
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
    public class InquiryItemController : ApiController
    {
        #region Private

        private readonly IInquiryService inquiryService;
        #endregion
        #region Constructor
        public InquiryItemController(IInquiryService inquiryService)
        {
            this.inquiryService = inquiryService;
        }
        #endregion
        #region Public
        /// <summary>
        /// Get Inquiry Items By Id
        /// </summary>
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrder })]
        [CompressFilterAttribute]
        public InquiryItemsResponse Get(int id)
        {
            return new InquiryItemsResponse
            {
                InquiryItems = inquiryService.GetInquiryItems(id).Select(x => x.CreateFrom()),
            };
        }
        #endregion
    }
}