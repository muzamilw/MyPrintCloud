
Create PROCEDURE [dbo].[sp_Estimation_Get_EstimateShortByID]
(@EstimateID int)
AS
	SELECT tbl_estimates.EstimateID,tbl_estimates.Estimate_Code,tbl_estimates.Estimate_Name,tbl_estimates.Version
     FROM tbl_estimates
     WHERE tbl_estimates.EstimateID = @EstimateID
	RETURN