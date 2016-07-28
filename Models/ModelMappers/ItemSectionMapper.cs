using MPC.Models.DomainModels;
namespace MPC.Models.ModelMappers
{
    using System;

    /// <summary>
    /// Item Section mapper
    /// </summary>
    public static class ItemSectionMapper
    {
        #region Public

        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateTo(this ItemSection source, ItemSection target)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.ItemSectionId = source.ItemSectionId;
            target.SectionNo = source.SectionNo;
            target.SectionName = source.SectionName;
            target.ItemId = source.ItemId;
            target.PressId = source.PressId;
            target.StockItemID1 = source.StockItemID1;
            target.SectionSizeId = source.SectionSizeId;
            target.ItemSizeId = source.ItemSizeId;
            target.SectionSizeHeight = source.SectionSizeHeight;
            target.SectionSizeWidth = source.SectionSizeWidth;
            target.ItemSizeHeight = source.ItemSizeHeight;
            target.ItemSizeWidth = source.ItemSizeWidth;
            target.IsItemSizeCustom = source.IsItemSizeCustom;
            target.IsSectionSizeCustom = source.IsSectionSizeCustom;
            target.IsDoubleSided = source.IsDoubleSided;
            target.PrintViewLayoutLandScape = source.PrintViewLayoutLandScape;
            target.PrintViewLayoutPortrait = source.PrintViewLayoutPortrait;
            target.PressIdSide2 = source.PressIdSide2;
            target.ImpressionCoverageSide1 = source.ImpressionCoverageSide1;
            target.ImpressionCoverageSide2 = source.ImpressionCoverageSide2;
            target.PrintingType = source.PrintingType;
            target.isWorknTurn = source.isWorknTurn;
            target.IsPortrait = source.IsPortrait;
            target.ItemGutterHorizontal = source.ItemGutterHorizontal;
            target.ItemGutterVertical = source.ItemGutterHorizontal;
            target.CostCentreQueue = source.CostCentreQueue;
            target.InputQueue = source.InputQueue;
            target.QuestionQueue = source.QuestionQueue;
            target.StockQueue = source.StockQueue;
            target.PressSpeed1 = source.PressSpeed1;
            target.PressSpeed2 = source.PressSpeed2;
            target.PressSpeed3 = source.PressSpeed3;
            target.ImpressionQty1 = source.ImpressionQty1;
            target.ImpressionQty2 = source.ImpressionQty2;
            target.ImpressionQty3 = source.ImpressionQty3;
            target.PrintSheetQty1 = source.PrintSheetQty1;
            target.PrintSheetQty2 = source.PrintSheetQty2;
            target.PrintSheetQty3 = source.PrintSheetQty3;
            target.SetupSpoilage = source.SetupSpoilage;
            target.RunningSpoilage = source.RunningSpoilage;
            target.RunningSpoilageValue = source.RunningSpoilageValue;
            target.IsBooklet = source.IsBooklet;
        }

        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateToForOrder(this ItemSection source, ItemSection target)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.ItemSectionId = source.ItemSectionId;
            target.SectionNo = source.SectionNo;
            target.SectionName = source.SectionName;
            target.ItemId = source.ItemId;
            target.PressId = source.PressId;
            target.StockItemID1 = source.StockItemID1;
            target.SectionSizeId = source.SectionSizeId;
            target.ItemSizeId = source.ItemSizeId;
            target.SectionSizeHeight = source.SectionSizeHeight;
            target.SectionSizeWidth = source.SectionSizeWidth;
            target.ItemSizeHeight = source.ItemSizeHeight;
            target.ItemSizeWidth = source.ItemSizeWidth;
            target.IsItemSizeCustom = source.IsItemSizeCustom;
            target.IsSectionSizeCustom = source.IsSectionSizeCustom;
            target.IsDoubleSided = source.IsDoubleSided;
            target.IsPaperSupplied = source.IsPaperSupplied;
            target.PrintViewLayoutLandScape = source.PrintViewLayoutLandScape;
            target.PrintViewLayoutPortrait = source.PrintViewLayoutPortrait;
            target.SimilarSections = source.SimilarSections;
            target.PlateInkId = source.PlateInkId;
            target.Qty1 = source.Qty1;
            target.Qty2 = source.Qty2;
            target.Qty3 = source.Qty3;
            target.Qty1MarkUpID = source.Qty1MarkUpID;
            target.Qty2MarkUpID = source.Qty2MarkUpID;
            target.Qty3MarkUpID = source.Qty3MarkUpID;
            target.BaseCharge1 = source.BaseCharge1;
            target.BaseCharge2 = source.BaseCharge2;
            target.Basecharge3 = source.Basecharge3;
            target.IsFirstTrim = source.IsFirstTrim;
            target.IsSecondTrim = source.IsSecondTrim;
            target.PressIdSide2 = source.PressIdSide2;
            target.PassesSide1 = source.PassesSide1;
            target.PassesSide2 = source.PassesSide2;
            target.ImpressionCoverageSide1 = source.ImpressionCoverageSide1;
            target.ImpressionCoverageSide2 = source.ImpressionCoverageSide2;
            target.PrintingType = source.PrintingType;
            target.isWorknTurn = source.isWorknTurn;
            target.ItemGutterHorizontal = source.ItemGutterHorizontal;
            target.ItemGutterVertical = source.ItemGutterHorizontal;
            target.CostCentreQueue = source.CostCentreQueue;
            target.InputQueue = source.InputQueue;
            target.StockQueue = source.StockQueue;
            target.QuestionQueue = source.QuestionQueue;
            target.PressSpeed1 = source.PressSpeed1;
            target.PressSpeed2 = source.PressSpeed2;
            target.PressSpeed3 = source.PressSpeed3;
            target.ImpressionQty1 = source.ImpressionQty1;
            target.ImpressionQty2 = source.ImpressionQty2;
            target.ImpressionQty3 = source.ImpressionQty3;
            target.PrintSheetQty1 = source.PrintSheetQty1;
            target.PrintSheetQty2 = source.PrintSheetQty2;
            target.PrintSheetQty3 = source.PrintSheetQty3;
            target.SetupSpoilage = source.SetupSpoilage;
            target.RunningSpoilage = source.RunningSpoilage;
            target.RunningSpoilageValue = source.RunningSpoilageValue;
            target.IsBooklet = source.IsBooklet;
        }

        #endregion
    }
}
