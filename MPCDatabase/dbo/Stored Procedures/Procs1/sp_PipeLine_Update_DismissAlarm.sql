CREATE PROCEDURE dbo.sp_PipeLine_Update_DismissAlarm
	@ProjectionID int
AS
	UPDATE tbl_estimate_projection
    SET IsProjectionAlarm=0 
    WHERE ProjectionID=@ProjectionID
	RETURN