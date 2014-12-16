using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Common;
using MPC.Interfaces.Data;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;

namespace MPC.Implementation.WebStoreServices
{
    public sealed class WebstoreClaimsHelperService : IWebstoreClaimsHelperService
    {
        public long loginContactID()
        {
            IList<ContactClaimValue> roles = ClaimHelper.GetClaimsByType<ContactClaimValue>(WebstoreClaimTypes.LoginUser);
            return roles.Select(role => role.ContactId).FirstOrDefault();
        }
        public long loginContactCompanyID()
        {
            IList<ContactClaimValue> roles = ClaimHelper.GetClaimsByType<ContactClaimValue>(WebstoreClaimTypes.LoginUser);
            return roles.Select(role => role.ContactCompanyId).FirstOrDefault();
        }
        public long loginContactRoleID()
        {
            IList<ContactClaimValue> roles = ClaimHelper.GetClaimsByType<ContactClaimValue>(WebstoreClaimTypes.LoginUser);
            return roles.Select(role => role.ContactRoleId).FirstOrDefault();
        }
        public long loginContactTerritoryID()
        {
            IList<ContactClaimValue> roles = ClaimHelper.GetClaimsByType<ContactClaimValue>(WebstoreClaimTypes.LoginUser);
            return roles.Select(role => role.ContactTerritoryId).FirstOrDefault();
        }
    }
}
