using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

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

    }
}
