using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Claims;
using Microsoft.IdentityModel.Web;
using MPC.Interfaces.MISServices;

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
        private void CheckName(string user, IClaimsIdentity claimsIdentity)
        {
            Claim nameClaim = claimsIdentity.Claims.FirstOrDefault(c => c.ClaimType == ClaimTypes.Name);
            if (nameClaim == null)
            {
       //         claimsIdentity.Claims.Add(new Claim(ClaimTypes.Name, user));
            }
        }
        
        ///// <summary>
        ///// Add security role claims
        ///// </summary>
        //private static void AddRoleClaims(IClaimsIdentity claimsIdentity, SiteUser siteUser)
        //{
        //    foreach (SecurityRole role in siteUser.SecurityRoles)
        //    {
        //        Claim claim = new Claim(OdyPortalClaimTypes.OdyPortalRole,
        //                                ClaimHelper.Serialize(
        //                                    new RoleClaimValue { Role = role.Name, SiteUrlPostFix = siteUser.Site.UrlPostFix }),
        //                                typeof(RoleClaimValue).AssemblyQualifiedName);
        //        claimsIdentity.Claims.Add(claim);
        //    }
        //}
        
        /// <summary>
        /// Add role claims
        /// </summary>
        private void AddSiteUserClaims(IClaimsIdentity claimsIdentity, string siteUser)
        {
           // AddSiteAccessClaims(claimsIdentity, siteUser);
          //  AddRoleClaims(claimsIdentity, siteUser);
          //  AddRepresentationClaims(claimsIdentity, siteUser);
        }

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ClaimsSecurityService()
        {
        }
        #endregion
        #region Public

        /// <summary>
        /// Handles the <see cref="WSFederationAuthenticationModule.SecurityTokenValidated"/> event
        /// </summary>
        public void SecurityTokenValidated(object sender, SecurityTokenValidatedEventArgs e, string ipAddress)
        {
            IClaimsPrincipal claimsPrincipal = e.ClaimsPrincipal;
            IClaimsIdentity claimsIdentity = (IClaimsIdentity)claimsPrincipal.Identity;

            if (claimsIdentity.IsAuthenticated)
            {
                //UserLoginIdentity identity = LookupIdentity(claimsIdentity);

                //if (identity != null)
              //  {
                //    AddClaimsToIdentity(identity, claimsIdentity);
              //  }
            }
        }
        
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
        public void AddClaimsToIdentity(IClaimsIdentity identity, IClaimsIdentity claimsIdentity)
        {
          //  if (identity.User != null)
       //     {
         //       CheckName(identity.User, claimsIdentity);

         //       foreach (SiteUser siteUser in identity.User.SiteUsers)
          //      {
             //       AddSiteUserClaims(claimsIdentity, siteUser);
         //       }
        //    }
        }


        /// <summary>
        /// Identity provider as supplied by ACS
        /// </summary>
        public const string IdentityProviderClaimType = @"http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider";

        #endregion
    }
}
