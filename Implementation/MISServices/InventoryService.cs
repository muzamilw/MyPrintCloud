﻿using System;
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
        private readonly IPaperBasisAreaRepository paperBasisAreaRepository;
        private readonly ILengthUnitRepository lengthUnitRepository;
        private readonly IRegistrationQuestionRepository registrationQuestionRepository;
        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public InventoryService(IStockCategoryRepository stockCategoryRepository, IStockSubCategoryRepository stockSubCategoryRepository,
            IStockItemRepository stockItemRepository, ISectionFlagRepository sectionFlagRepository, IWeightUnitRepository weightUnitRepository,
            ICompanyRepository companyRepository, IPaperSizeRepository paperSizeRepository, IStockCostAndPriceRepository stockCostAndPriceRepository,
            IPaperBasisAreaRepository paperBasisAreaRepository, ILengthUnitRepository lengthUnitRepository, IRegistrationQuestionRepository registrationQuestionRepository)
        {
            this.stockCategoryRepository = stockCategoryRepository;
            this.stockSubCategoryRepository = stockSubCategoryRepository;
            this.stockItemRepository = stockItemRepository;
            this.sectionFlagRepository = sectionFlagRepository;
            this.weightUnitRepository = weightUnitRepository;
            this.companyRepository = companyRepository;
            this.paperSizeRepository = paperSizeRepository;
            this.stockCostAndPriceRepository = stockCostAndPriceRepository;
            this.paperBasisAreaRepository = paperBasisAreaRepository;
            this.lengthUnitRepository = lengthUnitRepository;
            this.registrationQuestionRepository = registrationQuestionRepository;
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
                LengthUnits = lengthUnitRepository.GetAll(),
                PaperBasisAreas = paperBasisAreaRepository.GetAll(),
                RegistrationQuestions = registrationQuestionRepository.GetAll(),
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

            if (stockItem != null && stockItem.StockItemId > 0)
            {
                return UpdateStockItem(stockItem);
            }
            else
            {
                return SaveStockItem(stockItem);
            }
        }

        /// <summary>
        /// Save Stock Item
        /// </summary>
        private StockItem SaveStockItem(StockItem stockItem)
        {
            stockItem.StockCreated = DateTime.Now;
            stockItem.LastModifiedDateTime = DateTime.Now;
            stockItem.OrganisationId = stockItemRepository.OrganisationId;
            stockItemRepository.Add(stockItem);
            stockItemRepository.SaveChanges();
            //After save item content for list view
            return StockItemDeatilForListView(stockItem.StockItemId);
        }

        /// <summary>
        /// Update Stock Item
        /// </summary>
        private StockItem UpdateStockItem(StockItem stockItem)
        {
            StockItem stockItemDbVersion = stockItemRepository.Find(stockItem.StockItemId);
            stockItemDbVersion.ItemName = stockItem.ItemName;
            stockItemDbVersion.ItemCode = stockItem.ItemCode;
            stockItemDbVersion.SupplierId = stockItem.SupplierId;
            stockItemDbVersion.SubCategoryId = stockItem.SubCategoryId;
            stockItemDbVersion.BarCode = stockItem.BarCode;
            stockItemDbVersion.inStock = stockItem.inStock;
            stockItemDbVersion.ItemDescription = stockItem.ItemDescription;
            stockItemDbVersion.FlagID = stockItem.FlagID;
            stockItemDbVersion.Status = stockItem.Status;
            stockItemDbVersion.isDisabled = stockItem.isDisabled;
            stockItemDbVersion.ItemSizeSelectedUnit = stockItem.ItemSizeSelectedUnit;
            stockItemDbVersion.PerQtyQty = stockItem.PerQtyQty;
            stockItemDbVersion.ItemSizeCustom = stockItem.ItemSizeCustom;
            stockItemDbVersion.StockLocation = stockItem.StockLocation;
            stockItemDbVersion.ItemSizeId = stockItem.ItemSizeId;
            stockItemDbVersion.ItemSizeHeight = stockItem.ItemSizeHeight;
            stockItemDbVersion.ItemSizeWidth = stockItem.ItemSizeWidth;
            stockItemDbVersion.PerQtyType = stockItem.PerQtyType;
            stockItemDbVersion.PackageQty = stockItem.PackageQty;
            stockItemDbVersion.RollWidth = stockItem.RollWidth;
            stockItemDbVersion.RollLength = stockItem.RollLength;
            stockItemDbVersion.ReOrderLevel = stockItem.ReOrderLevel;
            stockItemDbVersion.ReorderQty = stockItem.ReorderQty;
            stockItemDbVersion.ItemWeight = stockItem.ItemWeight;
            stockItemDbVersion.ItemColour = stockItem.ItemColour;
            stockItemDbVersion.InkAbsorption = stockItem.InkAbsorption;
            stockItemDbVersion.ItemCoated = stockItem.ItemCoated;
            stockItemDbVersion.PaperBasicAreaId = stockItem.PaperBasicAreaId;
            stockItemDbVersion.ItemCoatedType = stockItem.ItemCoatedType;
            stockItemDbVersion.ItemWeightSelectedUnit = stockItem.ItemWeightSelectedUnit;
            stockItemDbVersion.LastModifiedDateTime = DateTime.Now;
            UpdateStockCostAndPrice(stockItem, stockItemDbVersion);

            return StockItemDeatilForListView(stockItem.StockItemId);
        }

        /// <summary>
        /// Update Stock Cost And Price
        /// </summary>
        private void UpdateStockCostAndPrice(StockItem stockItem, StockItem stockItemDbVersion)
        {

            if (stockItem.StockCostAndPrices != null)
            {
                foreach (var item in stockItem.StockCostAndPrices)
                {
                    //In case of added new Stock cost and Price
                    if (
                        stockItemDbVersion.StockCostAndPrices.All(
                            x =>
                                x.CostPriceId != item.CostPriceId ||
                                item.CostPriceId == 0))
                    {
                        item.ItemId = stockItem.StockItemId;
                        stockCostAndPriceRepository.Add(item);
                        stockCostAndPriceRepository.SaveChanges();
                    }
                    else
                    {
                        //In case of Stock Cost And Price Updated

                        StockCostAndPrice dbStockCostAndPriceItem =
                            stockItemDbVersion.StockCostAndPrices.First(x => x.CostPriceId == item.CostPriceId);

                        if (dbStockCostAndPriceItem != null)
                        {
                            if (dbStockCostAndPriceItem.CostPrice != item.CostPrice || dbStockCostAndPriceItem.FromDate != item.FromDate
                                || dbStockCostAndPriceItem.ToDate != item.ToDate)
                            {
                                dbStockCostAndPriceItem.CostPrice = item.CostPrice;
                                dbStockCostAndPriceItem.FromDate = item.FromDate;
                                dbStockCostAndPriceItem.ToDate = item.ToDate;
                            }
                        }
                    }
                }
            }
            stockItemRepository.SaveChanges();
            //find missing items
            List<StockCostAndPrice> missingStockCostAndPriceListItems = new List<StockCostAndPrice>();
            foreach (StockCostAndPrice dbversionStockCostAndPriceItem in stockItemDbVersion.StockCostAndPrices)
            {
                if (stockItem.StockCostAndPrices != null && stockItem.StockCostAndPrices.All(x => x.CostPriceId != dbversionStockCostAndPriceItem.CostPriceId))
                {
                    missingStockCostAndPriceListItems.Add(dbversionStockCostAndPriceItem);
                }
                //In case user delete all Stock Cost And Price items from client side then it delete all items from db
                if (stockItem.StockCostAndPrices == null)
                {
                    missingStockCostAndPriceListItems.Add(dbversionStockCostAndPriceItem);
                }
            }
            //remove missing items
            foreach (StockCostAndPrice missingStockCostAndPriceItem in missingStockCostAndPriceListItems)
            {
                StockCostAndPrice dbVersionMissingItem = stockItemDbVersion.StockCostAndPrices.First(x => x.CostPriceId == missingStockCostAndPriceItem.CostPriceId);
                if (dbVersionMissingItem.CostPriceId > 0)
                {
                    stockCostAndPriceRepository.Delete(dbVersionMissingItem);
                    stockCostAndPriceRepository.SaveChanges();
                }
            }
        }
        /// <summary>
        /// After Add/Edit return stock item detail contents for list view
        /// </summary>
        private StockItem StockItemDeatilForListView(long stockItemId)
        {
            StockItem stockItem = stockItemRepository.Find(stockItemId);
            //Set selected color code
            if (stockItem.FlagID != null && stockItem.FlagID != 0)
            {
                long flagId = Convert.ToInt64(stockItem.FlagID ?? 0);
                SectionFlag sectionFlag = sectionFlagRepository.Find(flagId);
                if (sectionFlag != null)
                    stockItem.FlagColor = sectionFlag.FlagColor;
            }
            //Set selected unit name
            if (stockItem.ItemWeightSelectedUnit != null && stockItem.ItemWeightSelectedUnit != 0)
            {
                long selectedWeightUnit = Convert.ToInt64(stockItem.ItemWeightSelectedUnit ?? 0);
                WeightUnit weightUnit = weightUnitRepository.Find(selectedWeightUnit);
                if (weightUnit != null)
                    stockItem.WeightUnitName = weightUnit.UnitName;
            }
            //Set Supplier Company Name
            if (stockItem.SupplierId != null && stockItem.SupplierId != 0)
            {
                long supplierId = Convert.ToInt64(stockItem.SupplierId ?? 0);
                if (supplierId != 0)
                {
                    stockItem.SupplierCompanyName = companyRepository.Find(supplierId).Name;
                }
            }
            return stockItem;
        }

        /// <summary>
        ///Find Stock Item By Id 
        /// </summary>
        public StockItem GetById(long stockItemId)
        {
            return stockItemRepository.Find(stockItemId);
        }
        #endregion
    }
}
