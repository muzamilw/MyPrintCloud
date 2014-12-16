﻿using System;
using System.Net;
using System.Web;
using System.Web.Http;
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
        public Item Get(int id)
        {
            if (id <= 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return itemService.GetById(id).CreateFrom();
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

            return itemService.GetItems(request).CreateFrom();
        }

        /// <summary>
        /// Post
        /// </summary>
        [ApiException]
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