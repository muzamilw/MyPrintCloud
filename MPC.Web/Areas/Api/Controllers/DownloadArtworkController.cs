using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class DownloadArtworkController : ApiController
    {
        #region Private

        private readonly IOrderService orderService;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DownloadArtworkController(IOrderService orderService)
        {
            if (orderService == null)
            {
                throw new ArgumentNullException("orderService");
            }
            this.orderService = orderService;
        }

        #endregion

        #region Public
       // [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrder })]
        [CompressFilterAttribute]
        public string Get(int OrderId, long OrganisationId)
        {
            return orderService.DownloadOrderArtwork(OrderId, string.Empty, OrganisationId);
        }
        #endregion
    }
}
