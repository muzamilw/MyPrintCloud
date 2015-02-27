using MPC.Common;
using MPC.Interfaces.WebStoreServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace MPC.Webstore.Areas.DesignerApi.Controllers
{
    public class SmartFormController : ApiController
    {
       #region Private

        private readonly ISmartFormService smartFormService;
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public SmartFormController(ISmartFormService smartFormService)
        {
            this.smartFormService = smartFormService;
        }

        #endregion
        #region public
        public HttpResponseMessage GetVariablesList(bool parameter1, long parameter2,long parameter3)
        {
            var result = smartFormService.GetVariablesData(parameter1, parameter2, parameter3);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, result, formatter);

        }
        public HttpResponseMessage GetTemplateVariables(long id)
        {
            var result = smartFormService.GetTemplateVariables(id);

            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, result, formatter);

        }

        [HttpPost]
        public HttpResponseMessage SaveTemplateVariables([FromBody]  List<TemplateVariablesObj> obj)
        {
            
            var result = smartFormService.SaveTemplateVariables(obj);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, result, formatter);
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetUsersList(long id)
        {
            var result = smartFormService.GetUsersList(id);

            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, result, formatter);

        }
        #endregion
    }
}
