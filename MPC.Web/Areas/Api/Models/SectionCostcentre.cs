using System;
using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// SectionCostcentre WebApi Model
    /// </summary>
    public class SectionCostcentre
    {
        public int SectionCostcentreId { get; set; }
        public long? ItemSectionId { get; set; }
        public long? CostCentreId { get; set; }
        public int? CostCentreType { get; set; }
        public string CostCentreName { get; set; }
        public int? SystemCostCentreType { get; set; }
        public short Order { get; set; }
        public short? IsDirectCost { get; set; }
        public short IsOptionalExtra { get; set; }
        public short? IsPurchaseOrderRaised { get; set; }
        public short? Status { get; set; }
        public int? ActivityUser { get; set; }
        public short? IsPrintable { get; set; }
        public DateTime? EstimatedStartTime { get; set; }
        public int? EstimatedDuration { get; set; }
        public DateTime? EstimatedEndTime { get; set; }
        public long? ActualDuration { get; set; }
        public DateTime? ActualStartDateTime { get; set; }
        public DateTime? ActualEndTime { get; set; }
        public double? Qty1Charge { get; set; }
        public double? Qty2Charge { get; set; }
        public double? Qty3Charge { get; set; }
        public double? Qty4Charge { get; set; }
        public double? Qty5Charge { get; set; }
        public int? Qty1MarkUpID { get; set; }
        public int? Qty2MarkUpID { get; set; }
        public int? Qty3MarkUpID { get; set; }
        public int? Qty4MarkUpID { get; set; }
        public int? Qty5MarkUpID { get; set; }
        public double? Qty1MarkUpValue { get; set; }
        public double? Qty2MarkUpValue { get; set; }
        public double? Qty3MarkUpValue { get; set; }
        public double? Qty4MarkUpValue { get; set; }
        public double? Qty5MarkUpValue { get; set; }
        public double? Qty1NetTotal { get; set; }
        public double? Qty2NetTotal { get; set; }
        public double? Qty3NetTotal { get; set; }
        public double? Qty4NetTotal { get; set; }
        public double? Qty5NetTotal { get; set; }
        public double? Qty1EstimatedPlantCost { get; set; }
        public double? Qty1EstimatedLabourCost { get; set; }
        public double? Qty1EstimatedStockCost { get; set; }
        public double Qty1EstimatedTime { get; set; }
        public double? Qty1QuotedPlantCharge { get; set; }
        public double? Qty1QuotedLabourCharge { get; set; }
        public double? Qty1QuotedStockCharge { get; set; }
        public double? Qty2EstimatedPlantCost { get; set; }
        public double? Qty2EstimatedLabourCost { get; set; }
        public double? Qty2EstimatedStockCost { get; set; }
        public double Qty2EstimatedTime { get; set; }
        public double? Qty2QuotedPlantCharge { get; set; }
        public double? Qty2QuotedLabourCharge { get; set; }
        public double? Qty2QuotedStockCharge { get; set; }
        public double? Qty3EstimatedPlantCost { get; set; }
        public double? Qty3EstimatedLabourCost { get; set; }
        public double? Qty3EstimatedStockCost { get; set; }
        public double Qty3EstimatedTime { get; set; }
        public double? Qty3QuotedPlantCharge { get; set; }
        public double? Qty3QuotedLabourCharge { get; set; }
        public double? Qty3QuotedStockCharge { get; set; }
        public double? Qty4EstimatedPlantCost { get; set; }
        public double? Qty4EstimatedLabourCost { get; set; }
        public double? Qty4EstimatedStockCost { get; set; }
        public double Qty4EstimatedTime { get; set; }
        public double? Qty4QuotedPlantCharge { get; set; }
        public double? Qty4QuotedLabourCharge { get; set; }
        public double? Qty4QuotedStockCharge { get; set; }
        public double? Qty5EstimatedPlantCost { get; set; }
        public double? Qty5EstimatedLabourCost { get; set; }
        public double? Qty5EstimatedStockCost { get; set; }
        public double Qty5EstimatedTime { get; set; }
        public double? Qty5QuotedPlantCharge { get; set; }
        public double? Qty5QuotedLabourCharge { get; set; }
        public double? Qty5QuotedStockCharge { get; set; }
        public double? ActualPlantCost { get; set; }
        public double? ActualLabourCost { get; set; }
        public double? ActualStockCost { get; set; }
        public string Qty1WorkInstructions { get; set; }
        public string Qty2WorkInstructions { get; set; }
        public string Qty3WorkInstructions { get; set; }
        public string Qty4WorkInstructions { get; set; }
        public string Qty5WorkInstructions { get; set; }
        public short? IsCostCentreUsedinPurchaseOrder { get; set; }
        public short IsMinimumCost { get; set; }
        public double SetupTime { get; set; }
        public short IsScheduled { get; set; }
        public short IsScheduleable { get; set; }
        public bool Locked { get; set; }
        public double? CostingActualCost { get; set; }
        public double? CostingActualTime { get; set; }
        public double? CostingActualQty { get; set; }
        public string Name { get; set; }
        public int? Qty1 { get; set; }
        public int? Qty2 { get; set; }
        public int? Qty3 { get; set; }
        public double? SetupCost { get; set; }
        public double? PricePerUnitQty { get; set; }
        public IEnumerable<SectionCostCentreDetail> SectionCostCentreDetails { get; set; }
        public IEnumerable<SectionCostCentreResource> SectionCostCentreResources { get; set; }
    }
}