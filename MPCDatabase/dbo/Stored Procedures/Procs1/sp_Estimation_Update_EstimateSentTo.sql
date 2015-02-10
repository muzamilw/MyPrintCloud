
create PROCEDURE [dbo].[sp_Estimation_Update_EstimateSentTo]
@EstimateID int , @EstimateSentTo smallint
AS
	update tbl_Estimates set EstimateSentTo=@EstimateSentTo where EstimateID=@EstimateID
	RETURN