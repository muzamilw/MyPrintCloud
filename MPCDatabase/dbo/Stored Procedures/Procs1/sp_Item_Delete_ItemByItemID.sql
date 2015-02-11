CREATE PROCEDURE dbo.sp_Item_Delete_ItemByItemID
	(@ItemID int)
AS
	Delete from tbl_items where ItemID=@ItemID
	RETURN