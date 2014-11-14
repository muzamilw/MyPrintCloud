using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Tax Rate Repository
    /// </summary>
    public class TaxRateRepository : BaseRepository<TaxRate>, ITaxRateRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public TaxRateRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<TaxRate> DbSet
        {
            get
            {
                return db.TaxRates;
            }
        }
        
        #endregion

    }
}

