
create PROCEDURE [dbo].[sp_Estimation_Update_EstiamteToParent]
@EstimateID int
AS
	update tbl_Estimates set ParentID=0 where EstimateID=@EstimateID
	RETURN