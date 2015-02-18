
Create PROCEDURE [dbo].[sp_Estimation_Update_EstimateParentID]
@EstimateID int,
@ParentID int

AS
	update tbl_Estimates set ParentID=@ParentID where EstimateID=@EstimateID
	RETURN