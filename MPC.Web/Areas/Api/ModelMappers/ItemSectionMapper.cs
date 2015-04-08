using System.Collections.Generic;
using System.Linq;
using MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.DomainModels;

    /// <summary>
    /// Item Section Mapper
    /// </summary>
    public static class ItemSectionMapper
    {
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static ItemSection CreateFrom(this DomainModels.ItemSection source)
        {
            return new ItemSection
            {
                ItemSectionId = source.ItemSectionId,
                SectionNo = source.SectionNo,
                SectionName = source.SectionName,
                ItemId = source.ItemId,
                PressId = source.PressId,
                PressName = source.Machine != null ? source.Machine.MachineName : string.Empty,
                StockItemId1 = source.StockItemID1,
                StockItem1Name = source.StockItem != null ? source.StockItem.ItemName : string.Empty,
                SectionSizeId = source.SectionSizeId,
                ItemSizeId = source.ItemSizeId,
                SectionSizeHeight = source.SectionSizeHeight,
                SectionSizeWidth = source.SectionSizeWidth,
                ItemSizeHeight = source.ItemSizeHeight,
                ItemSizeWidth = source.ItemSizeWidth,
                IsItemSizeCustom = source.IsItemSizeCustom,
                IsSectionSizeCustom = source.IsSectionSizeCustom,
                SectionCostcentres = source.SectionCostcentres != null ? source.SectionCostcentres.Select(c => c.CreateFrom()).ToList() : null
            };
        }

        /// <summary>
        /// Create From WebApi Model
        /// </summary>
        public static DomainModels.ItemSection CreateFrom(this ItemSection source)
        {
            return new DomainModels.ItemSection
            {
                ItemSectionId = source.ItemSectionId,
                SectionNo = source.SectionNo,
                SectionName = source.SectionName,
                ItemId = source.ItemId,
                PressId = source.PressId,
                StockItemID1 = source.StockItemId1,
                SectionSizeId = source.SectionSizeId,
                ItemSizeId = source.ItemSizeId,
                SectionSizeHeight = source.SectionSizeHeight,
                SectionSizeWidth = source.SectionSizeWidth,
                ItemSizeHeight = source.ItemSizeHeight,
                ItemSizeWidth = source.ItemSizeWidth,
                IsItemSizeCustom = source.IsItemSizeCustom,
                IsSectionSizeCustom = source.IsSectionSizeCustom
            };
        }

        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static ItemSection CreateFromForOrder(this DomainModels.ItemSection source)
        {
            return new ItemSection
            {
                ItemSectionId = source.ItemSectionId,
                SectionNo = source.SectionNo,
                SectionName = source.SectionName,
                ItemId = source.ItemId,
                PressId = source.PressId,
                PressName = source.Machine != null ? source.Machine.MachineName : string.Empty,
                StockItemId1 = source.StockItemID1,
                StockItem1Name = source.StockItem != null ? source.StockItem.ItemName : string.Empty,
                SectionSizeId = source.SectionSizeId,
                ItemSizeId = source.ItemSizeId,
                SectionSizeHeight = source.SectionSizeHeight,
                SectionSizeWidth = source.SectionSizeWidth,
                ItemSizeHeight = source.ItemSizeHeight,
                ItemSizeWidth = source.ItemSizeWidth,
                IsItemSizeCustom = source.IsItemSizeCustom,
                IsSectionSizeCustom = source.IsSectionSizeCustom,
                Qty1 = source.Qty1,
                Qty2 = source.Qty2,
                Qty3 = source.Qty3,
                Qty1Profit = source.Qty1Profit,
                Qty2Profit = source.Qty2Profit,
                Qty3Profit = source.Qty3Profit,
                BaseCharge1 = source.BaseCharge1,
                BaseCharge2 = source.BaseCharge2,
                Basecharge3 = source.Basecharge3,
                IsDoubleSided = source.IsDoubleSided,
                IsWorknTurn = source.isWorknTurn,
                IsPaperSupplied = source.IsPaperSupplied,
                IncludeGutter = source.IncludeGutter,
                SimilarSections = source.SimilarSections,
                PlateInkId = source.PlateInkId,
                PrintViewLayoutLandScape = source.PrintViewLayoutLandScape,
                PrintViewLayoutPortrait = source.PrintViewLayoutPortrait,
                SectionCostcentres = source.SectionCostcentres != null ? source.SectionCostcentres.Select(sc => sc.CreateFrom()) :
                new List<SectionCostcentre>()
            };
        }

    }
}