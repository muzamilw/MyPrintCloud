
Create PROCEDURE [dbo].[sp_Estimation_Get_ChildEstimatesShortByParentEstimateID]
(@EstimateID int)
AS
	select tbl_estimates.EstimateID,tbl_estimates.Estimate_Code,tbl_estimates.Estimate_Name,tbl_estimates.Version,ParentID from tbl_estimates where ParentID=@EstimateID and IsEstimate<>0 order by tbl_estimates.Version
	RETURN