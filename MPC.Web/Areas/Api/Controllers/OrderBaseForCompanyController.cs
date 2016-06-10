using System;
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
    /// Order Base API Controller
    /// </summary>
    public class OrderBaseForCompanyController : ApiController
    {
        #region Private

        private readonly IOrderService orderService;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public OrderBaseForCompanyController(IOrderService orderService)
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
        /// Get Company Base Data
        /// </summary>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrder })]
        [CompressFilterAttribute]
        public OrderBaseResponseForCompany Get([FromUri]int id,long storeId, long orderId = 0)
        {
            if (id <= 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            return orderService.GetBaseDataForCompany(id, storeId, orderId).CreateFrom();
        }

        #endregion
    }
}