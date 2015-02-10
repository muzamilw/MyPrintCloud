CREATE PROCEDURE dbo.sp_ItemSection_Insert_SectionInkCoverage
(
	@SectionID int,
	@InkOrder int,
	@InkId int,
	@InkCoveragegroupId int,
	@InkSide int
)
AS
	insert into tbl_section_inkcoverage (SectionID,InkOrder,InkID,CoverageGroupID,Side) VALUES (@SectionID,@InkOrder,@InkId,@InkCoveragegroupId,@InkSide)
RETURN