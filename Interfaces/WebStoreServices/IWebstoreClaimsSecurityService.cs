
using System.Security.Claims;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.WebStoreServices
{
    public interface IWebstoreClaimsSecurityService
    {
        void AddSignInClaimsToIdentity(long contactId, long companyId, int roleId, long territoryId,
            ClaimsIdentity claimsIdentity, bool? HasUserDamRights);
    }
}
