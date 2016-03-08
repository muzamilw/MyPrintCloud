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
    public class TemplateColorStylesController : ApiController
    {
          #region Private

        private readonly ITemplateColorStylesService templateColorStylesService;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public TemplateColorStylesController(ITemplateColorStylesService templateColorStylesService)
        {
            this.templateColorStylesService = templateColorStylesService;
        }

        #endregion

        #region public

        // function moved here from templateSvcSP
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetColorStyleSP(long id)
        {
            //parameter1 = template id and parameter 2 = CustomerID
            var colors = templateColorStylesService.GetColorStyle(id);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, colors, formatter);
        }
        public HttpResponseMessage GetColorStyle(long parameter1, long parameter2, long parameter3)
        {
            //parameter1 = template id and parameter 2 = CustomerID and parameter3 = terriotryID
            var colors = templateColorStylesService.GetColorStyle(parameter1, parameter2, parameter3);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, colors, formatter);
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage SaveCorpColor(string parameter1, int parameter2, int parameter3, int parameter4, int parameter5, long parameter6)
        {
            //parameter1 = name and parameter 2 = color c , parameter 3 = color m, paramter 4 = color y, parameter 5 =  color k , parameter 6 = customerID
            var res = templateColorStylesService.SaveCorpColor(parameter2, parameter3, parameter4, parameter5, parameter1, parameter6);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, res, formatter);
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage UpdateCorpColor(long parameter1, string parameter2)
        {
            //parameter1 = PelleteID and parameter 2 = type
            string res = templateColorStylesService.UpdateCorpColor(parameter1, parameter2);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, res, formatter);
        }
        #endregion
    }
}
