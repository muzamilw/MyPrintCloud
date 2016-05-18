using System;
using System.Collections.Generic;
using System.Linq;
using MPC.Common;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.Models.Common;
using MPC.Interfaces.Repository;

namespace MPC.Implementation.MISServices
{
    /// <summary>
    /// Check that the current user has the required access rights
    /// </summary>
    public sealed class AuthorizationChecker : IAuthorizationChecker
    {
        #region Public
        private readonly IRoleRepository _roleRepository;
        private List<string> rolesList { get; set; }

        public AuthorizationChecker(IRoleRepository roleRepository)
        {
            this._roleRepository = roleRepository;
            GetUserRoles();
        }
        /// <summary>
        /// Check that the user has the required rights
        /// </summary>
        public bool Check(AuthorizationCheckRequest request)
        {
            if (IsPortalAdministrator() && HasValidCompany())
            {
                return true;
            }

            if (!HasValidCompany())
            {
                return false;
            }
            //var hasRole = HasRequiredPortalRole(request.RequiredPortalRoles);
            var hasRole = HasRequiredPortalRole(rolesList);
            var hasRights = HasRequiredAccessRights(request.RequiredAccessRights);
            return hasRole && hasRights;
               
        }
        
        /// <summary>
        /// True if the current user is a portal administrator (work across sites, ie. if applied to one site user then we are good)
        /// </summary>
        public bool IsPortalAdministrator()
        {
            IList<MisRoleClaimValue> roles = ClaimHelper.GetClaimsByType<MisRoleClaimValue>(MpcClaimTypes.MisRole);
            var role = roles.FirstOrDefault();
            return IsAdminRole(Convert.ToInt32(role.RoleId));
            //return roles.Any(role => role.Role == SecurityRoles.Admin);
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
            var isValidRole = roles.Any(role => requiredPortalRoles.Contains(role.RoleId.ToString()));
            var hasRole = requiredPortalRoles.Any() && isValidRole ? true : false;
            return hasRole;
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

        /// <summary>
        /// Check if the user has valid company
        /// </summary>
        public bool HasValidCompany()
        {
            IEnumerable<OrganisationClaimValue> organisationClaimValues = ClaimHelper.GetClaimsByType<OrganisationClaimValue>(MpcClaimTypes.Organisation);
            return organisationClaimValues != null && organisationClaimValues.Any(org => org.OrganisationId > 0);
        }

        private void GetUserRoles()
        {
            rolesList = _roleRepository.GetUserRoles().Select(a => a.RoleId.ToString()).ToList();
        }

        private bool IsAdminRole(int roleId)
        {
            var role = _roleRepository.GetRoleById(roleId);
            if (role != null)
            {
                return Convert.ToBoolean(role.IsCompanyLevel);
            }
            else
            {
              return  false;
            }
            
        }
        #endregion
    }
}
