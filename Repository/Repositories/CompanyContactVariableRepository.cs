
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
    public class CompanyContactVariableRepository : BaseRepository<ScopeVariable>, ICompanyContactVariableRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyContactVariableRepository(IUnityContainer container)
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
        /// Get Company Contact By Contact ID
        /// </summary>
        public IEnumerable<ScopeVariable> GetContactVariableByContactId(long contactId)
        {
            return DbSet.Where(cv => cv.Id == contactId);

        }


        #endregion
    }
}
