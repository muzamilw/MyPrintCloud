using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Activity Repository Interface
    /// </summary>
    public interface IActivityRepository : IBaseRepository<Activity, long>
    {
        /// <summary>
        ///Get Activities By Sytem User Id
        /// </summary>
        IEnumerable<Activity> GetActivitiesByUserId();
    }
}
