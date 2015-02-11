CREATE PROCEDURE [dbo].[sp_costcentre_get_costcentrebyid]
(@CostCentreID int)
AS
SELECT tbl_costcentres.IsPrintOnJobCard,tbl_costcentres.IsScheduleable,tbl_costcentres.QuantityQuestionDefaultValue,tbl_costcentres.TimeRunSpeed,tbl_costcentres.TimeNoOfPasses,tbl_costcentres.TimeQuestionDefaultValue,isnull(tbl_costcentres.SystemTypeID,0),tbl_costcentres.ItemDescription,tbl_costcentres.CostCentreID,tbl_costcentres.Name,tbl_costcentres.Description,tbl_costcentres.Type,
         tbl_costcentres.CreatedBy,tbl_costcentres.LockedBy,tbl_costcentres.LastModifiedBy,tbl_costcentres.MinimumCost,tbl_costcentres.SetupCost,
         tbl_costcentres.DefaultVA,tbl_costcentres.SetupTime,tbl_costcentres.DefaultVAId,tbl_costcentres.OverHeadRate,tbl_costcentres.HourlyCharge,
         tbl_costcentres.CostPerThousand,tbl_costcentres.CreationDate,tbl_costcentres.LastModifiedDate,tbl_costcentres.PreferredSupplierID,
         tbl_costcentres.nominalCode,tbl_costcentres.CodeFileName,tbl_costcentres.CompletionTime,tbl_costcentres.strCostPlantUnParsed,tbl_costcentres.strCostLabourUnParsed,
         tbl_costcentres.strCostMaterialUnParsed,tbl_costcentres.strPricePlantUnParsed,tbl_costcentres.strPriceLabourUnParsed,tbl_costcentres.strPriceMaterialUnParsed,
         tbl_costcentres.strActualCostPlantUnParsed,tbl_costcentres.strActualCostLabourUnParsed,tbl_costcentres.strActualCostMaterialUnParsed,
         tbl_costcentres.strTimeUnParsed,tbl_costcentres.IsDisabled,tbl_costcentres.IsDirectCost,tbl_costcentres.SetupSpoilage,tbl_costcentres.RunningSpoilage,
         tbl_costcentres.CalculationMethodType,tbl_costcentres.NoOfHours,tbl_costcentres.PerHourCost,tbl_costcentres.PerHourPrice,
         tbl_costcentres.UnitQuantity,tbl_costcentres.QuantitySourceType,tbl_costcentres.QuantityVariableID,tbl_costcentres.QuantityQuestionString,
         tbl_costcentres.QuantityCalculationString,tbl_costcentres.CostPerUnitQuantity,tbl_costcentres.PricePerUnitQuantity,tbl_costcentres.TimePerUnitQuantity,
         tbl_costcentres.TimeSourceType,tbl_costcentres.TimeVariableID,tbl_costcentres.TimeQuestionString,tbl_costcentres.TimeCalculationString,
         tbl_costcentres.Priority,tbl_costcentres.CostQuestionString,tbl_costcentres.CostDefaultValue,tbl_costcentres.PriceQuestionString,tbl_costcentres.PriceDefaultValue,
         tbl_costcentres.EstimatedTimeQuestionString,tbl_costcentres.EstimatedTimeDefaultValue,tbl_costcentres.Sequence,tbl_contactcompanies.Name FROM tbl_contactcompanies 
         right outer JOIN tbl_costcentres ON (tbl_contactcompanies.contactcompanyid = tbl_costcentres.PreferredSupplierID) where tbl_contactcompanies.iscustomer = 2 and tbl_costcentres.CostCentreID=@CostCentreID
	RETURN