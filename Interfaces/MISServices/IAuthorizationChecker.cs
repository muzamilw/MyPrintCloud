using System.Collections.Generic;
using MPC.Interfaces.Data;

namespace MPC.Interfaces.MISServices
{
    /// <summary>
    /// Check that the current user has the required access rights
    /// </summary>
    public interface IAuthorizationChecker
    {
        /// <summary>
        /// Check 
        /// </summary>
        /// <returns>True if the user has the required rights</returns>
        bool Check(AuthorizationCheckRequest request);

        /// <summary>
        /// Check if use has all the required access rights
        /// </summary>
        bool HasRequiredAccessRights(IEnumerable<SecurityAccessRight> requiredAccessRights);

        /// <summary>
        /// Check if use has all the required access rights
        /// </summary>
        bool HasRequiredAccessRight(SecurityAccessRight requiredAccessRight);

        /// <summary>
        /// Check if the user has the required portal roles
        /// </summary>
        bool HasRequiredPortalRole(IEnumerable<string> requiredPortalRoles);

        /// <summary>
        /// True if the current user is a portal administrator (work across sites, ie. if applied to one site user then we are good)
        /// </summary>
        bool IsPortalAdministrator();
    }
}
