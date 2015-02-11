CREATE PROCEDURE dbo.sp_Item_Get_ItemForFinishedGood
(
@ItemID int
)
AS
	SELECT tbl_items.ItemID,tbl_items.ItemCode,tbl_items.Title,
        tbl_items.Qty1,tbl_items.Qty2,tbl_items.Qty3, 
        tbl_items.Qty1NetTotal,tbl_items.Qty2NetTotal,tbl_items.Qty3NetTotal, 
        tbl_items.EstimateDescription,tbl_items.IsMultipleQty,tbl_items.IsRunOnQty,tbl_items.RunOnQty 
        FROM tbl_items WHERE  tbl_items.ItemID = @ItemID
	RETURN