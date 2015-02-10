CREATE PROCEDURE dbo.sp_ItemAttachments_INSERT_Attachment
(
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
insert into tbl_item_attachments 
(Parent,FileTitle,isFromCustomer,CustomerID,ContactID,SystemUserID,UploadDate,UploadTime,Version,ItemID,Comments,Type,IsApproved,FileName,FolderPath,FileType,ContentType) VALUES 
(@Parent,@FileTitle,@IsFromCustomer,@CustomerID,@ContactID,@SystemUserID,@UploadDate,@UploadTime,@Version,@ItemID,@Comments,@Type,@IsApproved,@FileName,@FolderPath,@FileType,@ContentType);select @@Identity as ID
	RETURN