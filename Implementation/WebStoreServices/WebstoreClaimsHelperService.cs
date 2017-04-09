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
using System.Security.Claims;
using System.Threading;

namespace MPC.Implementation.WebStoreServices
{
    public sealed class WebstoreClaimsHelperService : IWebstoreClaimsHelperService
    {
        public bool loginContactDAMRights()
        {
            IList<ContactClaimValue> roles = ClaimHelper.GetClaimsByType<ContactClaimValue>(WebstoreClaimTypes.LoginUser);
            return roles.Select(role => role.HasUserDamRights).FirstOrDefault();
        }
        public long loginContactID()
        {
            
            IList<ContactClaimValue> roles = ClaimHelper.GetClaimsByType<ContactClaimValue>(WebstoreClaimTypes.LoginUser);
            long contactID = roles.Select(role => role.ContactId).FirstOrDefault();
            return contactID;
        }
        public long loginContactCompanyID()
        {
            IList<ContactClaimValue> roles = ClaimHelper.GetClaimsByType<ContactClaimValue>(WebstoreClaimTypes.LoginUser);
            long CompanyId = roles.Select(role => role.ContactCompanyId).FirstOrDefault();
            return CompanyId;
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
        public bool isUserLoggedIn()
        {
           
            IList<ContactClaimValue> roles = ClaimHelper.GetClaimsByType<ContactClaimValue>(WebstoreClaimTypes.LoginUser);
            long contactId = roles.Select(role => role.ContactId).FirstOrDefault();
            if (contactId > 0)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
        public bool removeAuthenticationClaim()
        {
            return ClaimHelper.RemoveClaim(WebstoreClaimTypes.LoginUser);
        }
    }
}
