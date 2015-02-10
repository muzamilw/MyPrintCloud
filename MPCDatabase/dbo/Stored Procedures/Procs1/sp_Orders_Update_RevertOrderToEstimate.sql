
CREATE PROCEDURE [dbo].[sp_Orders_Update_RevertOrderToEstimate]
(
	@EstimateID int,
	@IsEstimate smallint
)
AS
	update tbl_Estimates set IsEstimate=@IsEstimate where EstimateID=@EstimateID
	RETURN