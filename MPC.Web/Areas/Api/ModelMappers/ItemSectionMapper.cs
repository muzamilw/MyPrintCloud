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
                StockItem = source.StockItem != null ? source.StockItem.CreateFromDetail() : null,
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
                IncludeGutter = source.IncludeGutter,
                IsDoubleSided = source.IsDoubleSided,
                PrintViewLayoutLandScape = source.PrintViewLayoutLandScape,
                PrintViewLayoutPortrait = source.PrintViewLayoutPortrait,
                IsPaperSupplied = source.IsPaperSupplied,
                IsWorknTurn = source.isWorknTurn,
                PlateInkId = source.PlateInkId,
                PrintViewLayout = source.PrintViewLayout,
                SectionCostcentres = source.SectionCostcentres != null ? source.SectionCostcentres.Select(c => c.CreateFrom()).ToList() : null,
                SectionInkCoverages = source.SectionInkCoverages != null ? source.SectionInkCoverages.Select(sc => sc.CreateFrom()) : new List<SectionInkCoverage>()
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
                Machine = source.Machine != null ? source.Machine.CreateFrom() : null,
                StockItemID1 = source.StockItemId1,
                StockItem = source.StockItem != null ? source.StockItem.CreateFrom() : null,
                Item = source.Item != null ? source.Item.CreateFrom() : null,
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
                IncludeGutter = source.IncludeGutter,
                IsDoubleSided = source.IsDoubleSided,
                PrintViewLayout = source.PrintViewLayout,
                PrintViewLayoutLandScape = source.PrintViewLayoutLandScape,
                PrintViewLayoutPortrait = source.PrintViewLayoutPortrait,
                IsPaperSupplied = source.IsPaperSupplied,
                isWorknTurn = source.IsWorknTurn,
                PlateInkId = source.PlateInkId,
                Side1Inks = source.Side1Inks,
                Side2Inks = source.Side2Inks,
                SectionCostcentres = source.SectionCostcentres != null ? source.SectionCostcentres.Select(c => c.CreateFrom()).ToList() : null,
                SectionInkCoverages = source.SectionInkCoverages != null ? source.SectionInkCoverages.Select(sc => sc.CreateFrom()).ToList() : null

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
                Machine = source.Machine != null ? source.Machine.CreateFrom() : null,
                PressName = source.Machine != null ? source.Machine.MachineName : string.Empty,
                StockItemId1 = source.StockItemID1,
                StockItem1Name = source.StockItem != null ? source.StockItem.ItemName : string.Empty,
                StockItem = source.StockItem != null ? source.StockItem.CreateFromDetail() : null,
                Item = source.Item != null ? source.Item.CreateFrom() : null,
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
                PrintViewLayout = source.PrintViewLayout,
                PrintViewLayoutLandScape = source.PrintViewLayoutLandScape,
                PrintViewLayoutPortrait = source.PrintViewLayoutPortrait,
                Side1Inks = source.Side1Inks,
                Side2Inks = source.Side2Inks,
                SectionCostcentres = source.SectionCostcentres != null ? source.SectionCostcentres.Select(sc => sc.CreateFrom()) :  new List<SectionCostcentre>(),
                SectionInkCoverages = source.SectionInkCoverages != null ? source.SectionInkCoverages.Select(sc => sc.CreateFrom()) : new List<SectionInkCoverage>()
            };
        }

    }
}