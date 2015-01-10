using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
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
    public class TemplatePageController : ApiController
    {
       #region Private

        private readonly ITemplatePageService templatePageService;

       #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public TemplatePageController(ITemplatePageService templatePageService)
        {
            this.templatePageService = templatePageService;
        }

        #endregion
        #region public

        /// <summary>
        ///  called from the designer, all the values converted to pixel before sending
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage GetTemplatePages(long id)
        {
            var template = templatePageService.GetTemplatePages(id);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, template, formatter);
         
        }
        public HttpResponseMessage GetTemplatePagesSP(long id)
        {
            var template = templatePageService.GetTemplatePages(id);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, template, formatter);

        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        // Important: if called from MIS call implementation function instead of this function because organizationID will not exist in cookie when called from MIS
        public string testTemplate(long id)
        {
            List<TemplatePage> objList = new List<TemplatePage>();
            TemplatePage objpage = new TemplatePage();
            TemplatePage objpag2e = new TemplatePage();
            objList.Add(objpage); objList.Add(objpag2e);
            
            bool result = templatePageService.CreateBlankBackgroundPDFsByPages(1078,100,100,1,objList,0);
            string res = templatePageService.CreatePageBlankBackgroundPDFs(1079, objpage, 100, 100, 0);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return res.ToString();

        }
        #endregion
    }
}
