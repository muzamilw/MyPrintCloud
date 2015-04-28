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
using MPC.WebBase.Mvc;

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
        [CompressFilter]
        public IEnumerable<DeliveryCarrier> Get()
        {

            IEnumerable<DeliveryCarrier> objenumdelivery = _deliveryCarrierService.GetAll().Select(g=>g.CreateFrom());
            return objenumdelivery;
        }
        public bool Post(DeliveryCarrier deliveryc)
        {
            bool result = true; 

            if (deliveryc == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            if(deliveryc.CarrierId > 0 )
            { 
                result = _deliveryCarrierService.Update(deliveryc.CreateFrom());
            }
            else
            {
                if(deliveryc.isEnable == null)
                {
                    deliveryc.isEnable = false;
                }
                result = _deliveryCarrierService.Add(deliveryc.CreateFrom());
            }

            return result;
        
        }

        #endregion
    }


    }
