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
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public RealEstateController(IListingService listingService, IItemService itemService, ICompanyService myCompanyService)
        {
            this.listingService = listingService;
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
    }
}
