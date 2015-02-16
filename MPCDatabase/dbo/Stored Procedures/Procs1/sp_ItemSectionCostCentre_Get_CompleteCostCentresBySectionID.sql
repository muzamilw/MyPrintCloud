﻿
CREATE PROCEDURE [dbo].[sp_ItemSectionCostCentre_Get_CompleteCostCentresBySectionID]
(
	@SectionID int
)
AS
	SELECT tbl_costcentres.CostCentreID,tbl_costcentres.Name,tbl_costcentres.Description,tbl_costcentres.Type,tbl_costcentres.CreatedBy,tbl_costcentres.LockedBy,tbl_costcentres.LastModifiedBy,tbl_costcentres.MinimumCost,tbl_costcentres.SetupCost,tbl_costcentres.SetupTime,tbl_costcentres.DefaultVA,tbl_costcentres.DefaultVAId,tbl_costcentres.OverHeadRate,tbl_costcentres.HourlyCharge,tbl_costcentres.CostPerThousand,tbl_costcentres.CreationDate,tbl_costcentres.LastModifiedDate,tbl_costcentres.PreferredSupplierID,tbl_costcentres.CodeFileName,tbl_costcentres.nominalCode,tbl_costcentres.CompletionTime,tbl_costcentres.HeaderCode,tbl_costcentres.MiddleCode,tbl_costcentres.FooterCode,tbl_costcentres.strCostPlantParsed,tbl_costcentres.strCostPlantUnParsed,tbl_costcentres.strCostLabourParsed,tbl_costcentres.strCostLabourUnParsed,tbl_costcentres.strCostMaterialParsed,tbl_costcentres.strCostMaterialUnParsed,tbl_costcentres.strPricePlantParsed,tbl_costcentres.strPricePlantUnParsed,tbl_costcentres.strPriceLabourParsed,tbl_costcentres.strPriceLabourUnParsed,tbl_costcentres.strPriceMaterialParsed,tbl_costcentres.strPriceMaterialUnParsed,tbl_costcentres.strActualCostPlantParsed,tbl_costcentres.strActualCostPlantUnParsed,tbl_costcentres.strActualCostLabourParsed,tbl_costcentres.strActualCostLabourUnParsed,tbl_costcentres.strActualCostMaterialParsed,tbl_costcentres.strActualCostMaterialUnParsed,tbl_costcentres.strTimeParsed,tbl_costcentres.strTimeUnParsed,tbl_costcentres.IsDisabled,tbl_costcentres.IsDirectCost,tbl_costcentres.SetupSpoilage,tbl_costcentres.RunningSpoilage,tbl_costcentres.CalculationMethodType,tbl_costcentres.NoOfHours,tbl_costcentres.PerHourCost,tbl_costcentres.PerHourPrice,tbl_costcentres.UnitQuantity,tbl_costcentres.QuantitySourceType,tbl_costcentres.QuantityVariableID,tbl_costcentres.QuantityQuestionString,tbl_costcentres.QuantityQuestionDefaultValue,tbl_costcentres.QuantityCalculationString,tbl_costcentres.CostPerUnitQuantity,tbl_costcentres.PricePerUnitQuantity,tbl_costcentres.TimePerUnitQuantity,tbl_costcentres.TimeRunSpeed,tbl_costcentres.TimeNoOfPasses,tbl_costcentres.TimeSourceType,tbl_costcentres.TimeVariableID,tbl_costcentres.TimeQuestionString,tbl_costcentres.TimeQuestionDefaultValue,tbl_costcentres.TimeCalculationString,tbl_costcentres.Priority,tbl_costcentres.CostQuestionString,tbl_costcentres.CostDefaultValue,tbl_costcentres.PriceQuestionString,tbl_costcentres.PriceDefaultValue,tbl_costcentres.EstimatedTimeQuestionString,tbl_costcentres.EstimatedTimeDefaultValue,tbl_costcentres.Sequence,tbl_costcentres.CompleteCode,tbl_costcentres.ItemDescription,tbl_costcentres.CompanyID,tbl_costcentres.SystemTypeID,tbl_costcentres.FlagID,tbl_costcentres.IsScheduleable,tbl_costcentres.SystemSiteID,tbl_costcentres.IsPrintOnJobCard,tbl_costcentres.WebStoreDesc,tbl_costcentres.isPublished,tbl_costcentres.EstimateProductionTime,tbl_costcentres.MainImageURL,tbl_costcentres.ThumbnailImageURL,tbl_costcentres.isOption1,tbl_costcentres.isOption2,tbl_costcentres.isOption3,tbl_costcentres.TextOption1,tbl_costcentres.TextOption2,tbl_costcentres.TextOption3,tbl_costcentres.CCIDOption1,tbl_costcentres.CCIDOption2,tbl_costcentres.CCIDOption3,tbl_costcentres.SetupCharge2,tbl_costcentres.SetupCharge3,tbl_costcentres.MinimumCost2,tbl_costcentres.MinimumCost3,tbl_costcentres.PricePerUnitQuantity2,tbl_costcentres.PricePerUnitQuantity3,tbl_costcentres.QuantityVariableID2,tbl_costcentres.QuantityVariableID3,tbl_costcentres.QuantitySourceType2,tbl_costcentres.QuantitySourceType3,tbl_costcentres.QuantityQuestionString2,tbl_costcentres.QuantityQuestionString3,tbl_costcentres.QuantityQuestionDefaultValue2,tbl_costcentres.QuantityQuestionDefaultValue3,tbl_costcentres.DefaultVAId2,tbl_costcentres.DefaultVAId3,tbl_costcentres.IsPrintOnJobCard2,tbl_costcentres.IsPrintOnJobCard3,tbl_costcentres.IsDirectCost2,tbl_costcentres.IsDirectCost3,tbl_costcentres.PreferredSupplierID2,tbl_costcentres.PreferredSupplierID3,tbl_costcentres.EstimateProductionTime2,tbl_costcentres.EstimateProductionTime3,tbl_costcentres.DeliveryCharges,
