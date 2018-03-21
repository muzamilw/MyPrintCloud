using MPC.Interfaces.WebStoreServices;
using MPC.Models.ResponseModels;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace MPC.Webstore.Areas.WebstoreApi.Controllers
{
    public class DamImageAPIController : ApiController
    {
        private readonly ICompanyService companyService;

        public DamImageAPIController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetAssets(string keywords, long storeId, long orgId, int parentFolderId)
        {
            if (keywords == null ||keywords == "0")
            {
                keywords = "";
            }

            if ( orgId == 1651 && parentFolderId == 0 )  //hard coding the root to 1806 aka ICE upload folder
            {
                parentFolderId = 1806;
            }

            FolderSearchResponse damFoldersAssets = this.companyService.GetDamFoldersAssets(keywords, storeId, orgId, 0, parentFolderId);
            JsonMediaTypeFormatter jsonMediaTypeFormatter = new JsonMediaTypeFormatter();
            JsonSerializerSettings serializerSettings = jsonMediaTypeFormatter.SerializerSettings;

            serializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            serializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            return Request.CreateResponse(HttpStatusCode.OK, damFoldersAssets, jsonMediaTypeFormatter);


            //return HttpRequestMessageExtensions.CreateResponse<FolderSearchResponse>(base.get_Request(), HttpStatusCode.OK, damFoldersAssets, jsonMediaTypeFormatter);
        }
    }
}