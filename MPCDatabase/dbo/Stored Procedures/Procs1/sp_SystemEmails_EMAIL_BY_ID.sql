CREATE PROCEDURE dbo.sp_SystemEmails_EMAIL_BY_ID
(@ID int)
AS
	select * from tbl_system_emails where ID=@ID
	RETURN