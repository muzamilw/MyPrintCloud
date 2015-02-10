
create PROCEDURE [dbo].[sp_Estimation_Update_EstimateTotal]
@EstimateID int , @EstimateTotal float
AS
	update tbl_Estimates set Estimate_Total=@EstimateTotal where EstimateID=@EstimateID
	RETURN