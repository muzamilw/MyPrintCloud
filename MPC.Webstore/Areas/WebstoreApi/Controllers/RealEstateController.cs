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
        public HttpResponseMessage SaveListing(string obj)
        {
            ListingProperty listingProperty = JsonConvert.DeserializeObject<ListingProperty>(obj);
            var result = listingService.UpdateListingData(listingProperty);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, result, formatter);
        }

    
    }
}
