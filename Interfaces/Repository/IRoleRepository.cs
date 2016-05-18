using System;
using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    public interface IRoleRepository : IBaseRepository<Role, long>
    {
        List<Role> GetUserRoles();
        List<AccessRight> GetUserAccessRights();
        Role GetRoleById(int roleId);
        void DeleteRoleRights(Role role);
        Role GetRoleByUserId(Guid userId);

    }
}
