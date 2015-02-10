CREATE PROCEDURE [dbo].[sp_PipeLine_Get_PipeLineProjection]
	@ProjectionID int

AS
	SELECT tbl_estimate_projection.ProjectionID,
    tbl_estimate_projection.Contactcompanyid,
    tbl_estimate_projection.Amount,tbl_estimate_projection.IsProjectionAlarm,tbl_estimate_projection.AlarmTime,
    tbl_estimate_projection.SuccessChanceID,
    tbl_estimate_projection.SalesPerson,
    tbl_estimate_projection.EstimateDate,tbl_estimate_projection.Notes,
    tbl_success_chance.Description as SuccessChanceDescription,
    tbl_contactcompanies.Name,
    tbl_systemusers.FullName,tbl_estimate_projection.SourceID,tbl_estimate_projection.ProductID
    FROM tbl_systemusers 
    RIGHT OUTER JOIN tbl_estimate_projection ON (tbl_systemusers.SystemUserID = tbl_estimate_projection.SuccessChanceID) 
    LEFT OUTER JOIN tbl_success_chance ON (tbl_estimate_projection.SuccessChanceID = tbl_success_chance.SuccessChanceID) 
    LEFT OUTER JOIN tbl_contactcompanies ON (tbl_estimate_projection.Contactcompanyid = tbl_contactcompanies.Contactcompanyid) 
    WHERE tbl_estimate_projection.ProjectionID = @ProjectionID
	RETURN