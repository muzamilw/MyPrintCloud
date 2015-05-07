using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using System.Web.Http;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class ItemDetailBaseController : ApiController
    {
         #region Private
        private readonly IOrderService orderService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public ItemDetailBaseController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Base Data for Orders
        /// </summary>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrder })]
        [CompressFilterAttribute]
        public ItemDetailBaseResponse Get()
        {
            return orderService.GetBaseDataForItemDetails().CreateFrom();
        }

        #endregion
    }
}