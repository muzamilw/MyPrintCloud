CREATE PROCEDURE dbo.sp_SystemEmail_SaveVariablesOfEmail

	(
		@EmailVariableID int,
		@EmailID int
		--@parameter2 datatype OUTPUT
	)

AS
	INSERT into tbl_system_email_emailsID_and_sectionsID values
	(@EmailVariableID, @EmailID)
	RETURN