using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.Models.RequestModels;
using MPC.WebBase.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class DeleteOrderController : ApiController
    {
        #region Private

        private readonly IOrderService orderService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DeleteOrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        #endregion
        
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [ApiException]
        [CompressFilterAttribute]
        public void Delete(DeleteOrderRequest model)
        {
            if (model.OrderId == 0 || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            orderService.DeleteOrderPermanently(model.OrderId,model.Comment);
        }
    }
}