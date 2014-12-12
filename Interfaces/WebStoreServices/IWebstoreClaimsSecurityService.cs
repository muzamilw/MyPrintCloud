
using System.Security.Claims;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.WebStoreServices
{
    public interface IWebstoreClaimsSecurityService
    {
        void AddClaimsToIdentity(long companyIdentity, CompanyContact contact, ClaimsIdentity claimsIdentity);
    }
}
