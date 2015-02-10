
create PROCEDURE [dbo].[sp_Estimation_Get_MaxEstimateVersion]--4890
(@EstimateID int)
AS
	select max(tbl_estimates.version) as Versions 
	from tbl_estimates where (tbl_estimates.ParentID=@EstimateID OR tbl_estimates.EstimateID=@EstimateID)
	RETURN