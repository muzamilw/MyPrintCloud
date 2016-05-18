using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.MISServices
{
    public interface IRoleService
    {
        List<Role> GetUserRoles();
        List<AccessRight> GetUserAccessRights();
        Role SaveUserRole(Role role);
        Role GetRoleByUserId(Guid userId);
    }
}
