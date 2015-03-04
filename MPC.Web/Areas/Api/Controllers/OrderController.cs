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
    /// Order API Controller
    /// </summary>
    public class OrderController : ApiController
    {
        #region Private

        private readonly IOrderService orderService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public OrderController(IOrderService orderService)
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
        /// Get Order By Id
        /// </summary>
        [ApiException]
        public Estimate Get(int id)
        {
            if (id <= 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return orderService.GetById(id).CreateFrom();
        }
        
        /// <summary>
        /// Get All Orders
        /// </summary>
        public GetOrdersResponse Get([FromUri] GetOrdersRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return orderService.GetAll(request).CreateFrom();
        }

        /// <summary>
        /// Post
        /// </summary>
        [ApiException]
        public Estimate Post(Estimate request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return orderService.SaveOrder(request.CreateFrom()).CreateFrom();
        }

        /// <summary>
        /// Delete
        /// </summary>
        [ApiException]
        public void Delete(OrderDeleteRequest request)
        {
            if (request == null || !ModelState.IsValid || request.OrderId <= 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            orderService.DeleteOrder(request.OrderId);
        }

        #endregion
    }
}