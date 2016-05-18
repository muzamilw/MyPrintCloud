using System.Collections.ObjectModel;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
using EnumerableExtensions = GrapeCity.Viewer.Common.EnumerableExtensions;

namespace MPC.Implementation.MISServices
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            this._roleRepository = roleRepository;
        }

        public List<Role> GetUserRoles()
        {
            return _roleRepository.GetUserRoles();
        }

        public List<AccessRight> GetUserAccessRights()
        {
            return _roleRepository.GetUserAccessRights();
        }

        public Role SaveUserRole(Role role)
        {
            var dbRole = _roleRepository.GetRoleById(role.RoleId)?? CreateNewRole();
            dbRole.RoleName = role.RoleName;
            dbRole.RoleDescription = role.RoleDescription;
            dbRole.IsCompanyLevel = role.IsCompanyLevel;
            _roleRepository.DeleteRoleRights(dbRole);
            
            if(role.Rolerights != null)
                role.Rolerights.ToList().ForEach(a => dbRole.Rolerights.Add(a));
            _roleRepository.SaveChanges();
            return dbRole;
        }
        
        private Role CreateNewRole()
        {
            Role newRole = _roleRepository.Create();
            newRole.OrganisationId = _roleRepository.OrganisationId;
            newRole.Rolerights = new Collection<Roleright>();
            _roleRepository.Add(newRole);
            return newRole;
        }

        public Role GetRoleByUserId(Guid userId)
        {
            return _roleRepository.GetRoleByUserId(userId);
        }
    }
}
