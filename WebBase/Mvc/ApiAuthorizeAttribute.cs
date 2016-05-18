using System;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;

namespace MPC.WebBase.Mvc
{
    /// <summary>
    /// Api Authorize Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        #region Private

        private SecurityAccessRight[] accessRights = new SecurityAccessRight[0];
        private string[] misRoles = { "Admin", "Manager", "User" };

        #endregion

        /// <summary>
        /// Check if user is authorized on a given permissionKey
        /// </summary>
        protected override bool IsAuthorized(HttpActionContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            IPrincipal user = HttpContext.Current.User;
            if (!user.Identity.IsAuthenticated)
            {
                return false;
            }
            AuthorizationChecker = UnityConfiguration.UnityConfig.GetConfiguredContainer().Resolve<IAuthorizationChecker>();
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
        public IAuthorizationChecker AuthorizationChecker { get; set; }

        #endregion

    }
}