using System;
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
    public class CopyOrderController : ApiController
    { 
        #region Private

        private readonly IOrderService orderService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CopyOrderController(IOrderService orderService)
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
        /// Get Estimate Id Make its Deep Copy and Return that Copy
        /// </summary>
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrder })]
        [CompressFilterAttribute]
        public Estimate Get(int id)
        {
            if (id <= 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return orderService.CloneOrder(id).CreateFrom();
        }
        #endregion
    }
}