CREATE PROCEDURE dbo.sp_SectionFlags_Get_Parent_Sections
/*
	(
		@parameter1 datatype = default value,
		@parameter2 datatype OUTPUT
	)
*/
AS
	 SELECT * FROM tbl_sections where tbl_sections.Independent = 1
	RETURN