CREATE PROCEDURE dbo.sp_Orders_Get_ItemsFromOrder
(
	@EstimateID int
)
AS
	SELECT tbl_items.ItemID,tbl_items.ItemCode,tbl_items.Title,
	( case tbl_items.JobSelectedQty when 1 then tbl_items.Qty1 when 2 then tbl_items.Qty2 when 3 then tbl_items.Qty3 end ) as Quantity, 
	(case tbl_items.JobSelectedQty when 1 then round(tbl_items.Qty1NetTotal,2) when 2 then round(tbl_items.Qty2NetTotal,2) when 3 then round(tbl_items.Qty3NetTotal,2) end ) as TotalPrice, 
    tbl_items.EstimateDescription,(select count(*) from tbl_item_sections where ItemID = tbl_items.ItemID) as TotalSections
        FROM tbl_items 
        WHERE  (tbl_items.EstimateID = @EstimateID) and (tbl_items.status=2 or tbl_items.status=3)
	RETURN