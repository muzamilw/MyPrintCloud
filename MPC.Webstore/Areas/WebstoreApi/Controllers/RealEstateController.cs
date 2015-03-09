using MPC.Common;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace MPC.Webstore.Areas.WebstoreApi.Controllers
{
    public class RealEstateController : ApiController
    {
         #region Private

        private readonly IListingService listingService;
        private readonly MPC.Implementation.MISServices.CompanyService myCompanyService;
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public RealEstateController(IListingService listingService, IItemService itemService, ICompanyService myCompanyService, MPC.Implementation.MISServices.CompanyService CompanyService)
        {
            this.listingService = listingService;
            this.myCompanyService = CompanyService;
        }

        #endregion
        [HttpPost]
        public HttpResponseMessage SaveListing([FromBody]  ListingProperty obj)
        {
            var result = listingService.UpdateListingData(obj);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, result, formatter);
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpPost]
        public void InsertOrganisation(long parameter1, string parameter2,bool parameter3)
        {
            myCompanyService.ImportOrganisation(parameter1, parameter2, parameter3);
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpPost]
        public void ExportOrganisation(long parameter1)
        {
            myCompanyService.ExportOrganisation(parameter1);
        }
    }
}
