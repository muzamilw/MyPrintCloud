using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Item Section WebApi Model
    /// </summary>
    public class ItemSection
    {
        public long ItemSectionId { get; set; }
        public int? SectionNo { get; set; }
        public string SectionName { get; set; }
        public bool? IsMainSection { get; set; }
        public long? ItemId { get; set; }
        public int? SectionSizeId { get; set; }
        public bool? IsSectionSizeCustom { get; set; }
        public double? SectionSizeHeight { get; set; }
        public double? SectionSizeWidth { get; set; }
        public int? ItemSizeId { get; set; }
        public bool? IsItemSizeCustom { get; set; }
        public double? ItemSizeHeight { get; set; }
        public double? ItemSizeWidth { get; set; }
        public int? GuillotineId { get; set; }
        public int? PressId { get; set; }
        public long? StockItemId1 { get; set; }
        public string StockItem1Name { get; set; }
        public string PressName { get; set; }
        public int? Qty1 { get; set; }
        public int? Qty2 { get; set; }
        public int? Qty3 { get; set; }
        public double? Qty1Profit { get; set; }
        public double? Qty2Profit { get; set; }
        public double? Qty3Profit { get; set; }
        public double? BaseCharge1 { get; set; }
        public double? BaseCharge2 { get; set; }
        public double? Basecharge3 { get; set; }
        public bool? IncludeGutter { get; set; }
        public int? FilmId { get; set; }
        public bool? IsPaperSupplied { get; set; }
        public int? Side1PlateQty { get; set; }
        public int? Side2PlateQty { get; set; }
        public bool? IsPlateSupplied { get; set; }
        public bool? IsDoubleSided { get; set; }
        public bool? IsWorknTurn { get; set; }
        public int? PrintViewLayout { get; set; }
        public int? PrintViewLayoutLandScape { get; set; }
        public int? PrintViewLayoutPortrait { get; set; }
        public int? SimilarSections { get; set; }
        public int? PlateInkId { get; set; }
        public int Side1Inks { get; set; }
        public int Side2Inks { get; set; }
        public bool? IsPortrait { get; set; }
        public bool? IsFirstTrim { get; set; }
        public bool? IsSecondTrim { get; set; }
        public int? PressIdSide2 { get; set; }
        public int? ImpressionCoverageSide1 { get; set; }
        public int? ImpressionCoverageSide2 { get; set; }
        public int? PassesSide1 { get; set; }
        public int? PassesSide2 { get; set; }
        public int? PrintingType { get; set; }
        public int? PressSide1ColourHeads { get; set; }
        public int? PressSide2ColourHeads { get; set; }
        public bool? PressSide1IsSpotColor { get; set; }
        public bool? PressSide2IsSpotColor { get; set; }
        public double? StockItemPackageQty { get; set; }
        
        public Item Item { get; set; }
        public StockItem StockItem { get; set; }
        public Machine Machine { get; set; }
       
        public IEnumerable<SectionCostcentre> SectionCostcentres { get; set; }
        public IEnumerable<SectionInkCoverage> SectionInkCoverages { get; set; }
    }
}