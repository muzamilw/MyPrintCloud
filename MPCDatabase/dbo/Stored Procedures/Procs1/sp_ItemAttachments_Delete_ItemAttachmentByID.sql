CREATE PROCEDURE dbo.sp_ItemAttachments_Delete_ItemAttachmentByID
@ID int
AS
	delete from tbl_item_attachments where ID=@ID
	RETURN