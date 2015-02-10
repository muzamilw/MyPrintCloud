CREATE PROCEDURE dbo.sp_SystemEmails_Update_Email
(
@SystemSiteID int,
@TextBody text,
@Title varchar(200),
@From varchar(200),
@FromEmail varchar(200),
@Subject text,
@Body text,
@ID int)
AS
	UPDATE tbl_system_emails set SystemSiteID=@SystemSiteID, TextBody=@TextBody,Title=@Title,FFrom=@From,
	FromEmail=@FromEmail,Subject=@Subject,Body=@Body where ID=@ID
	RETURN