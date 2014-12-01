﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class ItemSection
    {
        public long ItemSectionId { get; set; }
        public Nullable<int> SectionNo { get; set; }
        public string SectionName { get; set; }
        public Nullable<bool> IsMainSection { get; set; }
        public Nullable<bool> IsMultipleQty { get; set; }
        public Nullable<bool> IsRunOnQty { get; set; }
        public Nullable<long> ItemId { get; set; }
        public Nullable<int> Qty1 { get; set; }
        public Nullable<int> Qty2 { get; set; }
        public Nullable<int> Qty3 { get; set; }
        public Nullable<int> Qty4 { get; set; }
        public Nullable<int> Qty5 { get; set; }
        public Nullable<double> Qty1Profit { get; set; }
        public Nullable<double> Qty2Profit { get; set; }
        public Nullable<double> Qty3Profit { get; set; }
        public Nullable<double> Qty4Profit { get; set; }
        public Nullable<double> Qty5Profit { get; set; }
        public Nullable<double> BaseCharge1 { get; set; }
        public Nullable<double> BaseCharge2 { get; set; }
        public Nullable<double> Basecharge3 { get; set; }
        public Nullable<double> BaseCharge4 { get; set; }
        public Nullable<double> BaseCharge5 { get; set; }
        public Nullable<int> RunOnQty { get; set; }
        public Nullable<double> RunOnBaseCharge { get; set; }
        public Nullable<double> RunonProfit { get; set; }
        public Nullable<int> SectionSizeId { get; set; }
        public Nullable<bool> IsSectionSizeCustom { get; set; }
        public Nullable<double> SectionSizeHeight { get; set; }
        public Nullable<double> SectionSizeWidth { get; set; }
        public Nullable<int> ItemSizeId { get; set; }
        public Nullable<bool> IsItemSizeCustom { get; set; }
        public Nullable<double> ItemSizeHeight { get; set; }
        public Nullable<double> ItemSizeWidth { get; set; }
        public Nullable<int> GuillotineId { get; set; }
        public Nullable<bool> IncludeGutter { get; set; }
        public Nullable<int> PressId { get; set; }
        public Nullable<int> FilmId { get; set; }
        public Nullable<int> PlateId { get; set; }
        public Nullable<double> ItemGutterHorizontal { get; set; }
        public Nullable<double> ItemGutterVertical { get; set; }
        public Nullable<bool> IsPressrestrictionApplied { get; set; }
        public Nullable<bool> IsDoubleSided { get; set; }
        public Nullable<bool> IsWashup { get; set; }
        public Nullable<int> PrintViewLayoutLandScape { get; set; }
        public Nullable<int> PrintViewLayoutPortrait { get; set; }
        public Nullable<int> PrintViewLayout { get; set; }
        public Nullable<int> SetupSpoilage { get; set; }
        public Nullable<int> RunningSpoilage { get; set; }
        public Nullable<int> RunningSpoilageValue { get; set; }
        public Nullable<int> EstimateForWholePacks { get; set; }
        public Nullable<bool> IsFirstTrim { get; set; }
        public Nullable<bool> IsSecondTrim { get; set; }
        public Nullable<int> PaperQty { get; set; }
        public Nullable<int> ImpressionQty1 { get; set; }
        public Nullable<int> ImpressionQty2 { get; set; }
        public Nullable<int> ImpressionQty3 { get; set; }
        public Nullable<int> ImpressionQty4 { get; set; }
        public Nullable<int> ImpressionQty5 { get; set; }
        public Nullable<int> FilmQty { get; set; }
        public Nullable<bool> IsFilmSupplied { get; set; }
        public Nullable<bool> IsPlateSupplied { get; set; }
        public Nullable<bool> IsPaperSupplied { get; set; }
        public Nullable<int> WashupQty { get; set; }
        public Nullable<int> MakeReadyQty { get; set; }
        public Nullable<short> IsPaperCoated { get; set; }
        public Nullable<int> GuillotineFirstCut { get; set; }
        public Nullable<int> GuillotineSecondCut { get; set; }
        public Nullable<int> GuillotineCutTime { get; set; }
        public Nullable<int> GuillotineQty1BundlesFirstTrim { get; set; }
        public Nullable<int> GuillotineQty2BundlesFirstTrim { get; set; }
        public Nullable<int> GuillotineQty3BundlesFirstTrim { get; set; }
        public Nullable<int> GuillotineQty1BundlesSecondTrim { get; set; }
        public Nullable<int> GuillotineQty2BundlesSecondTrim { get; set; }
        public Nullable<int> GuillotineQty3BundlesSecondTrim { get; set; }
        public Nullable<int> GuillotineQty1FirstTrimCuts { get; set; }
        public Nullable<int> GuillotineQty2FirstTrimCuts { get; set; }
        public Nullable<int> GuillotineQty3FirstTrimCuts { get; set; }
        public Nullable<int> GuillotineQty1SecondTrimCuts { get; set; }
        public Nullable<int> GuillotineQty2SecondTrimCuts { get; set; }
        public Nullable<int> GuillotineQty3SecondTrimCuts { get; set; }
        public Nullable<int> GuillotineQty1TotalsCuts { get; set; }
        public Nullable<int> GuillotineQty2TotalsCuts { get; set; }
        public Nullable<int> GuillotineQty3TotalsCuts { get; set; }
        public Nullable<int> AdditionalFilmUsed { get; set; }
        public Nullable<int> AdditionalPlateUsed { get; set; }
        public Nullable<bool> IsFilmUsed { get; set; }
        public Nullable<bool> IsPlateUsed { get; set; }
        public Nullable<int> NoofUniqueInks { get; set; }
        public Nullable<short> WizardRunMode { get; set; }
        public Nullable<int> OverAllPTV { get; set; }
        public Nullable<int> ItemPTV { get; set; }
        public int Side1Inks { get; set; }
        public int Side2Inks { get; set; }
        public Nullable<int> IsSwingApplied { get; set; }
        public Nullable<short> SectionType { get; set; }
        public Nullable<bool> IsMakeReadyUsed { get; set; }
        public Nullable<bool> isWorknTurn { get; set; }
        public Nullable<bool> isWorkntumble { get; set; }
        public byte[] QuestionQueue { get; set; }
        public byte[] StockQueue { get; set; }
        public byte[] InputQueue { get; set; }
        public byte[] CostCentreQueue { get; set; }
        public Nullable<int> PressSpeed1 { get; set; }
        public Nullable<int> PressSpeed2 { get; set; }
        public Nullable<int> PressSpeed3 { get; set; }
        public int PressSpeed4 { get; set; }
        public Nullable<int> PressSpeed5 { get; set; }
        public Nullable<int> PrintSheetQty1 { get; set; }
        public Nullable<int> PrintSheetQty2 { get; set; }
        public Nullable<int> PrintSheetQty3 { get; set; }
        public Nullable<int> PrintSheetQty4 { get; set; }
        public Nullable<int> PrintSheetQty5 { get; set; }
        public Nullable<double> PressHourlyCharge { get; set; }
        public Nullable<double> PrintChargeExMakeReady1 { get; set; }
        public Nullable<double> PrintChargeExMakeReady2 { get; set; }
        public Nullable<double> PrintChargeExMakeReady3 { get; set; }
        public Nullable<double> PrintChargeExMakeReady4 { get; set; }
        public Nullable<double> PrintChargeExMakeReady5 { get; set; }
        public Nullable<double> PaperGsm { get; set; }
        public Nullable<double> PaperPackPrice { get; set; }
        public Nullable<int> PTVRows { get; set; }
        public Nullable<int> PTVColoumns { get; set; }
        public Nullable<double> PaperWeight1 { get; set; }
        public Nullable<double> PaperWeight2 { get; set; }
        public Nullable<double> PaperWeight3 { get; set; }
        public Nullable<double> PaperWeight4 { get; set; }
        public Nullable<double> PaperWeight5 { get; set; }
        public Nullable<int> FinishedItemQty1 { get; set; }
        public Nullable<int> FinishedItemQty2 { get; set; }
        public Nullable<int> FinishedItemQty3 { get; set; }
        public Nullable<int> FinishedItemQty4 { get; set; }
        public Nullable<int> FinishedItemQty5 { get; set; }
        public int ProfileId { get; set; }
        public Nullable<int> SelectedPressCalculationMethodId { get; set; }
        public string SectionNotes { get; set; }
        public Nullable<bool> IsScheduled { get; set; }
        public Nullable<int> ImageType { get; set; }
        public Nullable<double> WebClylinderHeight { get; set; }
        public Nullable<double> WebCylinderWidth { get; set; }
        public Nullable<int> WebCylinderId { get; set; }
        public Nullable<double> WebPaperLengthWithSp { get; set; }
        public Nullable<double> WebPaperLengthWoSp { get; set; }
        public Nullable<int> WebReelMakereadyQty { get; set; }
        public Nullable<double> WebStockPaperCost { get; set; }
        public Nullable<short> WebSpoilageType { get; set; }
        public Nullable<int> PressPassesQty { get; set; }
        public Nullable<int> PrintingType { get; set; }
        public Nullable<int> PadsLeafQty { get; set; }
        public Nullable<int> PadsQuantity { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public Nullable<int> LastUpdatedBy { get; set; }
        public Nullable<int> Qty1MarkUpID { get; set; }
        public Nullable<int> Qty2MarkUpID { get; set; }
        public Nullable<int> Qty3MarkUpID { get; set; }
        public Nullable<int> StockItemID1 { get; set; }
        public Nullable<int> StockItemID2 { get; set; }
        public Nullable<int> StockItemID3 { get; set; }
        public Nullable<int> Side1PlateQty { get; set; }
        public Nullable<bool> IsPortrait { get; set; }
        public Nullable<int> Side2PlateQty { get; set; }
        public Nullable<int> InkColorType { get; set; }
        public Nullable<double> BaseCharge1Broker { get; set; }
        public Nullable<int> PlateInkId { get; set; }
        public Nullable<int> SimilarSections { get; set; }

        public virtual Item Item { get; set; }
    }
}
