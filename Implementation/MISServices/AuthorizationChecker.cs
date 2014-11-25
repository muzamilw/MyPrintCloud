using System;
using System.Collections.Generic;
using System.Linq;
using MPC.Common;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.Models.Common;

namespace MPC.Implementation.MISServices
{
    /// <summary>
    /// Check that the current user has the required access rights
    /// </summary>
    public sealed class AuthorizationChecker : IAuthorizationChecker
    {
        #region Public

        /// <summary>
        /// Check that the user has the required rights
        /// </summary>
        public bool Check(AuthorizationCheckRequest request)
        {
            if (IsPortalAdministrator())
            {
                return true;
            }
            
            return HasRequiredPortalRole(request.RequiredPortalRoles)
                && HasRequiredAccessRights(request.RequiredAccessRights);
        }
        
        /// <summary>
        /// True if the current user is a portal administrator (work across sites, ie. if applied to one site user then we are good)
        /// </summary>
        public bool IsPortalAdministrator()
        {
            IList<MisRoleClaimValue> roles = ClaimHelper.GetClaimsByType<MisRoleClaimValue>(MpcClaimTypes.MisRole);
            return roles.Any(role => role.Role == SecurityRoles.Admin);
        }

        /// <summary>
        /// Check if the user has the required portal roles
        /// </summary>
        public bool HasRequiredPortalRole(IEnumerable<string> requiredPortalRoles)
        {
            if (requiredPortalRoles == null)
            {
                throw new ArgumentNullException("requiredPortalRoles");
            }
            IEnumerable<MisRoleClaimValue> roles = ClaimHelper.GetClaimsByType<MisRoleClaimValue>(MpcClaimTypes.MisRole);
            return !requiredPortalRoles.Any() || roles.Any(role => requiredPortalRoles.Contains(role.Role));
        }

        /// <summary>
        /// Check if use has all the required access rights
        /// </summary>
        public bool HasRequiredAccessRights(IEnumerable<SecurityAccessRight> requiredAccessRights)
        {
            if (requiredAccessRights == null)
            {
                throw new ArgumentNullException("requiredAccessRights");
            }
            if (!requiredAccessRights.Any())
            {
                return true;
            }
            
            IEnumerable<AccessRightClaimValue> userAccessRights = ClaimHelper.GetClaimsByType<AccessRightClaimValue>(MpcClaimTypes.AccessRight);
            return requiredAccessRights.All(accessRight => userAccessRights.Any(arc => arc.RightId == (long)accessRight));
        }

        /// <summary>
        /// Check if use has all the required access rights
        /// </summary>
        public bool HasRequiredAccessRight(SecurityAccessRight requiredAccessRight)
        {
            return HasRequiredAccessRights(new[] { requiredAccessRight });
        }

        #endregion
    }
}
