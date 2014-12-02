using System.Linq;
using MPC.MIS.Areas.Api.Models;
using MPC.MIS.ModelMappers;
using InventoryBaseResponse = MPC.MIS.Areas.Api.Models.InventoryBaseResponse;
using DomainModels = MPC.Models.DomainModels;

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
                StockCategories = source.StockCategories.Select(s => s.CreateFromDropDown()).ToList(),
                StockSubCategories = source.StockSubCategories.Select(su => su.CreateFromDropDown()).ToList(),
                PaperSizes = source.PaperSizes.Select(su => su.CreateFromDropDown()).ToList(),
                SectionFlags = source.SectionFlags.Select(su => su.CreateFromDropDown()).ToList(),
                WeightUnits = source.WeightUnits.Select(su => su.CreateFromDropDown()).ToList(),
                StockCostAndPrice = source.StockCostAndPrice.CreateFrom(),
                LengthUnits = source.LengthUnits.Select(ul => ul.CreateFromDropDown()).ToList(),
                PaperBasisAreas = source.PaperBasisAreas.Select(p => p.CreateFromDropDown()).ToList(),
                RegistrationQuestions = source.RegistrationQuestions.Select(q => q.CreateFromDropDown()),
            };
        }

        #endregion

        #region Public

        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static InventorySearchResponse CreateFrom(this MPC.Models.ResponseModels.InventorySearchResponse source)
        {
            return new InventorySearchResponse
            {
                StockItems = source.StockItems.Select(stockItem => stockItem.CreateFrom()).ToList(),
                TotalCount = source.TotalCount
            };
        }
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static StockItemForListView CreateFrom(this DomainModels.StockItem source)
        {
            return new StockItemForListView
            {
                StockItemId = source.StockItemId,
                ItemName = source.ItemName,
                ItemWeight = source.ItemWeight,
                CategoryName = source.StockCategory != null ? source.StockCategory.Name : string.Empty,
                FullCategoryName = (source.StockCategory != null ? source.StockCategory.Name : string.Empty) + (source.StockSubCategory != null ? "  ( " + source.StockSubCategory.Name + " )" : string.Empty),
                SubCategoryName = source.StockSubCategory != null ? source.StockSubCategory.Name : string.Empty,
                WeightUnitName = source.WeightUnitName,
                PerQtyQty = source.PerQtyQty,
                FlagColor = source.FlagColor,
                SupplierCompanyName = source.SupplierCompanyName,
            };
        }

        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainModels.StockItem CreateFrom(this StockItem source)
        {
            return new DomainModels.StockItem
            {
                StockItemId = source.StockItemId,
                ItemName = source.ItemName,
                ItemWeight = source.ItemWeight,
                PerQtyQty = source.PerQtyQty,

            };
        }
        #endregion

    }
}