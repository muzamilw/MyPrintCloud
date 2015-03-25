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
    /// Item Clone API Controller
    /// </summary>
    public class ItemCloneController : ApiController
    {
        #region Private

        private readonly IItemService itemService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemCloneController(IItemService itemService)
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
        /// Post
        /// </summary>
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewProduct })]
        public Item Post(Item request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return itemService.CloneProduct(request.ItemId).CreateFrom();
        }

        #endregion
    }
}