tbl_section_costcentres.SectionCostcentreID,tbl_section_costcentres.ItemSectionID,tbl_section_costcentres.CostCentreID,IsNull(tbl_section_costcentres.CostCentreType,0) as CostCentreType,tbl_section_costcentres.SystemCostCentreType,tbl_section_costcentres.[Order],tbl_section_costcentres.IsDirectCost,tbl_section_costcentres.IsOptionalExtra,IsNull(tbl_section_costcentres.IsPurchaseOrderRaised,0) as IsPurchaseOrderRaised,tbl_section_costcentres.Status,tbl_section_costcentres.ActivityUser,isnull(tbl_section_costcentres.IsPrintable,0) as IsPrintable,tbl_section_costcentres.EstimatedStartTime,tbl_section_costcentres.EstimatedDuration,tbl_section_costcentres.EstimatedEndTime,tbl_section_costcentres.ActualDuration,tbl_section_costcentres.ActualStartDateTime,tbl_section_costcentres.ActualEndTime,IsNull(tbl_section_costcentres.Qty1Charge,0) as Qty1Charge,IsNull(tbl_section_costcentres.Qty2Charge,0) as Qty2Charge,isnull(tbl_section_costcentres.Qty3Charge,0) as Qty3Charge,tbl_section_costcentres.Qty4Charge,tbl_section_costcentres.Qty5Charge,isnull(tbl_section_costcentres.Qty1MarkUpID,0) as Qty1MarkUpID,isnull(tbl_section_costcentres.Qty2MarkUpID,0) as Qty2MarkUpID,isnull(tbl_section_costcentres.Qty3MarkUpID,0) as Qty3MarkUpID,isnull(tbl_section_costcentres.Qty4MarkUpID,0) as Qty4MarkUpID,isnull(tbl_section_costcentres.Qty5MarkUpID,0) as Qty5MarkUpID,isnull(tbl_section_costcentres.Qty1MarkUpValue,0) as Qty1MarkUpValue,isnull(tbl_section_costcentres.Qty2MarkUpValue,0) as Qty2MarkUpValue,isnull(tbl_section_costcentres.Qty3MarkUpValue,0) as Qty3MarkUpValue,isnull(tbl_section_costcentres.Qty4MarkUpValue,0) as Qty4MarkUpValue,isnull(tbl_section_costcentres.Qty5MarkUpValue,0) as Qty5MarkUpValue,isnull(tbl_section_costcentres.Qty1NetTotal,0) as Qty1NetTotal,isnull(tbl_section_costcentres.Qty2NetTotal,0) as Qty2NetTotal,isnull(tbl_section_costcentres.Qty3NetTotal,0) as Qty3NetTotal,isnull(tbl_section_costcentres.Qty4NetTotal,0) as Qty4NetTotal,isnull(tbl_section_costcentres.Qty5NetTotal,0) as Qty5NetTotal,isnull(tbl_section_costcentres.Qty1EstimatedPlantCost,0) as Qty1EstimatedPlantCost,isnull(tbl_section_costcentres.Qty1EstimatedLabourCost,0) as Qty1EstimatedLabourCost,isnull(tbl_section_costcentres.Qty1EstimatedStockCost,0) as Qty1EstimatedStockCost,isnull(tbl_section_costcentres.Qty1EstimatedTime,0) as Qty1EstimatedTime,isnull(tbl_section_costcentres.Qty1QuotedPlantCharge,0) as Qty1QuotedPlantCharge,isnull(tbl_section_costcentres.Qty1QuotedLabourCharge,0) as Qty1QuotedLabourCharge,
isnull(tbl_section_costcentres.Qty1QuotedStockCharge,0) as Qty1QuotedStockCharge,isnull(tbl_section_costcentres.Qty2EstimatedPlantCost,0) as Qty2EstimatedPlantCost,isnull(tbl_section_costcentres.Qty2EstimatedLabourCost,0) as Qty2EstimatedLabourCost,isnull(tbl_section_costcentres.Qty2EstimatedStockCost,0) as Qty2EstimatedStockCost,isnull(tbl_section_costcentres.Qty2EstimatedTime,0) as Qty2EstimatedTime,isnull(tbl_section_costcentres.Qty2QuotedPlantCharge,0) as Qty2QuotedPlantCharge,isnull(tbl_section_costcentres.Qty2QuotedLabourCharge,0) as Qty2QuotedLabourCharge,isnull(tbl_section_costcentres.Qty2QuotedStockCharge,0) as Qty2QuotedStockCharge,isnull(tbl_section_costcentres.Qty3EstimatedPlantCost,0) as Qty3EstimatedPlantCost,isnull(tbl_section_costcentres.Qty3EstimatedLabourCost,0) as Qty3EstimatedLabourCost,isnull(tbl_section_costcentres.Qty3EstimatedStockCost,0) as Qty3EstimatedStockCost,isnull(tbl_section_costcentres.Qty3EstimatedTime,0) as Qty3EstimatedTime,
isnull(tbl_section_costcentres.Qty3QuotedPlantCharge,0) as Qty3QuotedPlantCharge, isnull(tbl_section_costcentres.Qty3QuotedLabourCharge,0) as Qty3QuotedLabourCharge, isnull(tbl_section_costcentres.Qty3QuotedStockCharge,0) as Qty3QuotedStockCharge,tbl_section_costcentres.Qty4EstimatedPlantCost,tbl_section_costcentres.Qty4EstimatedLabourCost,tbl_section_costcentres.Qty4EstimatedStockCost,tbl_section_costcentres.Qty4EstimatedTime,tbl_section_costcentres.Qty4QuotedPlantCharge,tbl_section_costcentres.Qty4QuotedLabourCharge,tbl_section_costcentres.Qty4QuotedStockCharge,tbl_section_costcentres.Qty5EstimatedPlantCost,tbl_section_costcentres.Qty5EstimatedLabourCost,tbl_section_costcentres.Qty5EstimatedStockCost,tbl_section_costcentres.Qty5EstimatedTime,tbl_section_costcentres.Qty5QuotedPlantCharge,tbl_section_costcentres.Qty5QuotedLabourCharge,tbl_section_costcentres.Qty5QuotedStockCharge,tbl_section_costcentres.ActualPlantCost,tbl_section_costcentres.ActualLabourCost,tbl_section_costcentres.ActualStockCost,isnull(tbl_section_costcentres.Qty1WorkInstructions,'') as Qty1WorkInstructions,isnull(tbl_section_costcentres.Qty2WorkInstructions,'') as Qty2WorkInstructions,isnull(tbl_section_costcentres.Qty3WorkInstructions,'') as Qty3WorkInstructions,tbl_section_costcentres.Qty4WorkInstructions,tbl_section_costcentres.Qty5WorkInstructions,tbl_section_costcentres.IsCostCentreUsedinPurchaseOrder,tbl_section_costcentres.IsMinimumCost,tbl_section_costcentres.SetupTime,tbl_section_costcentres.IsScheduled,tbl_section_costcentres.IsScheduleable,isnull(tbl_section_costcentres.Locked,0) as Locked,tbl_section_costcentres.CostingActualCost,tbl_section_costcentres.CostingActualTime,tbl_section_costcentres.CostingActualQty,tbl_section_costcentres.Name,tbl_section_costcentres.QtyChargeBroker,tbl_section_costcentres.MarkUpValueBroker
FROM tbl_section_costcentres
Inner join tbl_costcentres  on tbl_costcentres.CostCentreID = tbl_section_costcentres.CostCentreID
where tbl_section_costcentres.ItemSectionID = @SectionID 
order by tbl_section_costcentres.[Order] ASC
	RETURN