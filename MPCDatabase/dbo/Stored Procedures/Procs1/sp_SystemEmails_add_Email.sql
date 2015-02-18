CREATE PROCEDURE dbo.sp_SystemEmails_add_Email
(
@SystemSiteID int,
@TextBody text,
@Title varchar(200),
@From varchar(200),
@FromEmail varchar(200),
@Subject text,
@Body text)
AS
	insert into tbl_system_emails (SystemSiteID,TextBody,Title,FFrom,FromEmail,Subject,Body) VALUES 
        (@SystemSiteID,@TextBody,@Title,@From,@FromEmail,@Subject,@Body)
	RETURN