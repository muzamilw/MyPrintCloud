using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Implementation.MISServices
{
    /// <summary>
    /// Inventory Service
    /// </summary>
    public class InventoryService : IInventoryService
    {
        #region Private

        /// <summary>
        /// Private members
        /// </summary>
        private readonly IStockCategoryRepository stockCategoryRepository;
        private readonly IStockSubCategoryRepository stockSubCategoryRepository;
        private readonly IStockItemRepository stockItemRepository;
        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public InventoryService(IStockCategoryRepository stockCategoryRepository, IStockSubCategoryRepository stockSubCategoryRepository,
            IStockItemRepository stockItemRepository)
        {
            this.stockCategoryRepository = stockCategoryRepository;
            this.stockSubCategoryRepository = stockSubCategoryRepository;
            this.stockItemRepository = stockItemRepository;
        }

        #endregion

        #region Public
        /// <summary>
        /// Load Inventory Base data
        /// </summary>
        public InventoryBaseResponse GetBaseData()
        {
            return new InventoryBaseResponse
            {
                StockCategories = stockCategoryRepository.GetAll(),
                StockSubCategories = stockSubCategoryRepository.GetAll(),
            };
        }

        /// <summary>
        /// Load Stock Items, based on search filters
        /// </summary>
        public InventorySearchResponse LoadStockItems(InventorySearchRequestModel request)
        {
            return stockItemRepository.GetStockItems(request);
        }

        #endregion
    }
}
