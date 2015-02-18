
Create PROCEDURE [dbo].[sp_PipeLine_Update_PipeLineProjection]
	@CustomerID int,@ProjectionAmount float,@SuccessChanceID int,@SalesPerson int,@EstimateDate datetime,@AlarmTime datetime,@ProjectionNotes varchar(100),@ProjectionID int,@IsProjectionAlarm bit,@SourceID int,@ProductID int
AS
	update tbl_estimate_projection set ContactCompanyID=@CustomerID,Amount=@ProjectionAmount,SuccessChanceID=@SuccessChanceID,SalesPerson=@SalesPerson,EstimateDate=@EstimateDate,AlarmTime=@AlarmTime,Notes=@ProjectionNotes,IsProjectionAlarm=@IsProjectionAlarm,SourceID=@SourceID,ProductID=@ProductID where (ProjectionID=@ProjectionID)
	RETURN