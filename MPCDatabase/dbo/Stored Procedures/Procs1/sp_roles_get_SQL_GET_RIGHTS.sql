
CREATE PROCEDURE [dbo].[sp_roles_get_SQL_GET_RIGHTS]
/*
	(
		@parameter1 datatype = default value,
		@parameter2 datatype OUTPUT
	)
*/
AS
	SELECT tbl_rights.RightID,tbl_rights.SectionID,tbl_rights.RightName,tbl_rights.Description FROM tbl_rights order by RightName
	RETURN