using System;
using System.Collections.Generic;
using System.Linq;
//using Microsoft.IdentityModel.Claims;
//using Microsoft.IdentityModel.Web;
using MPC.Common;
using MPC.Interfaces.MISServices;
using MPC.Models.Common;
using System.Security.Claims;

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
        private void CheckName(MisUser user, ClaimsIdentity claimsIdentity)
        {
            Claim nameClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            if (nameClaim == null)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.FullName));
                claimsIdentity.AddClaim(new Claim(MpcClaimTypes.MisUser,
                                        ClaimHelper.Serialize(
                                            new NameClaimValue
                                            {
                                                Name = user.FullName
                                            }),
                                        typeof(NameClaimValue).AssemblyQualifiedName));
                
                if (string.IsNullOrEmpty(user.Id))
                {
                    return;
                }

                Guid userId;
                if (Guid.TryParse(user.Id, out userId))
                {
                    claimsIdentity.AddClaim(new Claim(MpcClaimTypes.SystemUser,
                        ClaimHelper.Serialize(
                            new SystemUserClaimValue
                            {
                                SystemUserId = userId
                            }),
                        typeof(SystemUserClaimValue).AssemblyQualifiedName));        
                }
            }
        }
        
        /// <summary>
        /// Add Access Right claims
        /// </summary>
       private static void AddAccessRightClaims(MisUser user, ClaimsIdentity claimsIdentity)
        {
           
            List<AccessRight> accessRights = user.RoleSections.SelectMany(roleSection => roleSection.Section.AccessRights).ToList();
            foreach (AccessRight accessRight in accessRights)
            {
// ReSharper disable SuggestUseVarKeywordEvident
                Claim claim = new Claim(MpcClaimTypes.AccessRight,
// ReSharper restore SuggestUseVarKeywordEvident
                                        ClaimHelper.Serialize(
                                            new AccessRightClaimValue 
                                            { 
                                                RightId = accessRight.RightId, 
                                                Right = accessRight.RightName, 
                                                SectionId = accessRight.SectionId 
                                            }),
                                        typeof(AccessRightClaimValue).AssemblyQualifiedName);
                claimsIdentity.AddClaim(claim);
            }
        }

        #endregion
        #region Constructor
        /// <summary>
        /// Add security role claims
        /// </summary>
        private static void AddRoleClaims(MisUser user, ClaimsIdentity claimsIdentity)
        {
// ReSharper disable SuggestUseVarKeywordEvident
            Claim claim = new Claim(MpcClaimTypes.MisRole,
// ReSharper restore SuggestUseVarKeywordEvident
                                        ClaimHelper.Serialize(
                                            new MisRoleClaimValue { Role = user.Role, RoleId = user.RoleId}),
                                        typeof(MisRoleClaimValue).AssemblyQualifiedName);
            claimsIdentity.AddClaim(claim);
        }

        /// <summary>
        /// Adds Trail User Claims
        /// </summary>
        private static void AddTrialUserClaims(MisUser user, ClaimsIdentity claimsIdentity)
        {
            // ReSharper disable once SuggestUseVarKeywordEvident
            Claim isTrialClaim = new Claim(MpcClaimTypes.IsTrial,
                                        ClaimHelper.Serialize(
                                            new IsTrialClaimValue{ IsTrial = user.IsTrial }),
                                        typeof(IsTrialClaimValue).AssemblyQualifiedName);

            // ReSharper disable once SuggestUseVarKeywordEvident
            Claim trailCountClaim = new Claim(MpcClaimTypes.TrialCount,
                                        ClaimHelper.Serialize(
                                            new TrialCountClaimValue { TrialCount = user.TrialCount }),
                                        typeof(TrialCountClaimValue).AssemblyQualifiedName);
            claimsIdentity.AddClaim(isTrialClaim);
            claimsIdentity.AddClaim(trailCountClaim);
        }
        

        /// <summary>
        /// Add Organisation Claims
        /// </summary>
        private static void AddOrganisationClaims(MisUser user, ClaimsIdentity claimsIdentity)
        {
// ReSharper disable SuggestUseVarKeywordEvident
            Claim claim = new Claim(MpcClaimTypes.Organisation,
// ReSharper restore SuggestUseVarKeywordEvident
                                        ClaimHelper.Serialize(
                                            new OrganisationClaimValue { OrganisationId = user.OrganisationId }),
                                        typeof(OrganisationClaimValue).AssemblyQualifiedName);
            claimsIdentity.AddClaim(claim);
        }

        #endregion

        #region Public
        
        /// <summary>
        /// Add User Claims
        /// </summary>
        private void AddUserClaims(MisUser user, ClaimsIdentity claimsIdentity)
        {
            AddRoleClaims(user, claimsIdentity);
            AddOrganisationClaims(user, claimsIdentity);
            AddAccessRightClaims(user, claimsIdentity);
            AddTrialUserClaims(user, claimsIdentity);
        }

        #endregion
        
        #region Public

        ///// <summary>
        ///// Handles the <see cref="WSFederationAuthenticationModule.SecurityTokenValidated"/> event
        ///// </summary>
        //public void SecurityTokenValidated(object sender, SecurityTokenValidatedEventArgs e, string ipAddress)
        //{
        //    IClaimsPrincipal claimsPrincipal = e.ClaimsPrincipal;
        //    IClaimsIdentity claimsIdentity = (IClaimsIdentity)claimsPrincipal.Identity;

        //    if (claimsIdentity.IsAuthenticated)
        //    {
        //        IClaimsIdentity identity = LookupIdentity(claimsIdentity);

        //        if (identity != null)
        //        {
        //            // Add Claims To Identity
        //        }
        //    }
        //}
        
        /// <summary>
        /// Lookup name id and provider name
        /// </summary>
        public void LookupIdentityClaimValues(ClaimsIdentity claimsIdentity, out string providerName, out string nameIdentifier)
        {
            Claim nameIdentifierClaim = claimsIdentity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (nameIdentifierClaim == null)
            {
                throw new InvalidOperationException(LanguageResources.ClaimsSecurityService_MissingNameIdentifierClaim);
            }

            Claim identityProviderClaim = claimsIdentity.Claims.SingleOrDefault(c => c.Type == IdentityProviderClaimType);
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
        public ClaimsIdentity LookupIdentity(ClaimsIdentity claimsIdentity)
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
        public void AddClaimsToIdentity(UserIdentityModel identity, ClaimsIdentity claimsIdentity)
        {
            if (identity.User != null)
            {
                CheckName(identity.User, claimsIdentity);
                AddUserClaims(identity.User, claimsIdentity);
            }
        }

        /// <summary>
        /// Sets claims for trial user
        /// </summary>
        public void GetTrialUserClaims(ref Boolean isTrial, ref int count)
        {
            IsTrialClaimValue trialClaimValue = ClaimHelper.GetClaimsByType<IsTrialClaimValue>(MpcClaimTypes.IsTrial).FirstOrDefault();
            TrialCountClaimValue countClaimValue = ClaimHelper.GetClaimsByType<TrialCountClaimValue>(MpcClaimTypes.TrialCount).FirstOrDefault();
            if (trialClaimValue != null)
                isTrial = trialClaimValue.IsTrial;
            if (countClaimValue != null)
                count = countClaimValue.TrialCount;
        }
        /// <summary>
        /// Identity provider as supplied by ACS
        /// </summary>
        public const string IdentityProviderClaimType = @"http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider";

        #endregion
    }
}
