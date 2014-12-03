using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Stock Cost And Price Repository
    /// </summary>
    public interface IStockCostAndPriceRepository : IBaseRepository<StockCostAndPrice, long>
    {
        /// <summary>
        /// Get Deafault Stock Cost And Pricefor 
        /// </summary>
        StockCostAndPrice GetDefaultStockCostAndPrice();

    }
}
