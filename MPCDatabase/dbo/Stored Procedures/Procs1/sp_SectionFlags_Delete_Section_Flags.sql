CREATE PROCEDURE dbo.sp_SectionFlags_Delete_Section_Flags

	(
		@SectionFlagID integer
		--@parameter2 datatype OUTPUT
	)

AS
	 DELETE FROM tbl_section_flags WHERE SectionFlagID=@SectionFlagID
	RETURN