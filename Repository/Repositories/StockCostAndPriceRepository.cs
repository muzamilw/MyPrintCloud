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
    /// Stock Cost And Price Repository
    /// </summary>
    public class StockCostAndPriceRepository : BaseRepository<StockCostAndPrice>, IStockCostAndPriceRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public StockCostAndPriceRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<StockCostAndPrice> DbSet
        {
            get
            {
                return db.StockCostAndPrices;
            }
        }

        #endregion

        /// <summary>
        /// Get All Stock Cost And Pricefor User Domain Key
        /// </summary>
        public override IEnumerable<StockCostAndPrice> GetAll()
        {
            return DbSet.ToList();
        }

        /// <summary>
        /// Get Deafault Stock Cost And Pricefor 
        /// </summary>
        public StockCostAndPrice GetDefaultStockCostAndPrice()
        {
            return DbSet.FirstOrDefault(scap => scap.CostOrPriceIdentifier == -1);
        }
    }
}
