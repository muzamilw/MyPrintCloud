using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;

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
            return DbSet.OrderBy(s => s.StateName).ToList();
        }

        public List<State> GetStates()
        {
            try
            {
                return db.States.OrderBy(s => s.StateName).ToList();

            }
            catch(Exception ex)
            {
                throw ex;

            }
        }
        public State GetStateFromStateID(long StateID)
        {
            State State = null;
            
            State = db.States.Where(i => i.StateId == StateID).FirstOrDefault();


           
            return State;

        }
        public string GetStateNameById(long StateId)
        {
            try
            {
                return db.States.Where(s => s.StateId == StateId).Select(c => c.StateName).FirstOrDefault();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public string GetStateCodeById(long stateId)
        {

            try
            {

                return db.States.Where(a => a.StateId == stateId).Select(c => c.StateCode).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public State GetStateByName(string sStateName)
        {
            return DbSet.FirstOrDefault(s => s.StateName == sStateName);
        }
        #endregion
    }
}
