using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.WebBase.Mvc;
using MPC.Models.DomainModels;


namespace MPC.MIS.Areas.Api.Controllers
{
    public class OrderArtworkProductionReadyController : ApiController
    {
        #region Private

        private readonly IOrderService orderService;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public OrderArtworkProductionReadyController(IOrderService orderService)
        {
            if (orderService == null)
            {
                throw new ArgumentNullException("orderService");
            }
            this.orderService = orderService;
        }

        #endregion

        #region Public
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrder })]
        [CompressFilterAttribute]
        public bool Get(Estimate Order)
        {
            return orderService.MakeOrderArtworkProductionReady(Order);
        }
        #endregion
    }
}
