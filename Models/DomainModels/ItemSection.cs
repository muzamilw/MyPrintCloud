using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{

    [Serializable()]
    public class ItemSection
    {
        public long ItemSectionId { get; set; }
        public int? SectionNo { get; set; }
        public string SectionName { get; set; }
        public bool? IsMainSection { get; set; }
        public bool? IsMultipleQty { get; set; }
        public bool? IsRunOnQty { get; set; }
        public long? ItemId { get; set; }
        public int? Qty1 { get; set; }
        public int? Qty2 { get; set; }
        public int? Qty3 { get; set; }
        public int? Qty4 { get; set; }
        public int? Qty5 { get; set; }
        public double? Qty1Profit { get; set; }
        public double? Qty2Profit { get; set; }
        public double? Qty3Profit { get; set; }
        public double? Qty4Profit { get; set; }
        public double? Qty5Profit { get; set; }
        public double? BaseCharge1 { get; set; }
        public double? BaseCharge2 { get; set; }
        public double? Basecharge3 { get; set; }
        public double? BaseCharge4 { get; set; }
        public double? BaseCharge5 { get; set; }
        public int? RunOnQty { get; set; }
        public double? RunOnBaseCharge { get; set; }
        public double? RunonProfit { get; set; }
        public int? SectionSizeId { get; set; }
        public bool? IsSectionSizeCustom { get; set; }
        public double? SectionSizeHeight { get; set; }
        public double? SectionSizeWidth { get; set; }
        public int? ItemSizeId { get; set; }
        public bool? IsItemSizeCustom { get; set; }
        public double? ItemSizeHeight { get; set; }
        public double? ItemSizeWidth { get; set; }
        public int? GuillotineId { get; set; }
        public bool? IncludeGutter { get; set; }
        public int? PressId { get; set; }
        public int? FilmId { get; set; }
        public int? PlateId { get; set; }
        public double? ItemGutterHorizontal { get; set; }
        public double? ItemGutterVertical { get; set; }
        public bool? IsPressrestrictionApplied { get; set; }
        public bool? IsDoubleSided { get; set; }
        public bool? IsWashup { get; set; }
        public int? PrintViewLayoutLandScape { get; set; }
        public int? PrintViewLayoutPortrait { get; set; }
        public int? PrintViewLayout { get; set; }
        public int? SetupSpoilage { get; set; }
        public int? RunningSpoilage { get; set; }
        public int? RunningSpoilageValue { get; set; }
        public int? EstimateForWholePacks { get; set; }
        public bool? IsFirstTrim { get; set; }
        public bool? IsSecondTrim { get; set; }
        public int? PaperQty { get; set; }
        public int? ImpressionQty1 { get; set; }
        public int? ImpressionQty2 { get; set; }
        public int? ImpressionQty3 { get; set; }
        public int? ImpressionQty4 { get; set; }
        public int? ImpressionQty5 { get; set; }
        public int? FilmQty { get; set; }
        public bool? IsFilmSupplied { get; set; }
        public bool? IsPlateSupplied { get; set; }
        public bool? IsPaperSupplied { get; set; }
        public int? WashupQty { get; set; }
        public int? MakeReadyQty { get; set; }
        public short? IsPaperCoated { get; set; }
        public int? GuillotineFirstCut { get; set; }
        public int? GuillotineSecondCut { get; set; }
        public int? GuillotineCutTime { get; set; }
        public int? GuillotineQty1BundlesFirstTrim { get; set; }
        public int? GuillotineQty2BundlesFirstTrim { get; set; }
        public int? GuillotineQty3BundlesFirstTrim { get; set; }
        public int? GuillotineQty1BundlesSecondTrim { get; set; }
        public int? GuillotineQty2BundlesSecondTrim { get; set; }
        public int? GuillotineQty3BundlesSecondTrim { get; set; }
        public int? GuillotineQty1FirstTrimCuts { get; set; }
        public int? GuillotineQty2FirstTrimCuts { get; set; }
        public int? GuillotineQty3FirstTrimCuts { get; set; }
        public int? GuillotineQty1SecondTrimCuts { get; set; }
        public int? GuillotineQty2SecondTrimCuts { get; set; }
        public int? GuillotineQty3SecondTrimCuts { get; set; }
        public int? GuillotineQty1TotalsCuts { get; set; }
        public int? GuillotineQty2TotalsCuts { get; set; }
        public int? GuillotineQty3TotalsCuts { get; set; }
        public int? AdditionalFilmUsed { get; set; }
        public int? AdditionalPlateUsed { get; set; }
        public bool? IsFilmUsed { get; set; }
        public bool? IsPlateUsed { get; set; }
        public int? NoofUniqueInks { get; set; }
        public short? WizardRunMode { get; set; }
        public int? OverAllPTV { get; set; }
        public int? ItemPTV { get; set; }
        public int Side1Inks { get; set; }
        public int Side2Inks { get; set; }
        public int? IsSwingApplied { get; set; }
        public short? SectionType { get; set; }
        public bool? IsMakeReadyUsed { get; set; }
        public bool? isWorknTurn { get; set; }
        public bool? isWorkntumble { get; set; }
        public string QuestionQueue { get; set; }
        public string StockQueue { get; set; }
        public string InputQueue { get; set; }
        public string CostCentreQueue { get; set; }
        public int? PressSpeed1 { get; set; }
        public int? PressSpeed2 { get; set; }
        public int? PressSpeed3 { get; set; }
        public int PressSpeed4 { get; set; }
        public int? PressSpeed5 { get; set; }
        public int? PrintSheetQty1 { get; set; }
        public int? PrintSheetQty2 { get; set; }
        public int? PrintSheetQty3 { get; set; }
        public int? PrintSheetQty4 { get; set; }
        public int? PrintSheetQty5 { get; set; }
        public double? PressHourlyCharge { get; set; }
        public double? PrintChargeExMakeReady1 { get; set; }
        public double? PrintChargeExMakeReady2 { get; set; }
        public double? PrintChargeExMakeReady3 { get; set; }
        public double? PrintChargeExMakeReady4 { get; set; }
        public double? PrintChargeExMakeReady5 { get; set; }
        public double? PaperGsm { get; set; }
        public double? PaperPackPrice { get; set; }
        public int? PTVRows { get; set; }
        public int? PTVColoumns { get; set; }
        public double? PaperWeight1 { get; set; }
        public double? PaperWeight2 { get; set; }
        public double? PaperWeight3 { get; set; }
        public double? PaperWeight4 { get; set; }
        public double? PaperWeight5 { get; set; }
        public int? FinishedItemQty1 { get; set; }
        public int? FinishedItemQty2 { get; set; }
        public int? FinishedItemQty3 { get; set; }
        public int? FinishedItemQty4 { get; set; }
        public int? FinishedItemQty5 { get; set; }
        public int ProfileId { get; set; }
        public int? SelectedPressCalculationMethodId { get; set; }
        public string SectionNotes { get; set; }
        public bool? IsScheduled { get; set; }
        public int? ImageType { get; set; }
        public double? WebClylinderHeight { get; set; }
        public double? WebCylinderWidth { get; set; }
        public int? WebCylinderId { get; set; }
        public double? WebPaperLengthWithSp { get; set; }
        public double? WebPaperLengthWoSp { get; set; }
        public int? WebReelMakereadyQty { get; set; }
        public double? WebStockPaperCost { get; set; }
        public short? WebSpoilageType { get; set; }
        public int? PressPassesQty { get; set; }
        public int? PrintingType { get; set; }
        public int? PadsLeafQty { get; set; }
        public int? PadsQuantity { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public int? Qty1MarkUpID { get; set; }
        public int? Qty2MarkUpID { get; set; }
        public int? Qty3MarkUpID { get; set; }
        public long? StockItemID1 { get; set; }
        public int? StockItemID2 { get; set; }
        public int? StockItemID3 { get; set; }
        public int? Side1PlateQty { get; set; }
        public bool? IsPortrait { get; set; }
        public int? Side2PlateQty { get; set; }
        public int? InkColorType { get; set; }
        public int? PlateInkId { get; set; }
        public int? SimilarSections { get; set; }
        public int? PressIdSide2 { get; set; }
        public int? ImpressionCoverageSide1 { get; set; }
        public int? ImpressionCoverageSide2 { get; set; }
        public int? PassesSide1 { get; set; }
        public int? PassesSide2 { get; set; }
        [NotMapped]
        public string SectionSizeName { get; set; }

        //[NotMapped]
        //public double SectionPaperSizeHeight { get; set; }
        //[NotMapped]
        //public double SectionPaperSizeWidth { get; set; }

        //[NotMapped]
        //public int SectionPaperSizeMeasure { get; set; }
        //[NotMapped]
        //public bool SectionPaperSizeImperical { get; set; }

         [NotMapped]
        public string ItemSizeName { get; set; }

         //[NotMapped]
         //public double ItemPaperSizeHeight { get; set; }
         //[NotMapped]
         //public double ItemPaperSizeWidth { get; set; }

         //[NotMapped]
         //public int ItemPaperSizeMeasure { get; set; }
         //[NotMapped]
         //public bool ItemPaperSizeImperical { get; set; }
         [NotMapped]
        public string PressName { get; set; }
         [NotMapped]
         public string PressNameSide2 { get; set; }
         [NotMapped]
         public string StockName { get; set; }
        public virtual Item Item { get; set; }
        public virtual ICollection<SectionCostcentre> SectionCostcentres { get; set; }
        public virtual StockItem StockItem { get; set; }
        public virtual Machine Machine { get; set; }
        public virtual Machine MachineSide2 { get; set; }
        public virtual ICollection<SectionInkCoverage> SectionInkCoverages { get; set; }

        public double? SectionPaperWeightExSelfQty1 { get; set; }
        public double? SectionPaperWeightExSelfQty2 { get; set; }
        public double? SectionPaperWeightExSelfQty3 { get; set; }
        

        #region Public

        /// <summary>
        /// Creates Copy of Item Section
        /// </summary>
        public void Clone(ItemSection target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemSectionClone_InvalidItemSection, "target");
            }

            target.SectionName = SectionName;
            target.SectionNo = SectionNo;
            target.PressId = PressId;
            target.StockItemID1 = StockItemID1;
            target.SectionSizeId = SectionSizeId;
            target.ItemSectionId = ItemSectionId;
            target.SectionSizeHeight = SectionSizeHeight;
            target.SectionSizeWidth = SectionSizeWidth;
            target.IsSectionSizeCustom = IsSectionSizeCustom;
            target.ItemSizeHeight = ItemSizeHeight;
            target.ItemSizeWidth = ItemSizeWidth;
            target.IsItemSizeCustom = IsItemSizeCustom;
            target.IsMainSection = IsMainSection;
            target.ItemSizeId = ItemSizeId;
            target.PrintingType = PrintingType;
            target.PressIdSide2 = PressIdSide2;
            target.ImpressionCoverageSide1 = ImpressionCoverageSide1;
            target.ImpressionCoverageSide2 = ImpressionCoverageSide2;
            target.PrintViewLayout = PrintViewLayout;
            target.PrintViewLayoutLandScape = PrintViewLayoutLandScape;
            target.PrintViewLayoutPortrait = PrintViewLayoutPortrait;
            target.IsDoubleSided = IsDoubleSided;
            target.isWorknTurn = isWorknTurn;
            target.IsPortrait = IsPortrait;
        }

        /// <summary>
        /// Creates Copy of Item Section
        /// </summary>
        public void CloneForOrder(ItemSection target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemSectionClone_InvalidItemSection, "target");
            }

            Clone(target);
            target.Qty1 = Qty1;
            target.Qty2 = Qty2;
            target.Qty3 = Qty3;
            target.Qty1MarkUpID = Qty1MarkUpID;
            target.Qty2MarkUpID = Qty2MarkUpID;
            target.Qty3MarkUpID = Qty3MarkUpID;
            target.BaseCharge1 = BaseCharge1;
            target.BaseCharge2 = BaseCharge2;
            target.Basecharge3 = Basecharge3;
            target.PassesSide1 = PassesSide1;
            target.PassesSide2 = PassesSide2;
            target.IsFirstTrim = IsFirstTrim;
            target.IsSecondTrim = IsSecondTrim;
        }

        #endregion
    }
}
