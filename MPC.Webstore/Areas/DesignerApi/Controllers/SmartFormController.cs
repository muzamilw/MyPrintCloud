using AutoMapper;
using MPC.Common;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
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
        // parameter1 = user id , parameter2 = smartFormId, parameter3 = templateID
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetSmartFormData(long parameter1,long parameter2,long parameter3)
        {
            List<SmartFormUserList> usersListData = null;
            SmartFormWebstoreResponse objSmartform = smartFormService.GetSmartForm(parameter2);
            List<SmartFormDetail> smartFormObjs = smartFormService.GetSmartFormObjects(parameter2);
            bool hasContactVariables = false;
            Dictionary<long, List<ScopeVariable>> AllUserScopeVariables = null;
            List<ScopeVariable> scopeVariable = smartFormService.GetScopeVariables(smartFormObjs,out hasContactVariables,parameter1);
            List<ScopeVariable> allTemplateVariables = smartFormService.GetTemplateScopeVariables(parameter3, parameter1);
            List<ScopeVariable> variablesToRemove = new List<ScopeVariable>();
            //  variablesList = variables;
            foreach (var variable in scopeVariable)
            {
                if (variable == null)
                    variablesToRemove.Add(variable);
            }
            foreach (var variable in variablesToRemove)
            {
                scopeVariable.Remove(variable);
            }
            foreach(var item in allTemplateVariables)
            {
                var sVariable = scopeVariable.Where(g => g.VariableId == item.VariableId).FirstOrDefault();
                if(sVariable == null)
                {
                    scopeVariable.Add(item);
                }
            }
            if (hasContactVariables)
            {
                usersListData = new List<SmartFormUserList>();
                usersListData = smartFormService.GetUsersList(parameter1);
                if (usersListData != null)
                {
                    AllUserScopeVariables = new Dictionary<long, List<ScopeVariable>>();
                    AllUserScopeVariables = smartFormService.GetUserScopeVariables(smartFormObjs, usersListData, parameter3);

                }
            }
            List<VariableExtensionWebstoreResposne> extension = smartFormService.getVariableExtensions(scopeVariable, parameter1);
            SmartFormUserData result = new SmartFormUserData(usersListData, objSmartform, smartFormObjs, scopeVariable, AllUserScopeVariables,extension);


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

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        // parameter 1 = item id 
        // parameter 2 = contactID 
        public HttpResponseMessage GetUserVariableData(long parameter1, long parameter2)
        {
            var listVar = smartFormService.GetUserTemplateVariables(parameter1, parameter2);
            List<VariableExtensionWebstoreResposne> extension = smartFormService.getVariableExtensions(listVar, parameter2);
            UserVariableData result = new UserVariableData(listVar, extension);
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
        public SmartFormWebstoreResponse smartForm { get; set; }
        public List<SmartFormDetail> smartFormObjs { get; set; }

        public List<ScopeVariable> scopeVariables { get; set; }
        public Dictionary<long, List<ScopeVariable>> AllUserScopeVariables;

        public List<VariableExtensionWebstoreResposne> variableExtensions { get; set; }
        public SmartFormUserData(List<SmartFormUserList> usersList, SmartFormWebstoreResponse smartForm, List<SmartFormDetail> smartFormObjs, List<ScopeVariable> scopeVariables, Dictionary<long, List<ScopeVariable>> AllUserScopeVariables, List<VariableExtensionWebstoreResposne> variableExtensions)
        {
            this.usersList = usersList;
            this.smartForm = smartForm;
            this.smartFormObjs = smartFormObjs;
            this.scopeVariables = scopeVariables;

            if (AllUserScopeVariables != null)
            {
              foreach (var item in AllUserScopeVariables)
              {
                  foreach(var scope in item.Value)
                  {
                      if(scope.FieldVariable != null)
                      {
                          if(scope.FieldVariable.Company != null)
                          {
                              scope.FieldVariable.Company = null;
                          }
                      }
                  }
              }
              this.AllUserScopeVariables = AllUserScopeVariables;
            }
            else
            {
                this.AllUserScopeVariables = AllUserScopeVariables;
            }
         

          

            
            this.variableExtensions = variableExtensions;
        }
    }
    public class UserVariableData
    {
        public List<ScopeVariable> scopeVariables { get; set; }
        public List<VariableExtensionWebstoreResposne> variableExtensions { get; set; }
        public UserVariableData(List<ScopeVariable> scopeVariables, List<VariableExtensionWebstoreResposne> variableExtensions)
        {
            this.scopeVariables = scopeVariables;
            this.variableExtensions = variableExtensions;
        }
    }
}
