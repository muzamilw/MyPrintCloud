
Create PROCEDURE [dbo].[sp_PipeLine_Insert_PipeLineProjection]
	@CustomerID int,@ProjectionAmount float,@SuccessChanceID int,@SalesPerson int,@EstimateDate datetime,@AlarmDate datetime,@AlarmTime datetime,@ProjectionNotes varchar(100),@IsProjectionAlarm bit,@SourceID int,@ProductID int
AS
	insert into tbl_estimate_projection (ContactCompanyID,Amount,SuccessChanceID,SalesPerson,EstimateDate,AlarmDate,AlarmTime,Notes,IsIncludedInPipeLine,IsProjectionAlarm,SourceID,ProductID) VALUES (@CustomerID,@ProjectionAmount,@SuccessChanceID,@SalesPerson,@EstimateDate,@AlarmDate,@AlarmTime,@ProjectionNotes,'1',@IsProjectionAlarm,@SourceID,@ProductID)
	RETURN