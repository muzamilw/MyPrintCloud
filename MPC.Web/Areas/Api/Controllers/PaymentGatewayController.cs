using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.Models.RequestModels;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class PaymentGatewayController : ApiController
    {
        #region Private

        private readonly ICompanyService companyService;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public PaymentGatewayController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        #endregion
        /// <summary>
        /// Get Payment Gateways
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Models.PaymentGatewayResponse Get([FromUri] PaymentGatewayRequestModel request)
        {
            var result = companyService.SearchPaymentGateways(request);
            return new Models.PaymentGatewayResponse
            {
                PaymentGateways = result.PaymentGateways.Select(x => x.CreateFrom()),
                RowCount = result.RowCount
            };
        }
	}
}