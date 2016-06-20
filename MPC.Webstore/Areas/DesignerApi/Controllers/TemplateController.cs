using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using MPC.Webstore.ResponseModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using MPC.Webstore.ModelMappers;

namespace MPC.Webstore.Areas.DesignerApi.Controllers
{
    public class TemplateController : ApiController
    {
        #region Private

        private readonly ITemplateService templateService;
        private readonly IItemService itemService;
        private readonly ICompanyService _myCompanyService;
        private readonly ITemplateVariableService _templateVariableService;
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public TemplateController(ITemplateService templateService, IItemService itemService, ICompanyService myCompanyService,ITemplateVariableService templateVariableService)
        {
            this.templateService = templateService;
            this._myCompanyService = myCompanyService;
            this.itemService = itemService;
            this._templateVariableService = templateVariableService;

        }

        #endregion
        #region public

        /// <summary>
        ///  called from the designer, all the values converted to pixel before sending
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// templateid, categoryIDv2,tempHMM,tempWMM,organisationId,itemId
        public HttpResponseMessage GetTemplate(long parameter1, long parameter2, double parameter3, double parameter4,long parameter5,long parameter6)
        {
            var template = templateService.GetTemplateInDesigner(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, template, formatter);
         
        }

        //[System.Web.Http.AcceptVerbs("GET", "POST")]
        //[System.Web.Http.HttpGet]
        // Important: if called from MIS call implementation function instead of this function because OrganisationID will not exist in cookie when called from MIS
        //public string testTemplate(long id)
        //{
        //    long OrganisationId = UserCookieManager.OrganisationID;
        //    //var result = templateService.generateTemplateFromPDF("F:\\Development\\Github\\MyPrintCloud-dev\\MPC.web\\MPC_Content\\Products\\Organisation1\\Templates\\random__CorporateTemplateUpload.pdf",2, id, 1);
        //    templateService.processTemplatePDF(id, 1, true, true, true);
        //    var formatter = new JsonMediaTypeFormatter();
        //    var json = formatter.SerializerSettings;
        //    json.Formatting = Newtonsoft.Json.Formatting.Indented;
        //    json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        //    return "true".ToString();

        //}
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage mergeTemplate(int parameter1, long parameter2, long parameter3, long parameter4)
        {

            var oCustomer = _myCompanyService.GetCompanyByCompanyID(parameter4);




            var template = templateService.MergeRetailTemplate(parameter1, parameter2, parameter3, false, oCustomer.StoreId.Value, 0, 0);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, template, formatter);

        }
        [HttpPost]
        public HttpResponseMessage Preview([FromBody]  DesignerPostSettings obj)
        {
            double bleedAreaSize = 0; // will be calculated in generate proof function 
            //MyCompanyDomainBaseResponse response = _myCompanyService.GetStoreFromCache(UserCookieManager.WBStoreId).CreateFromOrganisation();
            
            //if(response != null)
            //{
            //    var org = response.Organisation;
            //    if(org != null && org.BleedAreaSize != null)
            //    {
            //        bleedAreaSize = org.BleedAreaSize.Value;
            //    }
            //}
            
            var result = templateService.GenerateProof(obj,bleedAreaSize);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, result, formatter);
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage getQuickText(long parameter1, long parameter2)
        {
            var result = templateService.GetContactQuickTextFields(parameter1, parameter2);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, result, formatter);

        }
        [HttpPost]
        public HttpResponseMessage SaveQuickText([FromBody]  QuickText obj)
        {
            var result = templateService.UpdateQuickTextTemplateSelection(obj);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, result, formatter);
        }

       [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        //templateid,itemid,customerId,displayName,caller,organisationId
        public HttpResponseMessage SaveDesignAttachments(long parameter1,long parameter2,long parameter3,string parameter4,string parameter5,long parameter6)
        {
            var result = itemService.SaveDesignAttachments(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, result, formatter);
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        //long WEBOrderId, int WEBStoreMode, long TemporaryCompanyId, long OrganisationId, long CompanyID, long ContactID, long itemID
        public HttpResponseMessage AutoGenerateTemplate(long parameter1, int parameter2, long parameter3,long parameter4, long parameter5, long parameter6, long parameter7, long parameter8)
        {
            var result = itemService.ProcessCorpOrderSkipDesignerMode(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8); //"generating";// itemService.SaveDesignAttachments(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, result, formatter);
        }

        //parameter 1 = templateID , parameter 2= companyID
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage updateTemplateVariables(long parameter1, long parameter2)
        {
            var res = _templateVariableService.UpdateTemplateVariablesList(parameter1,parameter2);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, res, formatter);

        }
        
        #endregion
    }
 
}
