using MPC.Models.DomainModels;
namespace MPC.Models.ModelMappers
{
    using System;

    /// <summary>
    /// Section Cost Centre mapper
    /// </summary>
    public static class SectionCostCentreMapper
    {
        #region Public

        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateTo(this SectionCostcentre source, SectionCostcentre target)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.SectionCostcentreId = source.SectionCostcentreId;
            target.Name = source.Name;
            target.ItemSectionId = source.ItemSectionId;
            target.Qty1 = source.Qty1;
            target.Qty2 = source.Qty2;
            target.Qty3 = source.Qty3;
            target.Qty1MarkUpID = source.Qty1MarkUpID;
            target.Qty2MarkUpID = source.Qty2MarkUpID;
            target.Qty3MarkUpID = source.Qty3MarkUpID;
            target.Qty1MarkUpValue = source.Qty1MarkUpValue;
            target.Qty2MarkUpValue = source.Qty2MarkUpValue;
            target.Qty3MarkUpValue = source.Qty3MarkUpValue;
            target.Qty1EstimatedStockCost = source.Qty1EstimatedStockCost;
            target.Qty2EstimatedStockCost = source.Qty2EstimatedStockCost;
            target.Qty3EstimatedStockCost = source.Qty3EstimatedStockCost;
            target.Qty1Charge = source.Qty1Charge;
            target.Qty2Charge = source.Qty2Charge;
            target.Qty3Charge = source.Qty3Charge;
            target.Qty1NetTotal = source.Qty1NetTotal;
            target.Qty2NetTotal = source.Qty2NetTotal;
            target.Qty3NetTotal = source.Qty3NetTotal;
            target.Qty1WorkInstructions = source.Qty1WorkInstructions;
            target.Qty2WorkInstructions = source.Qty2WorkInstructions;
            target.Qty3WorkInstructions = source.Qty3WorkInstructions;
            target.IsDirectCost = source.IsDirectCost;
            target.IsPurchaseOrderRaised = source.IsPurchaseOrderRaised;
            target.CostCentreId = source.CostCentreId;
            target.SystemCostCentreType = source.SystemCostCentreType;
        }

        #endregion
    }
}
