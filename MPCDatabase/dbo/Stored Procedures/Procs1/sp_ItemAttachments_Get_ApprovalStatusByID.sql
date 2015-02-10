CREATE PROCEDURE dbo.sp_ItemAttachments_Get_ApprovalStatusByID
(
@ID int
)
AS
SELECT tbl_item_attachments.ID  FROM tbl_item_attachments 
 WHERE ((ID=@ID or Parent=@ID) and  (tbl_item_attachments.IsApproved <> 0))
	RETURN