
create PROCEDURE [dbo].[sp_Estimation_Update_EstimateValueChanged]
@EstimateID int , @EstimateValueChanged bit
AS
	update tbl_Estimates set EstimateValueChanged=@EstimateValueChanged where EstimateID=@EstimateID
	RETURN