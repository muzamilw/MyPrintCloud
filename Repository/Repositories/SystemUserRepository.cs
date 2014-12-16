using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    public class SystemUserRepository : BaseRepository<SystemUser>, ISystemUserRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SystemUserRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<SystemUser> DbSet
        {
            get
            {
                return db.SystemUsers;
            }
        }

        #endregion
        #region Public
        /// <summary>
        /// Get All System Users of Organisation
        /// </summary>
        public override IEnumerable<SystemUser> GetAll()
        {
            return DbSet.Where(systemUser => systemUser.OrganizationId == OrganisationId).ToList();
        }
        public SystemUser GetSalesManagerById(long SytemUserId)
        {
            return db.SystemUsers.Where(s => s.SystemUserId == SytemUserId).FirstOrDefault();
        }
        #endregion
    }
}
