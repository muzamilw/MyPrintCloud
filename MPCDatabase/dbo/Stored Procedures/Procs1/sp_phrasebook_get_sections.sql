CREATE PROCEDURE dbo.sp_phrasebook_get_sections
/*
	(
		@parameter1 datatype = default value,
		@parameter2 datatype OUTPUT
	)
*/
AS
	select SectionID,SectionName from tbl_sections where ParentID=0
	RETURN