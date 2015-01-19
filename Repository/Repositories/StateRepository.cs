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
    /// State Repository 
    /// </summary>
    public class StateRepository : BaseRepository<State>, IStateRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public StateRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<State> DbSet
        {
            get
            {
                return db.States;
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// Get All States for Current Organisation
        /// </summary>
        public override IEnumerable<State> GetAll()
        {
            return DbSet.ToList();
        }

        /// <summary>
        /// Gets the Name of the state by its id
        /// </summary>
        /// <param name="StateId"></param>
        /// <returns></returns>
        public string GetStateNameById(long StateId) 
        {
            return db.States.Where(s => s.StateId == StateId).Select(n => n.StateName).FirstOrDefault();
        }

        #endregion
    }
}
