using System.Collections.Generic;
using System.Linq;
using MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.DomainModels;

    /// <summary>
    /// SectionCostcentre Mapper
    /// </summary>
    public static class SectionCostcentreMapper
    {
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static SectionCostcentre CreateFrom(this DomainModels.SectionCostcentre source)
        {
            return new SectionCostcentre
            {
                SectionCostcentreId = source.SectionCostcentreId,
                CostCentreId = source.CostCentreId,
                CostCentreType = source.CostCentre != null ? source.CostCentre.Type : source.CostCentreType,
                CostCentreName = source.CostCentre != null ? source.CostCentre.Name : string.Empty,
                CostingActualCost = source.CostingActualCost,
                CostingActualQty = source.CostingActualQty,
                CostingActualTime = source.CostingActualTime,
                Name = source.Name,
                Qty1 = source.Qty1,
                Qty1Charge = source.Qty1Charge,
                Qty1NetTotal = source.Qty1NetTotal,
                Qty1MarkUpID = source.Qty1MarkUpID,
                Qty1MarkUpValue = source.Qty1MarkUpValue,
                Qty2 = source.Qty2,
                Qty2Charge = source.Qty2Charge,
                Qty2NetTotal = source.Qty2NetTotal,
                Qty2MarkUpID = source.Qty2MarkUpID,
                Qty2MarkUpValue = source.Qty2MarkUpValue,
                Qty3 = source.Qty3,
                Qty3Charge = source.Qty3Charge,
                Qty3NetTotal = source.Qty3NetTotal,
                Qty3MarkUpID = source.Qty3MarkUpID,
                Qty3MarkUpValue = source.Qty3MarkUpValue,
                ItemSectionId = source.ItemSectionId,
                Qty1WorkInstructions = source.Qty1WorkInstructions,
                Qty2WorkInstructions = source.Qty2WorkInstructions,
                Qty3WorkInstructions = source.Qty3WorkInstructions,
                Qty1EstimatedStockCost = source.Qty1EstimatedStockCost,
                Qty2EstimatedStockCost = source.Qty2EstimatedStockCost,
                Qty3EstimatedStockCost = source.Qty3EstimatedStockCost,
                SectionCostCentreDetails = source.SectionCostCentreDetails != null ? source.SectionCostCentreDetails.Select(s => s.CreateFrom()) :
                new List<SectionCostCentreDetail>(),
                SectionCostCentreResources = source.SectionCostCentreResources != null ? source.SectionCostCentreResources.Select(s => s.CreateFrom()) :
                new List<SectionCostCentreResource>()
            };
        }

        /// <summary>
        /// Create From WebApi Model
        /// </summary>
        public static DomainModels.SectionCostcentre CreateFrom(this SectionCostcentre source)
        {
            return new DomainModels.SectionCostcentre
            {
                SectionCostcentreId = source.SectionCostcentreId,
                Name = source.Name,
                CostCentreId = source.CostCentreId,
                Qty1 = source.Qty1,
                Qty1Charge = source.Qty1Charge,
                Qty1NetTotal = source.Qty1NetTotal,
                Qty1MarkUpID = source.Qty1MarkUpID,
                Qty1MarkUpValue = source.Qty1MarkUpValue,
                Qty2 = source.Qty2,
                Qty2Charge = source.Qty2Charge,
                Qty2NetTotal = source.Qty2NetTotal,
                Qty2MarkUpID = source.Qty2MarkUpID,
                Qty2MarkUpValue = source.Qty2MarkUpValue,
                Qty3 = source.Qty3,
                Qty3Charge = source.Qty3Charge,
                Qty3NetTotal = source.Qty3NetTotal,
                Qty3MarkUpID = source.Qty3MarkUpID,
                Qty3MarkUpValue = source.Qty3MarkUpValue,
                ItemSectionId = source.ItemSectionId,
                Qty1WorkInstructions = source.Qty1WorkInstructions,
                Qty2WorkInstructions = source.Qty2WorkInstructions,
                Qty3WorkInstructions = source.Qty3WorkInstructions,
                Qty1EstimatedStockCost = source.Qty1EstimatedStockCost,
                Qty2EstimatedStockCost = source.Qty2EstimatedStockCost,
                Qty3EstimatedStockCost = source.Qty3EstimatedStockCost,
                SectionCostCentreDetails = source.SectionCostCentreDetails != null ? source.SectionCostCentreDetails.Select(s => s.CreateFrom()).ToList() :
                new List<DomainModels.SectionCostCentreDetail>(),
            };
        }

    }
}