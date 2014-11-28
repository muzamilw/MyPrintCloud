using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
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
        private readonly ISectionFlagRepository sectionFlagRepository;
        private readonly IWeightUnitRepository weightUnitRepository;
        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public InventoryService(IStockCategoryRepository stockCategoryRepository, IStockSubCategoryRepository stockSubCategoryRepository,
            IStockItemRepository stockItemRepository, ISectionFlagRepository sectionFlagRepository, IWeightUnitRepository weightUnitRepository)
        {
            this.stockCategoryRepository = stockCategoryRepository;
            this.stockSubCategoryRepository = stockSubCategoryRepository;
            this.stockItemRepository = stockItemRepository;
            this.sectionFlagRepository = sectionFlagRepository;
            this.weightUnitRepository = weightUnitRepository;
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
            IEnumerable<SectionFlag> sectionFlags = sectionFlagRepository.GetSectionFlagForInventory();
            IEnumerable<WeightUnit> weightUnits = weightUnitRepository.GetAll();
            IEnumerable<StockItem> stockItems = stockItemRepository.GetStockItems(request).StockItems;
            int totalCount = stockItemRepository.GetStockItems(request).TotalCount;
            foreach (var stockItem in stockItems)
            {
                //Set selected color code
                if (stockItem.FlagID != null && stockItem.FlagID != 0 && sectionFlags != null)
                {
                    SectionFlag sectionFlag = sectionFlags.FirstOrDefault(x => x.SectionFlagId == stockItem.FlagID);
                    if (sectionFlag != null)
                        stockItem.FlagColor = sectionFlag.FlagColor;
                }
                //Set selected unit name
                if (stockItem.ItemWeightSelectedUnit != null && weightUnits != null)
                {
                    WeightUnit weightUnit = weightUnits.FirstOrDefault(x => x.Id == stockItem.ItemWeightSelectedUnit);
                    if (weightUnit != null)
                        stockItem.WeightUnitName = weightUnit.UnitName;
                }
            }


            return new InventorySearchResponse { StockItems = stockItems, TotalCount = totalCount };
        }

        #endregion
    }
}
