--Exec [usp_ShoppingCart]318730
CREATE PROCEDURE [dbo].[usp_ShoppingCart]
	
	@CustomerID numeric = 0
	

AS
BEGIN
		
		select i.ItemID,e.EstimateID, e.ContactCompanyID, i.ProductName,
				i.Qty1 As ProductQty,i.Qty1BaseCharge1 As ProductPrice, 
				Qty1MarkUp1Value As ProductVAT,
				isnull((dbo.fn_GetAddonsTotal(@CustomerID)),0) As AddOnsPrice,
				(Qty1BaseCharge1 + ISNULL(dbo.fn_GetAddonsTotal(@CustomerID),0))As ProductTotalPrice
				
		from	tbl_items i
			inner join tbl_estimates e on i.EstimateID = e.EstimateID
		where e.StatusID = 3
			and e.ContactCompanyID = @CustomerID


		select	cc.CostCentreID, cc.Name As AddonName, cc.Description,
		 sc.Qty1Charge As AddonPrice, s.ItemID
		from	tbl_section_costcentres sc
			inner join tbl_costcentres cc on cc.CostCentreID = sc.CostCentreID
			inner join tbl_item_sections s on sc.ItemSectionID = s.ItemSectionID
			inner join tbl_items itm on itm.ItemID = s.ItemID
			inner join tbl_estimates es on es.EstimateID = itm.EstimateID
		and		es.ContactCompanyID = @CustomerID
		and cc.CostCentreID <> 202
		

END