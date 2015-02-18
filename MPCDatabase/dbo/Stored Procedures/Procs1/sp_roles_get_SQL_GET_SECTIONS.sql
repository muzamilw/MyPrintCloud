CREATE PROCEDURE dbo.sp_roles_get_SQL_GET_SECTIONS
/*
	(
		@parameter1 datatype = default value,
		@parameter2 datatype OUTPUT
	)
*/
AS
	SELECT tbl_sections.SectionID,tbl_sections.SectionName,tbl_sections.SecOrder,tbl_sections.ParentID,tbl_sections.href,tbl_sections.SectionImage FROM tbl_sections
	RETURN