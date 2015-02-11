
CREATE PROCEDURE [dbo].[sp_Estimation_Get_EstimateParentID]
@EstimateID int
AS
	select (case when ParentID=0 then EstimateID else isnull(ParentID,0) end) as EstimateID from tbl_estimates where EstimateID=@EstimateID and IsEstimate<>0
	RETURN