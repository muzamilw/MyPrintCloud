﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Web;
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
// ReSharper disable InconsistentNaming
        private static IMPCLogger mpcLogger;
// ReSharper restore InconsistentNaming
        /// <summary>
        /// Get Configured logger
        /// </summary>
// ReSharper disable InconsistentNaming
        private static IMPCLogger MPCLogger
// ReSharper restore InconsistentNaming
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
// ReSharper disable SuggestUseVarKeywordEvident
            MPCExceptionContent contents = new MPCExceptionContent
// ReSharper restore SuggestUseVarKeywordEvident
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
            string exceptionMessage = filterContext.Exception == null || HttpContext.Current.IsDebuggingEnabled
                ? string.Empty
                : filterContext.Exception.InnerException.Message;

// ReSharper disable SuggestUseVarKeywordEvident
            MPCExceptionContent contents = new MPCExceptionContent
// ReSharper restore SuggestUseVarKeywordEvident
            {
                Message = "There is some problem while performing this operation. " + exceptionMessage
                // Replace message text with this line for production environment 
                // filterContext.Exception.InnerException.Message
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
        private void LogError(Exception exp, long organisationId, string requestContents)
        {
            MPCLogger.Write(exp, MPCLogCategory.Error, -1, -1, TraceEventType.Warning, "", new Dictionary<string, object> { { "Organisation", organisationId }, 
            { "RequestContents", requestContents } });
        }
        #endregion

        /// <summary>
        /// Exception Handler for api calls; apply this attribute for all the Api calls
        /// </summary>
        public override void OnActionExecuted(HttpActionExecutedContext filterContext)
        {
            if (filterContext.Exception == null)
            {
                return;
            }
            if (filterContext.Exception is MPCException)
            {
                SetApplicationResponse(filterContext);
// ReSharper disable SuggestUseVarKeywordEvident
                MPCException exp = filterContext.Exception as MPCException;
// ReSharper restore SuggestUseVarKeywordEvident
                LogError(exp, exp.OrganisationId, filterContext.Request.Content.ToString());
            }
            else
            {
                SetGeneralExceptionApplicationResponse(filterContext);
                LogError(filterContext.Exception, -1, filterContext.Request.Content.ToString());
            }

        }
    }
}
