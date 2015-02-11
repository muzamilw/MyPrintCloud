CREATE PROCEDURE dbo.sp_Jobs_Get_JobDescriptionByJobID
(
	@ItemID int
)
AS
	SELECT tbl_items.ItemID,tbl_items.JobCode,tbl_items.Title 
       FROM tbl_items 
       WHERE tbl_items.ItemID = @ItemID
	RETURN