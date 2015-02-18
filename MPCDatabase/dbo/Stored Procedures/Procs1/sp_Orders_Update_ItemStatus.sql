CREATE PROCEDURE dbo.sp_Orders_Update_ItemStatus
(
	@EstimateID int
)
AS
	update tbl_items set Status=1 where EstimateID=@EstimateID
	RETURN