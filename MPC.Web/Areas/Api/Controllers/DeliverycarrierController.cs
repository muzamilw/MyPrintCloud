using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using MPC.MIS.Areas.Api.ModelMappers;
using System.Net;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class DeliverycarrierController : ApiController
    {
        // GET: Api/Deliverycarrier
        #region Private
        private readonly IDeliveryCarriersService _deliveryCarrierService;
        #endregion


        #region Constructor

        /// <summary>
        /// Constructor Prefix Controller
        /// </summary>
        /// <param name="prefixService"></param>
        public DeliverycarrierController(IDeliveryCarriersService deliveryCarrierService)
        {
            this._deliveryCarrierService = deliveryCarrierService;
        }
        #endregion

        #region Public
        public IEnumerable<DeliveryCarrier> Get()
        {

            IEnumerable<DeliveryCarrier> objenumdelivery = _deliveryCarrierService.GetAll().Select(g=>g.CreateFrom());
            return objenumdelivery;
        }
        public bool Post(DeliveryCarrier deliveryc)
        {

            if (deliveryc == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return _deliveryCarrierService.Add(deliveryc.CreateFrom());
        
        }

        #endregion
    }


    }
