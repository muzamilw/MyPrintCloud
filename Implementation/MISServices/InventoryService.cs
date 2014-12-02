using System;
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
        private readonly ICompanyRepository companyRepository;
        private readonly IPaperSizeRepository paperSizeRepository;
        private readonly IStockCostAndPriceRepository stockCostAndPriceRepository;
        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public InventoryService(IStockCategoryRepository stockCategoryRepository, IStockSubCategoryRepository stockSubCategoryRepository,
            IStockItemRepository stockItemRepository, ISectionFlagRepository sectionFlagRepository, IWeightUnitRepository weightUnitRepository,
            ICompanyRepository companyRepository, IPaperSizeRepository paperSizeRepository,IStockCostAndPriceRepository stockCostAndPriceRepository)
        {
            this.stockCategoryRepository = stockCategoryRepository;
            this.stockSubCategoryRepository = stockSubCategoryRepository;
            this.stockItemRepository = stockItemRepository;
            this.sectionFlagRepository = sectionFlagRepository;
            this.weightUnitRepository = weightUnitRepository;
            this.companyRepository = companyRepository;
            this.paperSizeRepository = paperSizeRepository;
            this.stockCostAndPriceRepository = stockCostAndPriceRepository;
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
                PaperSizes = paperSizeRepository.GetAll(),
                SectionFlags = sectionFlagRepository.GetSectionFlagForInventory(),
                WeightUnits = weightUnitRepository.GetAll(),
                StockCostAndPrice = stockCostAndPriceRepository.GetDefaultStockCostAndPrice(),
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
                //Set Supplier Company Name
                if (stockItem.SupplierId != null)
                {
                    long supplierId = Convert.ToInt64(stockItem.SupplierId ?? 0);
                    if (supplierId != 0)
                    {
                        stockItem.SupplierCompanyName = companyRepository.Find(supplierId).Name;
                    }
                }
            }


            return new InventorySearchResponse { StockItems = stockItems, TotalCount = totalCount };
        }

        /// <summary>
        /// Add/Update Stock Item
        /// </summary>
        public StockItem SaveInevntory(StockItem stockItem)
        {
            StockItem stockItemDbVersion = stockItemRepository.Find(stockItem.StockItemId);
            if (stockItemDbVersion == null)
            {
                saveStockItem(stockItem);
            }
            else
            {
                updateStockItem(stockItem);
            }
            return null;
        }

        /// <summary>
        /// Save Stock Item
        /// </summary>
        private StockItem saveStockItem(StockItem stockItem)
        {
            return null;
        }

        /// <summary>
        /// Update Stock Item
        /// </summary>
        private StockItem updateStockItem(StockItem stockItem)
        {
            return null;
        }
        #endregion
    }
}
