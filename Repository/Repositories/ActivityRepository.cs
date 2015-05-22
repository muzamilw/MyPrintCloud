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
        ///Get Activities By Duration
        /// </summary>
        public IEnumerable<Activity> GetActivitiesByUserId(Guid userId, DateTime? startDateTime, DateTime? endDateTime)
        {
            if (userId.ToString() == "00000000-0000-0000-0000-000000000000")
            {
                return DbSet.Where(a => a.ActivityStartTime >= startDateTime && a.ActivityEndTime < endDateTime).ToList();
            }
            else
            {
                return DbSet.Where(a => a.SystemUserId == userId && a.ActivityStartTime >= startDateTime && a.ActivityEndTime < endDateTime).ToList();
            }
            
        }
        #endregion
    }
}
