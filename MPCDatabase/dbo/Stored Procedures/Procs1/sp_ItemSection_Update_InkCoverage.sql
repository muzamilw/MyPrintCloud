CREATE PROCEDURE dbo.sp_ItemSection_Update_InkCoverage
(
	@SectionID int,
	@InkOrder int,
	@InkID int,
	@InkCoveragegroupId int,
	@InkSide int,
	@ID int
)
AS
	Update tbl_section_inkcoverage set SectionID=@SectionID,InkOrder=@InkOrder,InkID=@InkID,CoverageGroupID=@InkCoveragegroupId,Side=@InkSide where id = @ID
	
RETURN