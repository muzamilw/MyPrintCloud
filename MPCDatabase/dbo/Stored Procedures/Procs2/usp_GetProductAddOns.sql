--Exec [usp_GetProductAddOns]3380
CREATE PROCEDURE [dbo].[usp_GetProductAddOns]--6
(
	@ProductID numeric

)
As
BEGIN
		select iac.ProductAddOnID, iac.ItemID, iac.CostCentreID, ISNULL(iac.DiscountPercentage,0) As DiscountRate,
			cc.Name As AddOnName, cc.PriceDefaultValue as AddOnPrice
			 from tbl_Items_AddonCostCentres iac
			inner join tbl_costcentres cc on iac.CostCentreID = cc.CostCentreID
			where iac.ItemID = @ProductID
		
				
END