CREATE PROCEDURE dbo.sp_Item_Get_ShortItem
(@ItemID int)
AS
	Select IsGroupItem ,ItemType, ItemID,ItemCode,Title,Qty1Tax1Value as VAT ,Qty1BaseCharge1 as Total FROM tbl_items where ItemID=@ItemID
	RETURN