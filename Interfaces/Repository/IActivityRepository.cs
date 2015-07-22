using System;
using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Repository Interface
    /// </summary>
      public interface IActivityRepository : IBaseRepository<Activity, long>
    {

        /// <summary>
        ///Get Activities By Sytem User Id
        /// </summary>
        IEnumerable<Activity> GetActivitiesByUserId(Guid userId, DateTime? startDateTime, DateTime? endDateTime);
    }
}
