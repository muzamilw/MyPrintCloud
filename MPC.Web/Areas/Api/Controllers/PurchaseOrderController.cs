﻿using System.Net;
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
    /// Purchase Order API Controller
    /// </summary>
    public class PurchaseOrderController : ApiController
    {
        #region Private

        private readonly IPurchaseService purchaseService;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public PurchaseOrderController(IPurchaseService purchaseService)
        {
            this.purchaseService = purchaseService;
        }

        #endregion

        #region Public

        /// <summary>
        /// 
        /// </summary>
        public PurchaseResponseModel Get([FromUri] PurchaseOrderSearchRequestModel request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return purchaseService.GetPurchaseOrders(request).CreateFrom();
        }

        [ApiException]
        public PurchaseListView Post(Purchase purchase)
        {
            if (purchase == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            // return _deliveryNotesService.SaveDeliveryNote(deliveryNote.CreateFrom()).CreateFromListView();
            return null;
        }
        #endregion
    }
}