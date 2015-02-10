CREATE PROCEDURE [dbo].[sp_SystemEmails_get_SystemVariables]
/*
	(
		--@SectionID int
		--@parameter2 datatype OUTPUT
	)
*/
AS
	select * from tbl_Campaign_email_variables
	RETURN