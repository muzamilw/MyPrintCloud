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
        // parameter1 = user id , parameter2 = smartFormId, parameter3 =
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetSmartFormData(long parameter1,long parameter2)
        {
            List<SmartFormUserList> usersListData = null;
            SmartForm objSmartform = smartFormService.GetSmartForm(parameter2);
            List<SmartFormDetail> smartFormObjs = smartFormService.GetSmartFormObjects(parameter2);
            bool hasContactVariables = false;
            Dictionary<long, List<ScopeVariable>> AllUserScopeVariables = null;
            List<ScopeVariable> scopeVariable = smartFormService.GetScopeVariables(smartFormObjs,out hasContactVariables,parameter1);
            if (hasContactVariables)
            {
                usersListData = new List<SmartFormUserList>();
                usersListData = smartFormService.GetUsersList(parameter1);
                if (usersListData != null)
                {
                    AllUserScopeVariables = new Dictionary<long, List<ScopeVariable>>();
                    AllUserScopeVariables = smartFormService.GetUserScopeVariables(smartFormObjs, usersListData);
                }
            }
            SmartFormUserData result = new SmartFormUserData(usersListData, objSmartform, smartFormObjs, scopeVariable, AllUserScopeVariables);


            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, result, formatter);

        }

        [HttpPost]
        public HttpResponseMessage SaveUserVariables([FromBody]   smartFormPostedUser obj)
        {
            Dictionary<long, List<ScopeVariable>> data = new Dictionary<long,List<ScopeVariable>>();
            data.Add(obj.contactId, obj.variables);
            var result =smartFormService.SaveUserProfilesData(data);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, result, formatter);
        }
        #endregion
    }

    public class smartFormPostedUser
    {
        public long contactId { get; set; }
        public List<ScopeVariable> variables { get; set; }
    }
    public class SmartFormUserData
    {
        public List<SmartFormUserList> usersList { get; set; }
        public SmartForm smartForm { get; set; }
        public List<SmartFormDetail> smartFormObjs { get; set; }

        public List<ScopeVariable> scopeVariables { get; set; }
        public Dictionary<long, List<ScopeVariable>> AllUserScopeVariables;

        public SmartFormUserData(List<SmartFormUserList> usersList, SmartForm smartForm, List<SmartFormDetail> smartFormObjs, List<ScopeVariable> scopeVariables, Dictionary<long, List<ScopeVariable>> AllUserScopeVariables)
        {
            this.usersList = usersList;
            this.smartForm = smartForm;
            this.smartFormObjs = smartFormObjs;
            this.scopeVariables = scopeVariables;
            this.AllUserScopeVariables = AllUserScopeVariables;
        }
    }
}
