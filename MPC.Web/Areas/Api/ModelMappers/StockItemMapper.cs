﻿using System.Collections.Generic;
using System.Linq;
using InventoryBaseResponse = MPC.MIS.Areas.Api.Models.InventoryBaseResponse;
using DomainModels = MPC.Models.DomainModels;
using ApiModels = MPC.MIS.Areas.Api.Models;
using DomainResponseModel = MPC.Models.ResponseModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// 
    /// </summary>
    public static class StockItemMapper
    {
        #region Base Reposne Mapper
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static InventoryBaseResponse CreateFrom(this MPC.Models.ResponseModels.InventoryBaseResponse source)
        {
            return new InventoryBaseResponse
            {
                StockCategories = source.StockCategories != null ? source.StockCategories.Select(s => s.CreateFromDropDown()).ToList() : null,
                StockSubCategories = source.StockSubCategories != null ? source.StockSubCategories.Select(su => su.CreateFromDropDown()).ToList() : null,
                PaperSizes = source.PaperSizes != null ? source.PaperSizes.Select(su => su.CreateFromDropDown()).ToList() : null,
                SectionFlags = source.SectionFlags != null ? source.SectionFlags.Select(su => su.CreateFromDropDown()).ToList() : null,
                WeightUnits = source.WeightUnits != null ? source.WeightUnits.Select(su => su.CreateFromDropDown()).ToList() : null,
                LengthUnits = source.LengthUnits != null ? source.LengthUnits.Select(ul => ul.CreateFromDropDown()).ToList() : null,
                PaperBasisAreas = source.PaperBasisAreas != null ? source.PaperBasisAreas.Select(p => p.CreateFromDropDown()).ToList() : null,
                RegistrationQuestions = source.RegistrationQuestions != null ? source.RegistrationQuestions.Select(q => q.CreateFromDropDown()) : new List<ApiModels.RegistrationQuestionDropDown>()
            };
        }

        #endregion

        #region Public

        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ApiModels.InventorySearchResponse CreateFrom(this MPC.Models.ResponseModels.InventorySearchResponse source)
        {
            return new ApiModels.InventorySearchResponse
            {
                StockItems = source.StockItems != null ? source.StockItems.Select(stockItem => stockItem.CreateFrom()).ToList() : null,
                TotalCount = source.TotalCount
            };
        }

        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ApiModels.SupplierSearchResponseForInventory CreateFrom(this DomainResponseModel.SupplierSearchResponseForInventory source)
        {
            return new ApiModels.SupplierSearchResponseForInventory
            {
                Suppliers = source.Suppliers != null ? source.Suppliers.Select(s => s.CreateFromForInventory()).ToList() : null,
                TotalCount = source.TotalCount
            };
        }

        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ApiModels.StockItemForListView CreateFrom(this DomainModels.StockItem source)
        {
            return new ApiModels.StockItemForListView
            {
                StockItemId = source.StockItemId,
                ItemName = source.ItemName,
                ItemWeight = source.ItemWeight,
                ItemDescription = source.ItemDescription,
                CategoryName = source.StockCategory != null ? source.StockCategory.Name : string.Empty,
                FullCategoryName = (source.StockCategory != null ? source.StockCategory.Name : string.Empty) + (source.StockSubCategory != null ? "  ( " + source.StockSubCategory.Name + " )" : string.Empty),
                SubCategoryName = source.StockSubCategory != null ? source.StockSubCategory.Name : string.Empty,
                WeightUnitName = source.WeightUnitName,
                PerQtyQty = source.PerQtyQty,
                PerQtyType = source.PerQtyType,
                FlagColor = source.FlagColor,
                SupplierCompanyName = source.SupplierCompanyName,
            };

        }

        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainModels.StockItem CreateFrom(this ApiModels.StockItem source)
        {
            return new DomainModels.StockItem
            {
                StockItemId = source.StockItemId,
                ItemName = source.ItemName,
                ItemCode = source.ItemCode,
                SupplierId = source.SupplierId,
                CategoryId = source.CategoryId,
                SubCategoryId = source.SubCategoryId,
                BarCode = source.BarCode,
                inStock = source.inStock,
                ItemDescription = source.ItemDescription,
                StockCreated = source.StockCreated,
                FlagID = source.FlagID,
                Status = source.Status,
                isDisabled = source.isDisabled,
                PaperType = source.PaperType,
                ItemSizeSelectedUnit = source.ItemSizeSelectedUnit,
                PerQtyQty = source.PerQtyQty,
                ItemSizeCustom = source.ItemSizeCustom,
                StockLocation = source.StockLocation,
                ItemSizeId = source.ItemSizeId,
                ItemSizeHeight = source.ItemSizeHeight,
                ItemSizeWidth = source.ItemSizeWidth,
                PerQtyType = source.PerQtyType,
                PackageQty = source.PackageQty,
                RollWidth = source.RollWidth,
                RollLength = source.RollLength,
                ReOrderLevel = source.ReOrderLevel,
                ReorderQty = source.ReorderQty,
                ItemWeight = source.ItemWeight,
                ItemColour = source.ItemColour,
                InkAbsorption = source.InkAbsorption,
                ItemCoated = source.ItemCoated,
                PaperBasicAreaId = source.PaperBasicAreaId,
                ItemCoatedType = source.ItemCoatedType,
                ItemWeightSelectedUnit = source.ItemWeightSelectedUnit,
                StockCostAndPrices = source.StockCostAndPrices != null ? source.StockCostAndPrices.Select(cp => cp.CreateFrom()).ToList() : null
            };
        }
        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static ApiModels.StockItem CreateFromDetail(this DomainModels.StockItem source)
        {
            return new ApiModels.StockItem
            {
                StockItemId = source.StockItemId,
                ItemName = source.ItemName,
                ItemCode = source.ItemCode,
                SupplierId = source.SupplierId,
                CategoryId = source.CategoryId,
                SubCategoryId = source.SubCategoryId,
                BarCode = source.BarCode,
                inStock = source.inStock,
                ItemDescription = source.ItemDescription,
                StockCreated = source.StockCreated,
                FlagID = source.FlagID,
                Status = source.Status,
                isDisabled = source.isDisabled,
                PaperType = source.PaperType,
                ItemSizeSelectedUnit = source.ItemSizeSelectedUnit,
                PerQtyQty = source.PerQtyQty,
                ItemSizeCustom = source.ItemSizeCustom,
                StockLocation = source.StockLocation,
                ItemSizeId = source.ItemSizeId,
                ItemSizeHeight = source.ItemSizeHeight,
                ItemSizeWidth = source.ItemSizeWidth,
                PerQtyType = source.PerQtyType,
                PackageQty = source.PackageQty,
                RollWidth = source.RollWidth,
                RollLength = source.RollLength,
                ReOrderLevel = source.ReOrderLevel,
                ReorderQty = source.ReorderQty,
                ItemWeight = source.ItemWeight,
                ItemColour = source.ItemColour,
                InkAbsorption = source.InkAbsorption,
                ItemCoated = source.ItemCoated,
                PaperBasicAreaId = source.PaperBasicAreaId,
                ItemCoatedType = source.ItemCoatedType,
                ItemWeightSelectedUnit = source.ItemWeightSelectedUnit,
                StockCostAndPrices = source.StockCostAndPrices != null ? source.StockCostAndPrices.Select(cp => cp.CreateFrom()).ToList() : null
            };
        }
        #endregion

    }
}