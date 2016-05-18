using MPC.Interfaces.WebStoreServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace MPC.Webstore.Areas.DesignerApi.Controllers
{
    public class DamImageController : ApiController
    {
        #region Private

        private readonly ICompanyService companyService;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public DamImageController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        #endregion

        #region public

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        // parameter 1 = search text ID , parameter 2 = companyid, 3 = organizationid, 4 = territoryid, 5 = parentfolderid
        public HttpResponseMessage GetDAMImages(string parameter1, long parameter2, long parameter3, long parameter4, int parameter5)
        {
            if ( parameter1 == "0")
            {
                parameter1 = "";
            }

            var result = companyService.GetDamFoldersAssets(parameter1, parameter2, parameter3, parameter4, parameter5);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, result, formatter);

        }
        #endregion
    }
}


