using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    public interface ISystemUserRepository : IBaseRepository<SystemUser, long>
    {
        IEnumerable<SystemUser> GetAll();
        SystemUser GetUserrById(System.Guid SytemUserId);

        List<SystemUser> GetSystemUSersByOrganisationID(long OrganisationID);
        bool Update(System.Guid Id, string Email, string FullName, int status, string EmailSignature, string EstimateHeadNotes, string EstimateFootNotes);
        bool Add(System.Guid Id, string Email, string FullName, int OrganizationId);
        
    }
}
