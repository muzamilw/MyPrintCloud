CREATE PROCEDURE dbo.sp_ItemAttachments_Delete_ByItemID
@ItemID int
AS
	delete from tbl_item_attachments where ItemID=@ItemID
RETURN