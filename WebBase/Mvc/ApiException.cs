using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Web.Http.Filters;
using Microsoft.Practices.Unity;
using MPC.ExceptionHandling;
using MPC.ExceptionHandling.Logger;
using MPC.Interfaces.Logger;
using MPC.WebBase.UnityConfiguration;
using Newtonsoft.Json;

namespace MPC.WebBase.Mvc
{
    /// <summary>
    /// Api Exception filter attribute for Api controller methods
    /// </summary>
    public class ApiException : ActionFilterAttribute
    {
        #region Private
        private static IMPCLogger mpcLogger;
        /// <summary>
        /// Get Configured logger
        /// </summary>
        private static IMPCLogger MPCLogger
        {
            get
            {
                if (mpcLogger != null) return mpcLogger;
                mpcLogger = (UnityConfig.GetConfiguredContainer()).Resolve<IMPCLogger>();
                return mpcLogger;
            }
        }

        /// <summary>
        /// Set status code and contents of the Application exception
        /// </summary>
        private void SetApplicationResponse(HttpActionExecutedContext filterContext)
        {
            MPCExceptionContent contents = new MPCExceptionContent
            {
                Message = filterContext.Exception.Message                
            };
            filterContext.Response = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Content = new StringContent(JsonConvert.SerializeObject(contents))                
            };
        }
        private void SetGeneralExceptionApplicationResponse(HttpActionExecutedContext filterContext)
        {
            MPCExceptionContent contents = new MPCExceptionContent
            {
                Message = "There is some problem while performing this operation."
            };
            filterContext.Response = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Content = new StringContent(JsonConvert.SerializeObject(contents))
            };
        }
        /// <summary>
        /// Log Error
        /// </summary>
        private void LogError(Exception exp, int domainKey, string requestContents)
        {
            MPCLogger.Write(exp, MPCLogCategory.Error, -1, -1, TraceEventType.Warning, "", new Dictionary<string, object> { { "DomainKey", domainKey }, { "RequestContents", requestContents } });
        }
        #endregion

        /// <summary>
        /// Exception Handler for api calls; apply this attribute for all the Api calls
        /// </summary>
        public override void OnActionExecuted(HttpActionExecutedContext filterContext)
        {
            if (filterContext.Exception is MPCException)
            {
                SetApplicationResponse(filterContext);                
                MPCException exp = filterContext.Exception as MPCException;
                LogError(exp, exp.DomainKey, filterContext.Request.Content.ToString());
            }
            else
            {
                SetGeneralExceptionApplicationResponse(filterContext);
                LogError(filterContext.Exception, -1, filterContext.Request.Content.ToString());
            }
            
        }
    }
}
