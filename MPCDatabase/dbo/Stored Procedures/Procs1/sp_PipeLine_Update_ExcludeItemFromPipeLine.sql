CREATE PROCEDURE dbo.sp_PipeLine_Update_ExcludeItemFromPipeLine
	@ItemID int
AS
	update tbl_items set IsIncludedInPipeLine = 0 where ItemID=@ItemID
	RETURN