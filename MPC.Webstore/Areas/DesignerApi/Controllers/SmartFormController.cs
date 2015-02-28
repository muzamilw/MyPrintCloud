﻿using MPC.Common;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
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
        // parameter1 = user id , parameter2 = smartFormId
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetUsersList(long parameter1,long parameter2)
        {
            List<SmartFormUserList> usersListData = smartFormService.GetUsersList(parameter1);
            SmartForm objSmartform = smartFormService.GetSmartForm(parameter2);
            List<SmartFormDetail> smartFormObjs = smartFormService.GetSmartFormObjects(parameter2);
            SmartFormUserData result = new SmartFormUserData(usersListData, objSmartform, smartFormObjs);


            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, result, formatter);

        }
        #endregion
    }


    public class SmartFormUserData
    {
        public List<SmartFormUserList> usersList { get; set; }
        public SmartForm smartForm { get; set; }
        public List<SmartFormDetail> smartFormObjs { get; set; }

        public SmartFormUserData(List<SmartFormUserList> usersList, SmartForm smartForm, List<SmartFormDetail> smartFormObjs)
        {
            this.usersList = usersList;
            this.smartForm = smartForm;
            this.smartFormObjs = smartFormObjs;
        }
    }
}
