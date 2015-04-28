using System;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.Models;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// API to Delete Item Permanently
    /// </summary>
    public class DeleteItemController : ApiController
    {
        #region Private

        private readonly IItemService itemService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DeleteItemController(IItemService itemService)
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
        /// Delete
        /// </summary>
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewProduct })]
        [CompressFilterAttribute]
        public void Delete(ItemDeleteRequest request)
        {
            if (request == null || !ModelState.IsValid || request.ItemId <= 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            itemService.DeleteProduct(request.ItemId);
        }

        #endregion
    }
}