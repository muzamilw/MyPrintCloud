using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System.Collections.Generic;
using System.Linq;
namespace MPC.Repository.Repositories
{
    public class CompanyContactRoleRepository : BaseRepository<CompanyContactRole>, ICompanyContactRoleRepository
    {
        public CompanyContactRoleRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<CompanyContactRole> DbSet
        {
            get
            {
                return db.CompanyContactRoles;
            }
        }
        public List<CompanyContactRole> GetContactRolesExceptAdmin(int AdminRole)
        {
            return db.CompanyContactRoles.Where(c => c.ContactRoleId != AdminRole).ToList();
        }

        public List<CompanyContactRole> GetAllContactRoles()
        {
            return db.CompanyContactRoles.ToList();
        }
        public CompanyContactRole GetRoleByID(int RoleID)
        {
            return db.CompanyContactRoles.Where(i => i.ContactRoleId == RoleID).FirstOrDefault();
        }
    }
}
