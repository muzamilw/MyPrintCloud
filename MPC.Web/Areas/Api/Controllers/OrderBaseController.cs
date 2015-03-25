using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Order Base Data Controller 
    /// </summary>
    public class OrderBaseController : ApiController
    {
        #region Private
        private IOrderService orderService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public OrderBaseController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Base Data for Orders
        /// </summary>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrder })]
        public OrderBaseResponse Get()
        {
            return orderService.GetBaseData().CreateFrom();
        }

        #endregion
    }
}