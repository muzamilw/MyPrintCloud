CREATE PROCEDURE dbo.sp_ItemAttachments_Get_MaxVersionByID
(
@ID int
)
AS
select Max(version) as MaxVersion from tbl_item_attachments where ID=@ID or Parent=@ID
	RETURN