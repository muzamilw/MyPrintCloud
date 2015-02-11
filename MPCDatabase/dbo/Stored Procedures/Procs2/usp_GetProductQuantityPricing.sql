--Exec [usp_GetProductQuantityPricing]3380

CREATE PROCEDURE [dbo].[usp_GetProductQuantityPricing]
	@ProductID numeric
	

AS
BEGIN
				
			select top 5 PriceMatrixID, isnull(Quantity,0) As Quantity,
					ISNULL(Price, 0) As Price, ItemID, 
					ISNULL(PricePaperType1, 0) As PricePaperType1,
					ISNULL(PricePaperType2, 0) As PricePaperType2,
					ISNULL(PricePaperType3, 0) As PricePaperType3,
					ISNULL(IsDiscounted, 0) As isDiscount
			from	tbl_items_PriceMatrix
			where	ItemID = @ProductID

			select	iac.ProductAddOnID, iac.ItemID, iac.CostCentreID,
					ISNULL(iac.DiscountPercentage, 0) as DiscountRate,
					cc.Name As AddOnName, cc.PriceDefaultValue as AddOnPrice
			from tbl_Items_AddonCostCentres iac
				inner join tbl_costcentres cc on iac.CostCentreID = cc.CostCentreID
			where iac.ItemID = @ProductID
		
			select	itm.ProductSpecification, CompleteSpecification, DesignGuideLines
			from	tbl_items itm 
			where	itm.ItemID = @ProductID
			
		

END