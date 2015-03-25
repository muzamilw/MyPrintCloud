using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Order Controller 
    /// </summary>
    public class OrdersForCrmController : ApiController
    {
       #region Private

        private readonly IOrderForCrmService orderForCrmService;
        #endregion
       #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public OrdersForCrmController(IOrderForCrmService orderForCrmService)
        {
            this.orderForCrmService = orderForCrmService;
        }

        #endregion
       #region Public

        /// <summary>
        /// Get Addresses / Compnay Contacts
        /// </summary>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewCrm })]
        public OrdersForCrmResponse Get([FromUri] GetOrdersRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
           return orderForCrmService.GetOrdersForCrm(request).CreateFrom();
        }
        #endregion
    }
} 