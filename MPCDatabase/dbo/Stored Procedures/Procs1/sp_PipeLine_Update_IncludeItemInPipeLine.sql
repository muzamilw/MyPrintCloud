CREATE PROCEDURE dbo.sp_PipeLine_Update_IncludeItemInPipeLine
	@ItemID int
AS
	update tbl_items set IsIncludedInPipeLine = 1 where ItemID=@ItemID
	RETURN