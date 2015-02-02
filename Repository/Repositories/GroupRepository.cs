using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Group Repository
    /// </summary>
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public GroupRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Group> DbSet
        {
            get
            {
                return db.Groups;
            }
        }

        #endregion
    }
}
