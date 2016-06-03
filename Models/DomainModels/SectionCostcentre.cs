using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    [Serializable]
    public class SectionCostcentre
    {
        public int SectionCostcentreId { get; set; }
        public Nullable<long> ItemSectionId { get; set; }
        public Nullable<long> CostCentreId { get; set; }
        public Nullable<int> CostCentreType { get; set; }
        public Nullable<int> SystemCostCentreType { get; set; }
        public short Order { get; set; }
        public Nullable<short> IsDirectCost { get; set; }
        public short IsOptionalExtra { get; set; }
        public Nullable<short> IsPurchaseOrderRaised { get; set; }
        public Nullable<short> Status { get; set; }
        public Nullable<int> ActivityUser { get; set; }
        public Nullable<short> IsPrintable { get; set; }
        public Nullable<System.DateTime> EstimatedStartTime { get; set; }
        public Nullable<int> EstimatedDuration { get; set; }
        public Nullable<System.DateTime> EstimatedEndTime { get; set; }
        public Nullable<long> ActualDuration { get; set; }
        public Nullable<System.DateTime> ActualStartDateTime { get; set; }
        public Nullable<System.DateTime> ActualEndTime { get; set; }
        public Nullable<double> Qty1Charge { get; set; }
        public Nullable<double> Qty2Charge { get; set; }
        public Nullable<double> Qty3Charge { get; set; }
        public Nullable<double> Qty4Charge { get; set; }
        public Nullable<double> Qty5Charge { get; set; }
        public Nullable<int> Qty1MarkUpID { get; set; }
        public Nullable<int> Qty2MarkUpID { get; set; }
        public Nullable<int> Qty3MarkUpID { get; set; }
        public Nullable<int> Qty4MarkUpID { get; set; }
        public Nullable<int> Qty5MarkUpID { get; set; }
        public Nullable<double> Qty1MarkUpValue { get; set; }
        public Nullable<double> Qty2MarkUpValue { get; set; }
        public Nullable<double> Qty3MarkUpValue { get; set; }
        public Nullable<double> Qty4MarkUpValue { get; set; }
        public Nullable<double> Qty5MarkUpValue { get; set; }
        public Nullable<double> Qty1NetTotal { get; set; }
        public Nullable<double> Qty2NetTotal { get; set; }
        public Nullable<double> Qty3NetTotal { get; set; }
        public Nullable<double> Qty4NetTotal { get; set; }
        public Nullable<double> Qty5NetTotal { get; set; }
        public Nullable<double> Qty1EstimatedPlantCost { get; set; }
        public Nullable<double> Qty1EstimatedLabourCost { get; set; }
        public Nullable<double> Qty1EstimatedStockCost { get; set; }
        public double Qty1EstimatedTime { get; set; }
        public Nullable<double> Qty1QuotedPlantCharge { get; set; }
        public Nullable<double> Qty1QuotedLabourCharge { get; set; }
        public Nullable<double> Qty1QuotedStockCharge { get; set; }
        public Nullable<double> Qty2EstimatedPlantCost { get; set; }
        public Nullable<double> Qty2EstimatedLabourCost { get; set; }
        public Nullable<double> Qty2EstimatedStockCost { get; set; }
        public double Qty2EstimatedTime { get; set; }
        public Nullable<double> Qty2QuotedPlantCharge { get; set; }
        public Nullable<double> Qty2QuotedLabourCharge { get; set; }
        public Nullable<double> Qty2QuotedStockCharge { get; set; }
        public Nullable<double> Qty3EstimatedPlantCost { get; set; }
        public Nullable<double> Qty3EstimatedLabourCost { get; set; }
        public Nullable<double> Qty3EstimatedStockCost { get; set; }
        public double Qty3EstimatedTime { get; set; }
        public Nullable<double> Qty3QuotedPlantCharge { get; set; }
        public Nullable<double> Qty3QuotedLabourCharge { get; set; }
        public Nullable<double> Qty3QuotedStockCharge { get; set; }
        public Nullable<double> Qty4EstimatedPlantCost { get; set; }
        public Nullable<double> Qty4EstimatedLabourCost { get; set; }
        public Nullable<double> Qty4EstimatedStockCost { get; set; }
        public double Qty4EstimatedTime { get; set; }
        public Nullable<double> Qty4QuotedPlantCharge { get; set; }
        public Nullable<double> Qty4QuotedLabourCharge { get; set; }
        public Nullable<double> Qty4QuotedStockCharge { get; set; }
        public Nullable<double> Qty5EstimatedPlantCost { get; set; }
        public Nullable<double> Qty5EstimatedLabourCost { get; set; }
        public Nullable<double> Qty5EstimatedStockCost { get; set; }
        public double Qty5EstimatedTime { get; set; }
        public Nullable<double> Qty5QuotedPlantCharge { get; set; }
        public Nullable<double> Qty5QuotedLabourCharge { get; set; }
        public Nullable<double> Qty5QuotedStockCharge { get; set; }
        public Nullable<double> ActualPlantCost { get; set; }
        public Nullable<double> ActualLabourCost { get; set; }
        public Nullable<double> ActualStockCost { get; set; }
        public string Qty1WorkInstructions { get; set; }
        public string Qty2WorkInstructions { get; set; }
        public string Qty3WorkInstructions { get; set; }
        public string Qty4WorkInstructions { get; set; }
        public string Qty5WorkInstructions { get; set; }
        public Nullable<short> IsCostCentreUsedinPurchaseOrder { get; set; }
        public short IsMinimumCost { get; set; }
        public double SetupTime { get; set; }
        public short IsScheduled { get; set; }
        public short IsScheduleable { get; set; }
        public bool Locked { get; set; }
        public Nullable<double> CostingActualCost { get; set; }
        public Nullable<double> CostingActualTime { get; set; }
        public Nullable<double> CostingActualQty { get; set; }
        public string Name { get; set; }
        public Nullable<int> Qty1 { get; set; }
        public Nullable<int> Qty2 { get; set; }
        public Nullable<int> Qty3 { get; set; }
        public Nullable<double> SetupCost { get; set; }
        public Nullable<double> PricePerUnitQty { get; set; }

        public virtual CostCentre CostCentre { get; set; }
        public virtual ItemSection ItemSection { get; set; }
        public virtual ICollection<SectionCostCentreDetail> SectionCostCentreDetails { get; set; }
        public virtual ICollection<SectionCostCentreResource> SectionCostCentreResources { get; set; }

        public void Clone(SectionCostcentre target)
        {
            target.CostCentreId = CostCentreId;
            target.CostingActualCost = CostingActualCost;
            target.CostingActualQty = CostingActualQty;
            target.CostingActualTime = CostingActualTime;
            target.Name = Name;
            target.Qty1 = Qty1;
            target.Qty1Charge = Qty1Charge;
            target.Qty1NetTotal = Qty1NetTotal;
            target.Qty1MarkUpID = Qty1MarkUpID;
            target.Qty1MarkUpValue = Qty1MarkUpValue;
            target.Qty2 = Qty2;
            target.Qty2Charge = Qty2Charge;
            target.Qty2NetTotal = Qty2NetTotal;
            target.Qty2MarkUpID = Qty2MarkUpID;
            target.Qty2MarkUpValue = Qty2MarkUpValue;
            target.Qty3 = Qty3;
            target.Qty3Charge = Qty3Charge;
            target.Qty3NetTotal = Qty3NetTotal;
            target.Qty3MarkUpID = Qty3MarkUpID;
            target.Qty3MarkUpValue = Qty3MarkUpValue;
            target.ItemSectionId = ItemSectionId;
            target.Qty1WorkInstructions = Qty1WorkInstructions;
            target.Qty2WorkInstructions = Qty2WorkInstructions;
            target.Qty3WorkInstructions = Qty3WorkInstructions;
            target.Qty1EstimatedStockCost = Qty1EstimatedStockCost;
            target.Qty2EstimatedStockCost = Qty2EstimatedStockCost;
            target.Qty3EstimatedStockCost = Qty3EstimatedStockCost;
        }
    }
}
