using MPC.Interfaces.WebStoreServices;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MPC.Webstore.Areas.WebstoreApi.Controllers
{
    public class StoreCacheController : ApiController
    {
         #region Private

        private readonly ICompanyService myCompanyService;
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="myCompanyService"></param>
        public StoreCacheController(ICompanyService myCompanyService)
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
