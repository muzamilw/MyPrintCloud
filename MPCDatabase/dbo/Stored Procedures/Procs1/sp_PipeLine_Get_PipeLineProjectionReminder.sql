CREATE PROCEDURE [dbo].[sp_PipeLine_Get_PipeLineProjectionReminder]
	@SalesPerson int, @AlarmTime datetime
AS
	select tbl_estimate_projection.ProjectionID,
    tbl_estimate_projection.ContactCompanyID,
    tbl_estimate_projection.Amount,
    tbl_estimate_projection.SuccessChanceID,tbl_estimate_projection.SalesPerson,
    tbl_estimate_projection.EstimateDate,
    tbl_success_chance.Description as SuccessChanceDescription,tbl_success_chance.Percentage as Percentage,
    tbl_contactcompanies.Name 
    FROM tbl_estimate_projection 
    LEFT OUTER JOIN tbl_success_chance ON (tbl_estimate_projection.SuccessChanceID = tbl_success_chance.SuccessChanceID) 
    LEFT OUTER JOIN tbl_contactcompanies ON (tbl_estimate_projection.ContactCompanyID = tbl_contactcompanies.ContactCompanyID) 
    where  (
			(	/*DAY(alarmdate)<=DAY(@AlarmDate) AND 
                MONTH(alarmdate)<=MONTH(@AlarmDate) AND 
                YEAR(alarmdate)<=YEAR(@AlarmDate) AND 
                DATEPART(HOUR,AlarmTime)<=DATEPART(HOUR,@AlarmTime) and 
                DATEPART(MINUTE,AlarmTime)<=DATEPART(MINUTE,@AlarmTime) and 
                DATEPART(SECOND,AlarmTime)<=DATEPART(SECOND,@AlarmTime) */
                AlarmTime<=@AlarmTime)
     AND tbl_estimate_projection.salesperson=@SalesPerson and IsIncludedInPipeLine<>0 and IsLocked=0 and IsProjectionAlarm<>0)
	RETURN