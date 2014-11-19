using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MPC.ExceptionHandling;
using MPC.ExceptionHandling.Logger;
using MPC.Interfaces.Logger;
using MPC.WebBase.UnityConfiguration;

namespace MPC.WebBase.Mvc
{
    /// <summary>
    /// Log Exception Filter Attribut
    /// </summary>
    public sealed class LogExceptionFilterAttribute : HandleErrorAttribute, System.Web.Http.Filters.IExceptionFilter
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
                    MPCLogger.Write(execption, MPCLogCategory.Warning, 1, 0, TraceEventType.Information, MPCLogCategory.Warning, properties);
                }
                else
                {
                    MPCLogger.Write(filterContext.Exception, MPCLogCategory.Error, 1, 0, TraceEventType.Critical, MPCLogCategory.Error, properties);
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
