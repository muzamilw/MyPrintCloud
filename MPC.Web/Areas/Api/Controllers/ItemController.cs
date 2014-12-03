using System;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Item API Controller
    /// </summary>
    public class ItemController : ApiController
    {
        #region Private

        private readonly IItemService inventoryService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemController(IItemService inventoryService)
        {
            if (inventoryService == null)
            {
                throw new ArgumentNullException("inventoryService");
            }
            this.inventoryService = inventoryService;
        }

        #endregion

        #region Public

        /// <summary>
        /// Get Item By Id
        /// </summary>
        public Item Get(int id)
        {
            if (id <= 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return inventoryService.GetById(id).CreateFrom();
        }
        
        /// <summary>
        /// Get All Items
        /// </summary>
        public ItemSearchResponse Get([FromUri] ItemSearchRequestModel request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return inventoryService.GetItems(request).CreateFrom();
        }

        /// <summary>
        /// Post
        /// </summary>
        public Item Post(Item request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return request;
            //return inventoryService.SaveItem(request).CreateFrom();
        }

        /// <summary>
        /// Delete
        /// </summary>
        public void Delete(ItemDeleteRequest request)
        {
            if (request == null || !ModelState.IsValid || request.ItemId <= 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            //return inventoryService.DeleteItem(request.ItemId);
        }

        #endregion
    }
}