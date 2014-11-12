using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace MPC.WebBase.Mvc
{
    /// <summary>
    /// Log Exception Filter Attribut
    /// </summary>
    public sealed class LogExceptionFilterAttribute : HandleErrorAttribute, System.Web.Http.Filters.IExceptionFilter
    {
        #region Private

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
                Dictionary<string, object> properties = new Dictionary<string, object>();

                // add route data
                foreach (string key in filterContext.RouteData.Values.Keys)
                {
                    properties.Add(RouteDataPrefix + key, filterContext.RouteData.Values[key]);
                }

                Logger.Write(filterContext.Exception.ToString(),
                    LoggerCategories.Error, 0, 0, TraceEventType.Error, "Mvc Controller Error", properties);
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

        /// <summary>
        /// Logger
        /// </summary>
        //[Dependency]
        public string Logger { get; set; }

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
