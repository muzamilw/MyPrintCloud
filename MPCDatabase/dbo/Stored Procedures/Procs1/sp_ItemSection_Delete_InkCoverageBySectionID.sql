CREATE PROCEDURE dbo.sp_ItemSection_Delete_InkCoverageBySectionID
(
	@SectionID int
)
AS
	Delete From tbl_section_inkcoverage WHERE tbl_section_inkcoverage.SectionID = @SectionID
	RETURN