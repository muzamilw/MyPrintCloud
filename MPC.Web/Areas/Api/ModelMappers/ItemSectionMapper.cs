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
        /// Used in Product Screen
        /// </summary>
        public static ItemSection CreateFrom(this DomainModels.ItemSection source)
        {
// ReSharper disable SuggestUseVarKeywordEvident
            ItemSection section = new ItemSection
// ReSharper restore SuggestUseVarKeywordEvident
            {
                ItemSectionId = source.ItemSectionId,
                SectionNo = source.SectionNo,
                SectionName = source.SectionName,
                ItemId = source.ItemId,
                PressId = source.PressId,
                StockItemId1 = source.StockItemID1,
                SectionSizeId = source.SectionSizeId,
                ItemSizeId = source.ItemSizeId,
                SectionSizeHeight = source.SectionSizeHeight,
                SectionSizeWidth = source.SectionSizeWidth,
                ItemSizeHeight = source.ItemSizeHeight,
                ItemSizeWidth = source.ItemSizeWidth,
                IsItemSizeCustom = source.IsItemSizeCustom,
                IsSectionSizeCustom = source.IsSectionSizeCustom,
                IsDoubleSided = source.IsDoubleSided,
                IsWorknTurn = source.isWorknTurn,
                IsPortrait = source.IsPortrait,
                PrintViewLayout = source.PrintViewLayout,
                PrintViewLayoutLandScape = source.PrintViewLayoutLandScape,
                PrintViewLayoutPortrait = source.PrintViewLayoutPortrait,
                PressIdSide2 = source.PressIdSide2,
                ImpressionCoverageSide1 = source.ImpressionCoverageSide1,
                ImpressionCoverageSide2 = source.ImpressionCoverageSide2,
                PrintingType = source.PrintingType,
                WebSpoilageType = source.WebSpoilageType,
                SectionInkCoverages = source.SectionInkCoverages != null ? source.SectionInkCoverages.Select(sc => sc.CreateFrom()).ToList() : null
            };

            if (source.Machine != null)
            {
                section.PressName = source.Machine.MachineName;
                section.PressSide1ColourHeads = source.Machine.ColourHeads;
                section.PressSide1IsSpotColor = source.Machine.IsSpotColor;
            }

            if (source.MachineSide2 != null)
            {
                section.PressSide2ColourHeads = source.MachineSide2.ColourHeads;
                section.PressSide2IsSpotColor = source.MachineSide2.IsSpotColor;
            }

            if (source.StockItem != null)
            {
                section.StockItem1Name = source.StockItem.ItemName;
                section.StockItemPackageQty = source.StockItem.PackageQty;
            }

            return section;
        }

        /// <summary>
        /// Create From WebApi Model
        /// Used in Product Screen
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
                IsSectionSizeCustom = source.IsSectionSizeCustom,
                IsDoubleSided = source.IsDoubleSided,
                isWorknTurn = source.IsWorknTurn,
                IsPortrait = source.IsPortrait,
                PrintViewLayout = source.PrintViewLayout,
                PrintViewLayoutLandScape = source.PrintViewLayoutLandScape,
                PrintViewLayoutPortrait = source.PrintViewLayoutPortrait,
                PressIdSide2 = source.PressIdSide2,
                ImpressionCoverageSide1 = source.ImpressionCoverageSide1,
                ImpressionCoverageSide2 = source.ImpressionCoverageSide2,
                PrintingType = source.PrintingType,
                SectionInkCoverages = source.SectionInkCoverages != null ? source.SectionInkCoverages.Select(sc => sc.CreateFrom()).ToList() : null
            };
        }

        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static ItemSection CreateFromForOrder(this DomainModels.ItemSection source)
        {
// ReSharper disable SuggestUseVarKeywordEvident
            ItemSection section = new ItemSection
// ReSharper restore SuggestUseVarKeywordEvident
            {
                ItemSectionId = source.ItemSectionId,
                SectionNo = source.SectionNo,
                SectionName = source.SectionName,
                ItemId = source.ItemId,
                PressId = source.PressId,
                StockItemId1 = source.StockItemID1,
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
                IsPortrait = source.IsPortrait,
                PrintViewLayout = source.PrintViewLayout,
                PrintViewLayoutLandScape = source.PrintViewLayoutLandScape,
                PrintViewLayoutPortrait = source.PrintViewLayoutPortrait,
                Side1Inks = source.Side1Inks,
                Side2Inks = source.Side2Inks,
                IsFirstTrim = source.IsFirstTrim,
                IsSecondTrim = source.IsSecondTrim,
                PressIdSide2 = source.PressIdSide2,
                PassesSide1 = source.PassesSide1,
                PassesSide2 = source.PassesSide2,
                ImpressionCoverageSide1 = source.ImpressionCoverageSide1,
                ImpressionCoverageSide2 = source.ImpressionCoverageSide2,
                PrintingType = source.PrintingType,
                SectionCostcentres = source.SectionCostcentres != null ? source.SectionCostcentres.Select(sc => sc.CreateFrom()) :  new List<SectionCostcentre>(),
                SectionInkCoverages = source.SectionInkCoverages != null ? source.SectionInkCoverages.Select(sc => sc.CreateFrom()) : new List<SectionInkCoverage>()
            };

            if (source.Machine != null)
            {
                section.PressName = source.Machine.MachineName;
                section.PressSide1ColourHeads = source.Machine.ColourHeads;
                section.PressSide1IsSpotColor = source.Machine.IsSpotColor;
            }

            if (source.MachineSide2 != null)
            {
                section.PressSide2ColourHeads = source.MachineSide2.ColourHeads;
                section.PressSide2IsSpotColor = source.MachineSide2.IsSpotColor;
            }

            if (source.StockItem != null)
            {
                section.StockItem1Name = source.StockItem.ItemName;
                section.StockItemPackageQty = source.StockItem.PackageQty;
            }

            return section;
        }

        /// <summary>
        /// Create From WebApi Model
        /// Used in Orders
        /// </summary>
        public static DomainModels.ItemSection CreateFromForOrder(this ItemSection source)
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
                IsPortrait = source.IsPortrait,
                PrintViewLayout = source.PrintViewLayout,
                PrintViewLayoutLandScape = source.PrintViewLayoutLandScape,
                PrintViewLayoutPortrait = source.PrintViewLayoutPortrait,
                IsPaperSupplied = source.IsPaperSupplied,
                isWorknTurn = source.IsWorknTurn,
                PlateInkId = source.PlateInkId,
                Side1Inks = source.Side1Inks,
                Side2Inks = source.Side2Inks,
                IsFirstTrim = source.IsFirstTrim,
                IsSecondTrim = source.IsSecondTrim,
                PressIdSide2 = source.PressIdSide2,
                PassesSide1 = source.PassesSide1,
                PassesSide2 = source.PassesSide2,
                ImpressionCoverageSide1 = source.ImpressionCoverageSide1,
                ImpressionCoverageSide2 = source.ImpressionCoverageSide2,
                PrintingType = source.PrintingType,
                SectionCostcentres = source.SectionCostcentres != null ? source.SectionCostcentres.Select(c => c.CreateFrom()).ToList() : null,
                SectionInkCoverages = source.SectionInkCoverages != null ? source.SectionInkCoverages.Select(sc => sc.CreateFrom()).ToList() : null
            };
        }

    }
}