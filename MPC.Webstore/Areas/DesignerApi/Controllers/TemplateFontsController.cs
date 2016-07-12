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
    public class TemplateFontsController : ApiController
    {
       #region Private
       private readonly ITemplateFontsService templateFontSvc;
       #endregion
       #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public TemplateFontsController(ITemplateFontsService templateFontSvc)
        {
            this.templateFontSvc = templateFontSvc;
        }

        #endregion
        #region public

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetFontsList(long parameter1, long parameter2, long parameter3, long parameter4, long parameter5)
        {

            
            // parameter3 = organization id 
            var result = templateFontSvc.GetFontList(parameter1, parameter2, parameter3, parameter4, parameter5);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, result, formatter);

        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        //customerID,organisationId,fontName,font display name;
        public HttpResponseMessage uploadFontRecord(long parameter1, long parameter2,string parameter3,string parameter4)
        {
            templateFontSvc.InsertFontFile(parameter1, parameter2, parameter3, parameter4);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, "", formatter);

        }

        #endregion
    }
}
