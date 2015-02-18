-- =============================================
-- Author:		Muhammad Naveed
-- Create date: 08/01/2011
-- Description:	Get Total Amount of AddOns by Customer
-- =============================================
CREATE FUNCTION [dbo].[fn_GetAddonsTotal]
(
	@CustomerID		numeric
)
RETURNS float
AS
BEGIN
	-- Declare the return variable here
	DECLARE @AddOnsTotal	float

	select @AddOnsTotal = SUM(sc.Qty1Charge) 
	from	tbl_section_costcentres sc
				inner join tbl_item_sections s on sc.ItemSectionID = s.ItemSectionID
				inner join tbl_items itm on itm.ItemID = s.ItemID
				inner join tbl_estimates es on es.EstimateID = itm.EstimateID
				and es.ContactCompanyID = @CustomerID
				and sc.CostCentreID <> 202
	
	-- Return the result of the function
	RETURN @AddOnsTotal

END