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
    public class TemplateController : ApiController
    {
        #region Private

        private readonly ITemplateService templateService;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public TemplateController(ITemplateService templateService)
        {
            this.templateService = templateService;
        }

        #endregion
        #region public

        /// <summary>
        ///  called from the designer, all the values converted to pixel before sending
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage GetTemplate(int id)
        {
            var template = templateService.GetTemplateInDesigner(id);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, template, formatter);
         
        }


        //    public string update(Stream data)
        // public string preview(Stream data)
        // public string savecontine(Stream data)
        // public Stream GetFoldLine(string TemplateID)
        //public Stream GetCategoryV2(string CategoryIDStr)
        //public Stream GetProductV2(string TemplateID,string CategoryIDStr,string heightStr, string widthStr)
        //public Stream GetCatListV2(string CategoryIDStr, string pageNoStr, string pageSizeStr)
        #endregion
    }
}
