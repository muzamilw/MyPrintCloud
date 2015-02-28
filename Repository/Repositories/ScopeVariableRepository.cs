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
    /// Company Contact Variable Repository
    /// </summary>
    public class ScopeVariableRepository : BaseRepository<ScopeVariable>, IScopeVariableRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ScopeVariableRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ScopeVariable> DbSet
        {
            get
            {
                return db.ScopeVariables;
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// Get Scope Variable By Contact ID Anb Scope Type
        /// </summary>
        public IEnumerable<ScopeVariable> GetContactVariableByContactId(long contactId, int scope)
        {
            return DbSet.Where(cv => cv.Id == contactId && cv.Scope == scope);

        }


        #endregion
    }
}
