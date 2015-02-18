CREATE PROCEDURE dbo.sp_PipeLine_Get_ProjectionPipeLineByUser
	@SalesPerson int

AS
	select tbl_estimate_projection.ProjectionID as EstimateID,cast(0 as char) as Estimate_Code,0 as Estimate_Name,tbl_estimate_projection.EstimateDate,cast(0 as char) as ItemID,cast(0 as char) as ItemCode,cast(0 as char) as ItemTitle,tbl_customers.CustomerName,tbl_estimate_projection.Amount,tbl_estimate_projection.SuccessChanceID,tbl_success_chance.Percentage,tbl_success_chance.Description as SuccessChanceDescription,1 as IsProjection from tbl_estimate_projection 
    LEFT OUTER JOIN tbl_success_chance ON (tbl_estimate_projection.SuccessChanceID = tbl_success_chance.SuccessChanceID) 
    LEFT OUTER JOIN tbl_customers ON (tbl_estimate_projection.CustomerID = tbl_customers.CustomerID) 
    where tbl_estimate_projection.SalesPerson = @SalesPerson
	RETURN