using MPC.Models.DomainModels;
using System.Collections.Generic;
namespace MPC.Interfaces.Repository
{
    public interface ICompanyContactRoleRepository : IBaseRepository<CompanyContactRole, long>
    {
        List<CompanyContactRole> GetContactRolesExceptAdmin(int AdminRole);
        List<CompanyContactRole> GetAllContactRoles();
        CompanyContactRole GetRoleByID(int RoleID);
    }
}
