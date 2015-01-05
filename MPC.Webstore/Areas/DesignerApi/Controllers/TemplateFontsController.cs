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
        public HttpResponseMessage GetFontList(int parameter1, int parameter2)
        {
            var template = templateFontSvc.GetFontList(parameter1,parameter2);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, template, formatter);

        }
        #endregion
    }
}
