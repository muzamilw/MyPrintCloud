using System;
using System.Collections.Generic;
using System.Web.Http;
using MPC.Interfaces.IServices;
using MPC.Models.ResponseModels;
using MPC.WebApi.Models;

namespace MPC.WebApi.Areas.Api.Controllers
{
    /// <summary>
    /// Get Available Services
    /// </summary>
    public class GetAvailableServicesController : ApiController
    {
        #region Private
        private readonly IWebApiAvailableRentalService availableRentalService;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public GetAvailableServicesController(IWebApiAvailableRentalService availableRentalService)
        {
            if (availableRentalService == null)
            {
                throw new ArgumentNullException("availableRentalService");
            }
            this.availableRentalService = availableRentalService;
        }
        #endregion

        #region Public
        /// <summary>
        /// Get Available Services with their price
        /// </summary>        
        public IEnumerable<WebApiAvailableServices> Post(GetAvailableServicesRequest request)
        {
            var returnList = availableRentalService.GetAvailableServicesWithRates(request.OutLocationId, request.StartDateTime,
                request.EndDateTime, request.DomainKey, request.HireGroupDetailId);
            return returnList;
        }
        #endregion
    }
}