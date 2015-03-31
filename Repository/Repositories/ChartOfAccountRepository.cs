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
    /// Chart Of Account Repository
    /// </summary>
    public class ChartOfAccountRepository : BaseRepository<ChartOfAccount>, IChartOfAccountRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ChartOfAccountRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ChartOfAccount> DbSet
        {
            get
            {
                return db.ChartOfAccounts;
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// Get All Chart Of Account By Organisation
        /// </summary>
        public override IEnumerable<ChartOfAccount> GetAll()
        {
            return DbSet.Where(chartOfAccount => chartOfAccount.UserDomainKey == OrganisationId).ToList();
        }
        #endregion
    }
}

