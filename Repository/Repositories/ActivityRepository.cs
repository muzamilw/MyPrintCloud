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
    /// Activity Repository
    /// </summary>
    public class ActivityRepository : BaseRepository<Activity>, IActivityRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ActivityRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Activity> DbSet
        {
            get
            {
                return db.Activities;
            }
        }

        #endregion

        #region Public

        /// <summary>
        ///Get Activities By Sytem User Id
        /// </summary>
        public IEnumerable<Activity> GetActivitiesByUserId()
        {
            return DbSet.Where(a => a.SystemUserId == LoggedInUserId).ToList();
            //return DbSet.ToList();
        }
        #endregion
    }
}
