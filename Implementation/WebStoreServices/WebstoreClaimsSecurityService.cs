using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FaceSharp.Api.Objects;
using MPC.Common;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;

namespace MPC.Implementation.WebStoreServices
{
    public sealed class WebstoreClaimsSecurityService : MarshalByRefObject, IWebstoreClaimsSecurityService
    {
        private static void AddLoginUserClaims(long contactId, long companyId, int roleId, long territoryId, ClaimsIdentity claimsIdentity, bool _HasUserDamRights)
        {
            Claim claim = new Claim(WebstoreClaimTypes.LoginUser,
                                        ClaimHelper.Serialize(
                                            new ContactClaimValue
                                            {
                                                ContactId = contactId,
                                                ContactCompanyId = companyId,
                                                ContactRoleId = roleId,
                                                ContactTerritoryId = territoryId,
                                                HasUserDamRights = _HasUserDamRights
    
                                            }),
                                        typeof(ContactClaimValue).AssemblyQualifiedName);
            claimsIdentity.AddClaim(claim);
        }

        public void AddSignInClaimsToIdentity(long contactId, long companyId, int roleId, long territoryId, ClaimsIdentity claimsIdentity, bool? HasUserDamRights)
        {
            if (HasUserDamRights != null)
            {
                AddLoginUserClaims(contactId, companyId, roleId, territoryId, claimsIdentity, (bool)HasUserDamRights);
            }
            else
            {
                AddLoginUserClaims(contactId, companyId, roleId, territoryId, claimsIdentity, false);
            }
           
        }

        /// <summary>
        /// Identity provider as supplied by ACS
        /// </summary>
        public const string IdentityProviderClaimType = @"http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider";

    }
}
