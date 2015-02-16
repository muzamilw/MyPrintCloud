
Create PROCEDURE [dbo].[sp_Orders_Update_OrderStatus]
(
	@EstimateID int,
	@Order_Status int
)
AS
	update tbl_Estimates set Order_Status=@Order_Status,StatusID=@Order_Status where EstimateID=@EstimateID
	RETURN