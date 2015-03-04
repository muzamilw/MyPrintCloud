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
        ///Get Activities By Sytem User Id Of Current Month
        /// </summary>
        public IEnumerable<Activity> GetActivitiesByUserId(DateTime? startDateTime, DateTime? endDateTime)
        {
            
            return DbSet.Where(a => a.SystemUserId == LoggedInUserId && a.ActivityStartTime >= startDateTime && a.ActivityEndTime < endDateTime).ToList();
        }
        #endregion
    }
}
