using System;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Item Base API Controller
    /// </summary>
    public class ItemBaseController : ApiController
    {
        #region Private

        private readonly IItemService itemService;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ItemBaseController(IItemService itemService)
        {
            if (itemService == null)
            {
                throw new ArgumentNullException("itemService");
            }

            this.itemService = itemService;
        }

        #endregion

        #region Public
        /// <summary>
        /// Get Item Base Data
        /// </summary>
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewProduct })]
        [CompressFilterAttribute]
        public Models.ItemBaseResponse Get()
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            return itemService.GetBaseData().CreateFrom();
        }

        #endregion
    }
}