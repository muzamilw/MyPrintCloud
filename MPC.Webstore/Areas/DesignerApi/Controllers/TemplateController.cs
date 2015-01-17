using MPC.Interfaces.WebStoreServices;
using MPC.Webstore.Common;
using MPC.Webstore.ResponseModels;
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
        public HttpResponseMessage GetTemplate(long id)
        {
            var template = templateService.GetTemplateInDesigner(id);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, template, formatter);
         
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        // Important: if called from MIS call implementation function instead of this function because OrganisationID will not exist in cookie when called from MIS
        public string testTemplate(long id)
        {
            long OrganisationId = UserCookieManager.OrganisationID;
            //var result = templateService.generateTemplateFromPDF("F:\\Development\\Github\\MyPrintCloud-dev\\MPC.web\\MPC_Content\\Products\\Organisation1\\Templates\\random__CorporateTemplateUpload.pdf",2, id, 1);
            templateService.processTemplatePDF(id, 1, true, true, true);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return "true".ToString();

        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage mergeTemplate(int parameter1, long parameter2, long parameter3)
        {
            var template = templateService.MergeRetailTemplate(parameter1,parameter2,parameter3);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, template, formatter);

        }
 
        // public string preview(Stream data)
        //    public string update(Stream data)// not used in new designer 
        // public string savecontine(Stream data)// not used in new designer 
        // public Stream GetFoldLine(string TemplateID)  // not used in new designer 
        //public Stream GetCategoryV2(string CategoryIDStr)  // called from v2 service
        //public Stream GetProductV2(string TemplateID,string CategoryIDStr,string heightStr, string widthStr) // called from v2 service
        //public Stream GetCatListV2(string CategoryIDStr, string pageNoStr, string pageSizeStr) // called from v2 service
        #endregion
    }
}
