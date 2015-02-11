
create PROCEDURE [dbo].[sp_Estimation_Delete_Estimate]
(@EstimateID int)
AS
	Delete from tbl_estimates where EstimateID=@EstimateID
	RETURN