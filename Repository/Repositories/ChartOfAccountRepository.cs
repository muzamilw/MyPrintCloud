using System.Data.Entity;
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
        #endregion
    }
}

