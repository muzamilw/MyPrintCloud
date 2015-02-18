
create PROCEDURE [dbo].[sp_Estimation_Update_EstimateStatus]
@Status int,
@EstimateID int
AS
	update tbl_Estimates set StatusID=@Status where EstimateID=@EstimateID
	RETURN