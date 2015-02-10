
CREATE PROCEDURE [dbo].[sp_ItemAttachments_Update_AttachmentPathProduction]
@EstimateID int,
@FolderPath varchar(100)
AS
	update tbl_item_attachments 
	set FolderPath= @FolderPath
	where itemid in (select itemid from tbl_items where estimateid = @EstimateID)