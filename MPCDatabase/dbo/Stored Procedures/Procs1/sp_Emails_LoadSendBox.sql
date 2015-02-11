CREATE PROCEDURE dbo.sp_Emails_LoadSendBox
/*
	(
		@parameter1 datatype = default value,
		@parameter2 datatype OUTPUT
	)
*/
AS

select * from tbl_Emails_MailBox where isdeliverd=0 and errorresponse is null

	/* SET NOCOUNT ON */
	RETURN