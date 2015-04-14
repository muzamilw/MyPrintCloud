using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.Models;
using MPC.WebBase.Mvc;
using MPC.MIS.Areas.Api.ModelMappers;
using System.Web;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class BestPressController : ApiController
    {
        #region Private

        private readonly IOrderService orderService;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public BestPressController(IOrderService orderService)
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
        public BestPressResponse Get([FromUri] ItemSection section)
        {
            if (section == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return orderService.GetBestPresses(section.CreateFrom()).CreateFrom();
        }
        #endregion
    }
}
