using MPC.Interfaces.MISServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class ListingPropertyController : ApiController
    {
         #region Private

        private readonly ICompanyService companyService;
        private readonly IListingService listingService;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ListingPropertyController(ICompanyService companyService, IListingService listingService)
        {
            if (companyService == null)
            {
                throw new ArgumentNullException("companyService");
            }
            this.companyService = companyService;
            this.listingService = listingService;
        }

        #endregion

        [HttpGet]
        public string SaveListingData(long OrganisationId)
        {
            return listingService.SaveListingData(OrganisationId);
        }
    }
}