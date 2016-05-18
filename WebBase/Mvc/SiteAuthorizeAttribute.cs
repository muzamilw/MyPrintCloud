using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;

namespace MPC.WebBase.Mvc
{
    /// <summary>
    /// Site Authorize Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class SiteAuthorizeAttribute : AuthorizeAttribute
    {
        #region Private

        private SecurityAccessRight[] accessRights = new SecurityAccessRight[0];
        private string[] misRoles = { "Admin", "Manager", "User" };

        #endregion

        /// <summary>
        /// Check if user is authorized on a given permissionKey
        /// </summary>
        private bool IsAuthorized()
        {
            IPrincipal user = HttpContext.Current.User;
            if (!user.Identity.IsAuthenticated)
            {
                return false;
            }
            if (AuthorizationChecker == null)
            {
                throw new InvalidOperationException(
                    LanguageResources.SiteAuthorizeAttribute_AuthorizationCheckerMissing);
            }

            return AuthorizationChecker.Check(
                new AuthorizationCheckRequest(MisRoles, AccessRights));
        }

        #region Public

        /// <summary>
        /// Perform the authorization
        /// </summary>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            return IsAuthorized();
        }
        /// <summary>
        /// Redirects request to unauthroized request page
        /// </summary>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                filterContext.Result =
                    new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new { area = "", controller = "UnauthorizedRequest", action = "Index" }));
            }
        }

        /// <summary>
        /// Access rights
        /// </summary>
        public SecurityAccessRight[] AccessRights
        {
            get
            {
                return accessRights;
            }
            set
            {
                accessRights = value;
            }
        }

        /// <summary>
        /// MIS Roles
        /// </summary>
        public string[] MisRoles
        {
            get
            {
                return misRoles;
            }
            set
            {
                misRoles = value;
            }
        }

        /// <summary>
        /// Authorization check
        /// </summary>
        [Dependency]
        public IAuthorizationChecker AuthorizationChecker { get; set; }

        #endregion

    }
}