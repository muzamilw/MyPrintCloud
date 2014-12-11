using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Common;
using MPC.Interfaces.Data;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;

namespace MPC.Implementation.WebStoreServices
{
    public sealed class WebstoreClaimsHelperService : IWebstoreClaimsHelperService
    {
   
        public long OrganisationId()
        {
            IList<OrganisationClaimValue> roles = ClaimHelper.GetClaimsByType<OrganisationClaimValue>(WebstoreClaimTypes.OrganisationId);
            return roles.Select(role => role.OrganisationId).FirstOrDefault();
        }

        public long CompanyId()
        {
            IList<CompanyClaimValue> roles = ClaimHelper.GetClaimsByType<CompanyClaimValue>(WebstoreClaimTypes.Company);
            return roles.Select(role => role.CompanyId).FirstOrDefault();
        }
    }
}
