using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace MPC.WebBase.Mvc
{
    /// <summary>
    /// Filter that checks the input parameters are valid
    /// </summary>
    public class ValidateFilterAttribute : ActionFilterAttribute
    {
        #region Public

        /// <summary>
        /// Implement the filter
        /// </summary>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var modelState = actionExecutedContext.ActionContext.ModelState;
            if (!modelState.IsValid)
            {
                var errors = modelState
                    .Where(s => s.Value.Errors.Count > 0)
                    .Select(s => new KeyValuePair<string, string>(s.Key, s.Value.Errors.First().ErrorMessage))
                    .ToArray();

                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    errors
                );
            }
            base.OnActionExecuted(actionExecutedContext);
        }

        #endregion
    }
}
