
create PROCEDURE [dbo].[sp_ItemSectionCostCentre_Update_ItemSectionCostCentre]
(
@SectionCostcentreID int,
@IsMinimumCost smallint,
@Status int,
@SectionID int,
@CostCentreID int,
@CostCentreType int,
@Order int,
@IsDirectCost smallint,
@IsOptionalExtra smallint,
@IsPurchaseOrderRaised  smallint,
@IsPrintable smallint,
@EstimatedStartTime datetime,
@EstimatedDuration float,
@EstimatedEndTime datetime,
@Qty1Charge float,
@Qty2Charge float,
@Qty3Charge float,
@Qty1MarkUpID int,
@Qty2MarkUpID int,
@Qty3MarkUpID int,
@Qty1MarkUpValue float,
@Qty2MarkUpValue float,
@Qty3MarkUpValue float,
@Qty1NetTotal float,
@Qty2NetTotal float,
@Qty3NetTotal float,
@Qty1EstimatedPlantCost float,
@Qty1EstimatedLabourCost float,
@Qty1EstimatedStockCost float,
@Qty1QuotedPlantCharge float,
@Qty1QuotedLabourCharge float,
@Qty1QuotedStockCharge float,
@Qty2EstimatedPlantCost float,
@Qty2EstimatedLabourCost float,
@Qty2EstimatedStockCost float,
@Qty2QuotedPlantCharge float,
@Qty2QuotedLabourCharge float,
@Qty2QuotedStockCharge float,
@Qty3EstimatedPlantCost float,
@Qty3EstimatedLabourCost float,
@Qty3EstimatedStockCost float,
@Qty3QuotedPlantCharge float,
@Qty3QuotedLabourCharge float,
@Qty3QuotedStockCharge float,
@Qty1WorkInstructions text,
@Qty2WorkInstructions text,
@Qty3WorkInstructions text,
@Qty1EstimatedTime float,
@Qty2EstimatedTime float,
@Qty3EstimatedTime float,
@SystemCostCentreType int,
@SetUpTime float,
@Locked bit,
@CostingActualCost float,
@CostingActualTime float,
@CostingActualQty float
)
AS
Update tbl_section_costcentres set IsMinimumCost=@IsMinimumCost,Status=@Status,ItemSectionID=@SectionID,CostCentreID=@CostCentreID,CostCentreType=@CostCentreType,[Order]= @Order,IsDirectCost=@IsDirectCost,IsOptionalExtra=@IsOptionalExtra
,IsPurchaseOrderRaised=@IsPurchaseOrderRaised,IsPrintable=@IsPrintable,EstimatedStartTime=@EstimatedStartTime,EstimatedDuration=@EstimatedDuration,EstimatedEndTime=@EstimatedEndTime,Qty1Charge=@Qty1Charge,Qty2Charge=@Qty2Charge,Qty3Charge=@Qty3Charge,Qty1MarkUpID=@Qty1MarkUpID,Qty2MarkUpID=@Qty2MarkUpID,Qty3MarkUpID=@Qty3MarkUpID
,Qty1MarkUpValue=@Qty1MarkUpValue,Qty2MarkUpValue=@Qty2MarkUpValue,Qty3MarkUpValue=@Qty3MarkUpValue,Qty1NetTotal=@Qty1NetTotal,Qty2NetTotal=@Qty2NetTotal,Qty3NetTotal=@Qty3NetTotal,Qty1EstimatedPlantCost=@Qty1EstimatedPlantCost,Qty1EstimatedLabourCost=@Qty1EstimatedLabourCost,Qty1EstimatedStockCost=@Qty1EstimatedStockCost 
,Qty1QuotedPlantCharge=@Qty1QuotedPlantCharge,Qty1QuotedLabourCharge=@Qty1QuotedLabourCharge,Qty1QuotedStockCharge=@Qty1QuotedStockCharge,Qty2EstimatedPlantCost=@Qty2EstimatedPlantCost,Qty2EstimatedLabourCost=@Qty2EstimatedLabourCost,Qty2EstimatedStockCost=@Qty2EstimatedStockCost,Qty2QuotedPlantCharge=@Qty2QuotedPlantCharge
,Qty2QuotedLabourCharge=@Qty2QuotedLabourCharge,Qty2QuotedStockCharge=@Qty2QuotedStockCharge,Qty3EstimatedPlantCost=@Qty3EstimatedPlantCost,Qty3EstimatedLabourCost=@Qty3EstimatedLabourCost,Qty3EstimatedStockCost=@Qty3EstimatedStockCost,Qty3QuotedPlantCharge=@Qty3QuotedPlantCharge,Qty3QuotedLabourCharge=@Qty3QuotedLabourCharge 
,Qty3QuotedStockCharge=@Qty3QuotedStockCharge,Qty1WorkInstructions=@Qty1WorkInstructions,Qty2WorkInstructions=@Qty2WorkInstructions,Qty3WorkInstructions=@Qty3WorkInstructions,Qty1EstimatedTime=@Qty1EstimatedTime,Qty2EstimatedTime=@Qty2EstimatedTime,Qty3EstimatedTime=@Qty3EstimatedTime,SystemCostCentreType=@SystemCostCentreType 
,SetUpTime =@SetUpTime, Locked=@Locked,CostingActualCost=@CostingActualCost,CostingActualTime=@CostingActualTime,CostingActualQty=@CostingActualQty where SectionCostcentreID=@SectionCostcentreID 
	RETURN