using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.IdentityModel.Claims;
using MPC.Common;
using MPC.Interfaces.MISServices;
using MPC.Models.Common;

namespace MPC.Implementation.MISServices
{
    /// <summary>
    /// Service that manages security claims
    /// </summary>
    public sealed class ClaimsSecurityService : MarshalByRefObject, IClaimsSecurityService
    {
        #region Private

        /// <summary>
        /// Add the user's name to the claims
        /// </summary>
        private void CheckName(MisUser user, IClaimsIdentity claimsIdentity)
        {
            Claim nameClaim = claimsIdentity.Claims.FirstOrDefault(c => c.ClaimType == ClaimTypes.Name);
            if (nameClaim == null)
            {
                claimsIdentity.Claims.Add(new Claim(ClaimTypes.Name, user.FullName));
            }
        }

        /// <summary>
        /// Add Access Right claims
        /// </summary>
        private static void AddAccessRightClaims(MisUser user, IClaimsIdentity claimsIdentity)
        {
            List<AccessRight> accessRights = user.RoleSections.SelectMany(roleSection => roleSection.Section.AccessRights).ToList();
            foreach (AccessRight accessRight in accessRights)
            {
                Claim claim = new Claim(MpcClaimTypes.AccessRight,
                                        ClaimHelper.Serialize(
                                            new AccessRightClaimValue 
                                            { 
                                                RightId = accessRight.RightId, 
                                                Right = accessRight.RightName, 
                                                SectionId = accessRight.SectionId 
                                            }),
                                        typeof(AccessRightClaimValue).AssemblyQualifiedName);
                claimsIdentity.Claims.Add(claim);
            }
        }

        /// <summary>
        /// Add security role claims
        /// </summary>
        private static void AddRoleClaims(MisUser user, IClaimsIdentity claimsIdentity)
        {
            Claim claim = new Claim(MpcClaimTypes.MisRole,
                                        ClaimHelper.Serialize(
                                            new MisRoleClaimValue { Role = user.Role }),
                                        typeof(MisRoleClaimValue).AssemblyQualifiedName);
            claimsIdentity.Claims.Add(claim);
        }

        /// <summary>
        /// Add Organisation Claims
        /// </summary>
        private static void AddOrganisationClaims(MisUser user, IClaimsIdentity claimsIdentity)
        {
            Claim claim = new Claim(MpcClaimTypes.Organisation,
                                        ClaimHelper.Serialize(
                                            new OrganisationClaimValue { OrganisationId = user.OrganisationId }),
                                        typeof(OrganisationClaimValue).AssemblyQualifiedName);
            claimsIdentity.Claims.Add(claim);
        }

        /// <summary>
        /// Add role claims
        /// </summary>
        private void AddUserClaims(MisUser user, IClaimsIdentity claimsIdentity)
        {
            AddRoleClaims(user, claimsIdentity);
            AddOrganisationClaims(user, claimsIdentity);
            AddAccessRightClaims(user, claimsIdentity);
        }

        #endregion

        #region Public
        
        /// <summary>
        /// Lookup name id and provider name
        /// </summary>
        public void LookupIdentityClaimValues(IClaimsIdentity claimsIdentity, out string providerName, out string nameIdentifier)
        {
            Claim nameIdentifierClaim = claimsIdentity.Claims.SingleOrDefault(c => c.ClaimType == ClaimTypes.NameIdentifier);
            if (nameIdentifierClaim == null)
            {
                throw new InvalidOperationException(LanguageResources.ClaimsSecurityService_MissingNameIdentifierClaim);
            }

            Claim identityProviderClaim = claimsIdentity.Claims.SingleOrDefault(c => c.ClaimType == IdentityProviderClaimType);
            if (identityProviderClaim == null)
            {
                throw new InvalidOperationException(LanguageResources.ClaimsSecurityService_IdentityProviderClaim);
            }

            providerName = identityProviderClaim.Value;
            nameIdentifier = nameIdentifierClaim.Value;
        }

        /// <summary>
        /// Lookup identity using the claims identity
        /// </summary>
        public IClaimsIdentity LookupIdentity(IClaimsIdentity claimsIdentity)
        {
            if (claimsIdentity == null)
            {
                throw new ArgumentNullException("claimsIdentity");
            }

            string providerName;
            string nameIdentifier;

            LookupIdentityClaimValues(claimsIdentity, out providerName, out nameIdentifier);

            return claimsIdentity;
        }

        /// <summary>
        /// Add claims to the identity
        /// </summary>
        public void AddClaimsToIdentity(UserIdentityModel identity, IClaimsIdentity claimsIdentity)
        {
            if (identity.User != null)
            {
                CheckName(identity.User, claimsIdentity);
                AddUserClaims(identity.User, claimsIdentity);
            }
        }


        /// <summary>
        /// Identity provider as supplied by ACS
        /// </summary>
        public const string IdentityProviderClaimType = @"http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider";

        #endregion
    }
}
