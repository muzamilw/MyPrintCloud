
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
    public class CompanyContactVariableRepository : BaseRepository<CompanyContactVariable>, ICompanyContactVariableRepository
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
        protected override IDbSet<CompanyContactVariable> DbSet
        {
            get
            {
                return db.CompanyContactVariables;
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// Get Company Contact By Contact ID
        /// </summary>
        public IEnumerable<CompanyContactVariable> GetContactVariableByContactId(long contactId)
        {
            return DbSet.Where(cv => cv.ContactId == contactId);

        }


        #endregion
    }
}
