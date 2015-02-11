CREATE PROCEDURE dbo.sp_ItemSection_get_SectionInkCoverageBySectionID
(
@SectionID int
)
AS
	SELECT tbl_section_inkcoverage.ID, tbl_section_inkcoverage.SectionID, tbl_section_inkcoverage.InkOrder,  tbl_section_inkcoverage.InkID,  tbl_section_inkcoverage.CoverageGroupID, tbl_section_inkcoverage.Side FROM tbl_section_inkcoverage  where tbl_section_inkcoverage.SectionID = @SectionID  order by tbl_section_inkcoverage.InkOrder
RETURN