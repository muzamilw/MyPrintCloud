using MPC.Interfaces.WebStoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MPC.Webstore.Areas.WebstoreApi.Controllers
{
    public class OrganisationCacheController : ApiController
    {
         #region Private

        private readonly ICompanyService myCompanyService;
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="myCompanyService"></param>
        public OrganisationCacheController(ICompanyService myCompanyService)
        {
            this.myCompanyService = myCompanyService;
        }

        #endregion
        
        public HttpResponseMessage Get(int id)
        {
            myCompanyService.GetStoreFromCache(id, true);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    
    }
}
