CREATE PROCEDURE dbo.sp_Item_Get_ItemForDeliveryNote
(
	@ItemID int
)
AS
SELECT tbl_items.ItemID,tbl_items.ItemCode,tbl_items.Title,
(case tbl_items.JobSelectedQty when 1 then tbl_items.Qty1 when 2 then tbl_items.Qty2 else tbl_items.Qty3 end)  as Quantity,
(case tbl_items.JobSelectedQty when 1  then tbl_items.Qty1GrossTotal when 2 then tbl_items.Qty2grossTotal  else tbl_items.Qty3GrossTotal END) as TotalPrice,
dbo.Fnc_Items_getEstimateDescription(tbl_items.ItemID) as  EstimateDescription 
FROM tbl_items WHERE  tbl_items.ItemID = @ItemID


	RETURN