CREATE PROCEDURE dbo.sp_SystemEmail_DeleteRec_FromVar_EmailTable

	(
		@EmailID int
		--@parameter2 datatype OUTPUT
	)

AS
	delete from tbl_system_email_emailsID_and_sectionsID where EmailID=@EmailID
				
	RETURN