
create PROCEDURE [dbo].[sp_Estimation_Update_ExcludeEstimateFromPipeLine]
(@EstimateID int)
AS
	update tbl_estimates 
	set IsInPipeLine=0 
	where tbl_estimates.EstimateID=@EstimateID /*or ParentID=@EstimateID */
	/*update tbl_estimates set IsInPipeLine=0 where Estimate_ID=@EstimateID or ParentID=@EstimateID or tbl_estimates.Estimate_ID=(select ParentID from tbl_estimates where Estimate_ID=@EstimateID)*/
	RETURN