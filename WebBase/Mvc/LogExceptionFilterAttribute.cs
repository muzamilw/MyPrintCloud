using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
//using MPC.DatabaseLogger;

using MPC.ExceptionHandling;
using MPC.ExceptionHandling.Logger;
using MPC.Interfaces.Logger;

namespace MPC.WebBase.Mvc
{
    /// <summary>
    /// Log Exception Filter Attribut
    /// </summary>
    public sealed class LogExceptionFilterAttribute : HandleErrorAttribute, System.Web.Http.Filters.IExceptionFilter
    {
        #region Private

        private readonly IMPCLogger mpcLogger;
        public LogExceptionFilterAttribute(IMPCLogger mpcLogger)
        {
            this.mpcLogger = mpcLogger;
        }
        /// <summary>
        /// Route data prefix
        /// </summary>
        private const string RouteDataPrefix = "Route data: ";

        /// <summary>
        /// Log the exception
        /// </summary>
        private void LogException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled && filterContext.Exception != null)
            {
                Dictionary<string, object> properties = filterContext.RouteData.Values.Keys.ToDictionary(key => RouteDataPrefix + key, key => filterContext.RouteData.Values[key]);
                // add route data

                MPCException execption = filterContext.Exception as MPCException;
                if (execption != null)
                {
                    mpcLogger.Write(execption, MPCLogCategory.Warning, 1, 0, TraceEventType.Information, MPCLogCategory.Warning, properties);
                }
                else
                {
                    mpcLogger.Write(filterContext.Exception, MPCLogCategory.Error, 1, 0, TraceEventType.Critical, MPCLogCategory.Error, properties);
                }                  
            }
        }

        #endregion
        #region Public

        /// <summary>
        /// An exception occurred
        /// </summary>
        public override void OnException(ExceptionContext filterContext)
        {
            LogException(filterContext);

            base.OnException(filterContext);
        }

        #endregion
        /// <summary>
        /// Not implemented Async Call for Logging
        /// </summary>
        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
