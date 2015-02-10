CREATE PROCEDURE dbo.sp_ItemAttachments_Get_AttachmentsByItemIDParentNot0
(
@ItemID int
)
AS
	select ID,(case when Parent=0 then ID else Parent end ) as Parent,FileTitle,isFromCustomer,CustomerID,ContactID,SystemUserID,UploadDate,UploadTime,Version,ItemID,Comments,Type,CAST(IsApproved as CHAR) as IsApproved,FileName,FolderPath,FileType,ContentType,ApproveDate from tbl_item_attachments where ItemID=@ItemID order by  Version Desc
	RETURN