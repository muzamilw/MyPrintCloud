using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using System.Web.Http;
using MPC.WebBase.Mvc;


namespace MPC.MIS.Areas.Api.Controllers
{
    public class InquiryBaseController : ApiController
    {
       #region Private
        private IOrderService orderService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public InquiryBaseController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Base Data for Inquiries
        /// </summary>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrder })]
        [CompressFilterAttribute]
        public OrderBaseResponse Get()
        {
            return orderService.GetBaseDataForInquiries().CreateFrom();
        }

        #endregion
    }
}