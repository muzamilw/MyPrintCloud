﻿CREATE PROCEDURE dbo.sp_ItemAttachments_Get_ChildAttachmentsByItemID
(
@ItemID int
)
AS
	select ID,Parent,FileTitle,isFromCustomer,CustomerID,ContactID,SystemUserID,UploadDate,UploadTime,Version,ItemID,Comments,Type,CAST(IsApproved as CHAR) as IsApproved,FileName,FolderPath,FileType,ContentType,ApproveDate from tbl_item_attachments where ItemID=@ItemID and Parent <> 0 order by  Version Desc
	RETURN