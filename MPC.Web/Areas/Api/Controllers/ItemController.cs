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
    /// <summary>
    /// Item API Controller
    /// </summary>
    public class ItemController : ApiController
    {
        #region Private

        private readonly IItemService itemService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemController(IItemService itemService)
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
        /// Get Item By Id
        /// </summary>
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewProduct })]
        [CompressFilterAttribute]
        public Item Get(int id)
        {
            if (id <= 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            Item objitem = itemService.GetById(id).CreateFrom();
            return objitem;
        }
        
        /// <summary>
        /// Get All Items
        /// </summary>
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewProduct })]
        [CompressFilterAttribute]
        public ItemSearchResponse Get([FromUri] ItemSearchRequestModel request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return itemService.GetItems(request).CreateFrom();
        }

        /// <summary>
        /// Post
        /// </summary>
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewProduct })]
        [CompressFilterAttribute]
        public Item Post(Item request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return itemService.SaveProduct(request.CreateFrom()).CreateFrom();
        }

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

            itemService.ArchiveProduct(request.ItemId);
        }

        #endregion
    }
}