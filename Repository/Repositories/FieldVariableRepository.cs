using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Field Variable Repository
    /// </summary>
    public class FieldVariableRepository : BaseRepository<FieldVariable>, IFieldVariableRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public FieldVariableRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<FieldVariable> DbSet
        {
            get
            {
                return db.FieldVariables;
            }
        }

        #endregion

        #region Public

        ///// <summary>
        /////Get Activities By Sytem User Id Of Current Month
        ///// </summary>
        //public IEnumerable<FieldVariable> GetActivitiesByUserId(DateTime? startDateTime, DateTime? endDateTime)
        //{
        //    return DbSet.Where(a => a.SystemUserId == LoggedInUserId && a.ActivityStartTime >= startDateTime && a.ActivityEndTime < endDateTime).ToList();
        //}
        #endregion
    }
}
