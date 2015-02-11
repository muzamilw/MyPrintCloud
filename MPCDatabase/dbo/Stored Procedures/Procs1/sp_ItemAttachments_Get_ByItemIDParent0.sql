CREATE PROCEDURE dbo.sp_ItemAttachments_Get_ByItemIDParent0
(
	@ItemID int
)
AS
	select ID as ID,FileTitle,isFromCustomer,CustomerID,ContactID,SystemUserID,UploadDate,UploadTime,Version,ItemID,Comments,Type,CAST(IsApproved as CHAR) as IsApproved,FileName,FolderPath,FileType,ContentType,ApproveDate,Parent from tbl_item_attachments where ItemID=@ItemID and Parent=0
	RETURN