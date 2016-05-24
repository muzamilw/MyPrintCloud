using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public RoleRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Role> DbSet
        {
            get
            {
                return db.Roles;
            }
        }

        #endregion
        #region Public
        /// <summary>
        /// Get All System Users of Organisation
        /// </summary>
        public override IEnumerable<Role> GetAll()
        {
            
            return DbSet.Where(a => a.OrganisationId == OrganisationId).ToList();
        }
        
       public List<Role> GetUserRoles()
        {
            return DbSet.Where(o => o.OrganisationId == OrganisationId).ToList();
        }
        public List<AccessRight> GetUserAccessRights()
        {
            return db.AccessRights.ToList();
        }

        public Role GetRoleById(int roleId)
        {
            return DbSet.FirstOrDefault(r => r.RoleId == roleId);
        }

        public void DeleteRoleRights(Role role)
        {
            role.Rolerights.ToList().ForEach(a => db.Rolerights.Remove(a));
        }

        public Role GetRoleByUserId(Guid userId)
        {
            var user = db.SystemUsers.FirstOrDefault(a => a.SystemUserId == userId);
            if (user != null)
            {
                return DbSet.FirstOrDefault(a => a.RoleId == user.RoleId);
            }
            else
            {
                return null;
            }
        }

        public Role GetSystemAdminRole()
        {
            return DbSet.FirstOrDefault(a => a.IsCompanyLevel == 1);
        }
        
        #endregion
    }
}
