CREATE PROCEDURE dbo.sp_SystemEmail_GetEmailID_and_SectionIDTable
/*
	(
		@parameter1 datatype = default value,
		@parameter2 datatype OUTPUT
	)
*/
AS
	select * from tbl_system_email_emailsID_and_sectionsID
	RETURN