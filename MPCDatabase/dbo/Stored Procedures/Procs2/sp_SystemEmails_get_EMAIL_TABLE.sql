CREATE PROCEDURE dbo.sp_SystemEmails_get_EMAIL_TABLE
(@SystemSiteID int)
AS
	select tbl_system_emails.ID,tbl_system_emails.Title,tbl_system_emails.FFrom,
        tbl_system_emails.FromEmail,tbl_system_emails.Subject,tbl_system_emails.Body,tbl_system_emails.TextBody,tbl_system_emails.LockedBy from tbl_system_emails where SystemSiteID=@SystemSiteID
	RETURN