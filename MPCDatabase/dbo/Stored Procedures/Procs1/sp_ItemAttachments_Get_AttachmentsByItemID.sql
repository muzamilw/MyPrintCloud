CREATE PROCEDURE dbo.sp_ItemAttachments_Get_AttachmentsByItemID
(
	@ItemID int
)
AS
	select CAST(ID as smallint) as ID,FileTitle,isFromCustomer,CustomerID,ContactID,SystemUserID,UploadDate,UploadTime,Version,ItemID,Comments,Type,CAST(IsApproved as CHAR) as IsApproved,FileName,FolderPath,FileType,ContentType,ApproveDate,Parent from tbl_item_attachments where ItemID=@ItemID
	RETURN