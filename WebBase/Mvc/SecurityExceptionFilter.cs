using System.Net;
using System.Net.Http;
using System.Security;
using System.Web.Http.Filters;

namespace MPC.WebBase.Mvc
{
    /// <summary>
    /// Security Exception Filter
    /// </summary>
    public class SecurityExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// Handle security exception
        /// </summary>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is SecurityException)
            {

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content =
                        new StringContent(
                        actionExecutedContext
                        .Exception.Message)
                };

                actionExecutedContext.Response = response;
            }
        }
    }
}
