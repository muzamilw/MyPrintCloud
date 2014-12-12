using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MPC.Common;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;

namespace MPC.Implementation.WebStoreServices
{
    public sealed class WebstoreClaimsSecurityService : MarshalByRefObject, IWebstoreClaimsSecurityService
    {
        private static void AddOrganisationClaims(long organisationId, ClaimsIdentity claimsIdentity)
        {
            Claim claim = new Claim(WebstoreClaimTypes.OrganisationId,
                                        ClaimHelper.Serialize(
                                            new OrganisationClaimValue { OrganisationId = organisationId }),
                                        typeof(OrganisationClaimValue).AssemblyQualifiedName);
            claimsIdentity.AddClaim(claim);
        }

     

        private static void AddCompanyClaims(long companyId, ClaimsIdentity claimsIdentity)
        {
            Claim claim = new Claim(WebstoreClaimTypes.Company,
                                        ClaimHelper.Serialize(
                                            new CompanyClaimValue { CompanyId = companyId }),
                                        typeof(CompanyClaimValue).AssemblyQualifiedName);
            claimsIdentity.AddClaim(claim);
        }

        private static void AddLoginUserClaims(CompanyContact userContact, ClaimsIdentity claimsIdentity)
        {
            Claim claim = new Claim(WebstoreClaimTypes.LoginUser,
                                        ClaimHelper.Serialize(
                                            new OrganisationClaimValue { loginContact = userContact }),
                                        typeof(OrganisationClaimValue).AssemblyQualifiedName);
            claimsIdentity.AddClaim(claim);
        }

        public void AddClaimsToIdentity(long companyIdentity, CompanyContact contact ,ClaimsIdentity claimsIdentity)
        {
            if (companyIdentity > 0)
            {
                AddCompanyClaims(companyIdentity, claimsIdentity);
            }
            if (contact != null)
            {
                AddLoginUserClaims(contact, claimsIdentity);
            }
        }

        /// <summary>
        /// Identity provider as supplied by ACS
        /// </summary>
        public const string IdentityProviderClaimType = @"http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider";

    }
}
