CREATE PROCEDURE dbo.sp_ItemAttachments_Update_AttachmentByID
(
@ID int,
@Parent int,
@FileTitle text,
@IsFromCustomer smallint,
@CustomerID int,
@ContactID int,
@SystemUserID int,
@UploadDate datetime,
@UploadTime datetime,
@Version int,
@ItemID int,
@Comments text,
@Type varchar(50),
@IsApproved smallint,
@FileName text,
@FolderPath text,
@FileType varchar(50),
@ContentType varchar(50)
)
AS
	update tbl_item_attachments set Parent=@Parent,FileTitle=@FileTitle,IsFromCustomer=@IsFromCustomer,CustomerID=@CustomerID,ContactID=@ContactID,SystemUserID=@SystemUserID,UploadDate=@UploadDate,UploadTime=@UploadTime,Version=@Version,ItemID=@ItemID,Comments=@Comments,Type=@Type,IsApproved=@IsApproved,FileName=@FileName,FolderPath=@FolderPath,FileType=@FileType,ContentType=@ContentType where ID=@ID
	RETURN