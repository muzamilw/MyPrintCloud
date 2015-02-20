
using System.Data.Entity;
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
        #endregion
    }
}
