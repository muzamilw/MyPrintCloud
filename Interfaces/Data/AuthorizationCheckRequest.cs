using System;
using System.Collections.Generic;

namespace MPC.Interfaces.Data
{
    /// <summary>
    /// Used in IAuthorizationChecker.Check
    /// </summary>
    public sealed class AuthorizationCheckRequest
    {
        #region Private

        private readonly IEnumerable<string> requiredPortalRoles;
        private readonly IEnumerable<SecurityAccessRight> requiredAccessRights;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public AuthorizationCheckRequest(IEnumerable<string> requiredPortalRoles, 
            IEnumerable<SecurityAccessRight> requiredAccessRights)
        {
            if (requiredPortalRoles == null)
            {
                throw new ArgumentNullException("requiredPortalRoles");
            }

            if (requiredAccessRights == null)
            {
                throw new ArgumentNullException("requiredAccessRights");
            }

            this.requiredPortalRoles = requiredPortalRoles;
            this.requiredAccessRights = requiredAccessRights;
        }

        #endregion
        #region Public

        /// <summary>
        /// Required access rights
        /// </summary>
        public IEnumerable<SecurityAccessRight> RequiredAccessRights
        {
            get
            {
                return requiredAccessRights;
            }
        }

        /// <summary>
        /// Required Portal Roles
        /// </summary>
        public IEnumerable<string> RequiredPortalRoles
        {
            get
            {
                return requiredPortalRoles;
            }
        }
        
        #endregion
    }
}
