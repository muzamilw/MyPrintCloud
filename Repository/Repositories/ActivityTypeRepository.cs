using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Activity Type Repository
    /// </summary>
    public class ActivityTypeRepository : BaseRepository<ActivityType>, IActivityTypeRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ActivityTypeRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ActivityType> DbSet
        {
            get
            {
                return db.ActivityTypes;
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// Get All 
        /// </summary>
        public override IEnumerable<ActivityType> GetAll()
        {
            return DbSet.ToList();
        }
        #endregion
    }
}
