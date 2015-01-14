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
        public HttpResponseMessage GetFontsList(long parameter1, long parameter2)
        {
            var result = templateFontSvc.GetFontList(parameter1,parameter2);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, result, formatter);

        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage testSpFunction(long parameter1, long parameter2)
        {
             templateFontSvc.DeleteTemplateFonts(parameter1, parameter2);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, "", formatter);

        }
        #endregion
    }
}
