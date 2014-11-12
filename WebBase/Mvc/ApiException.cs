using System.Net.Http;
using System.Web.Http.Filters;
using MPC.ExceptionHandling;
using MPC.ExceptionHandling;
using Newtonsoft.Json;

namespace MPC.WebBase.Mvc
{
    /// <summary>
    /// Api Exception filter attribute for Api controller methods
    /// </summary>
    public class ApiException : ActionFilterAttribute
    {
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

        /// <summary>
        /// Exception Handler for api calls; apply this attribute for all the Api calls
        /// </summary>
        public override void OnActionExecuted(HttpActionExecutedContext filterContext)
        {
            if (filterContext.Exception is MPCException)
            {
                SetApplicationResponse(filterContext);
            }
        }
    }
}
