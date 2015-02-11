
create PROCEDURE [dbo].[sp_PipeLine_Get_PipeLineByUserIncludedOnly]
	@SalesPerson int
AS
	SELECT cast(tbl_items.ItemID as char) as ItemID,
    (case when tbl_estimates.IsEstimate<>0 then tbl_estimates.Estimate_Code else tbl_estimates.Order_Code END) as Estimate_Code,    
    tbl_estimates.Estimate_Name,tbl_estimates.ProjectionDate,tbl_items.EstimateID,tbl_items.ItemCode,
    tbl_items.Title AS ItemTitle,tbl_contactcompanies.Name, 
    (case when tbl_estimates.IsEstimate =1 then tbl_items.Qty1NetTotal else (case when tbl_items.jobselectedqty=1 then tbl_items.Qty1NetTotal when tbl_items.jobselectedqty=2 then tbl_items.Qty2NetTotal when tbl_items.jobselectedqty=3 then tbl_items.Qty3NetTotal else 0 END) END )AS Amount,
    tbl_estimates.SuccessChanceID,tbl_success_chance.Percentage,tbl_success_chance.Description as SuccessChanceDescription,/*0 as IsProjection*/(CASE WHEN tbl_estimates.IsEstimate<>0 THEN 0 ELSE 2 END)as IsProjection,tbl_items.IsIncludedInPipeLine as IsIncludedInPipeLine,tbl_estimates.SourceID,tbl_estimates.ProductID 
    FROM tbl_estimates 
    LEFT OUTER JOIN tbl_success_chance ON (tbl_estimates.SuccessChanceID = tbl_success_chance.SuccessChanceID) 
    INNER JOIN tbl_items ON (tbl_estimates.EstimateID = tbl_items.EstimateID) 
    INNER JOIN tbl_contactcompanies ON (tbl_estimates.ContactCompanyID = tbl_contactcompanies.ContactCompanyID) 
    WHERE (tbl_items.Status =1 or tbl_items.Status = 2) and tbl_estimates.IsInPipeLine <> 0 
    AND tbl_items.IsIncludedinPipeLine<>0
	and tbl_estimates.SalesPersonID = @SalesPerson
     UNION 
    select tbl_estimate_projection.ProjectionID as ItemID,'0' as Estimate_Code,'0' as Estimate_Name,tbl_estimate_projection.EstimateDate as ProjectionDate,0 as EstimateID,'0' as ItemCode,'0' as ItemTitle,tbl_contactcompanies.Name,tbl_estimate_projection.Amount,tbl_estimate_projection.SuccessChanceID,tbl_success_chance.Percentage,tbl_success_chance.Description as SuccessChanceDescription,1 as IsProjection,tbl_estimate_projection.IsIncludedInPipeLine as IsIncludedInPipeLine,tbl_estimate_projection.SourceID,tbl_estimate_projection.ProductID from tbl_estimate_projection 
    LEFT OUTER JOIN tbl_success_chance ON (tbl_estimate_projection.SuccessChanceID = tbl_success_chance.SuccessChanceID) 
    LEFT OUTER JOIN tbl_contactcompanies ON (tbl_estimate_projection.ContactCompanyID = tbl_contactcompanies.ContactCompanyID) 
    where tbl_estimate_projection.SalesPerson = @SalesPerson and tbl_estimate_projection.IsIncludedInPipeLine <> 0 ORDER BY Estimate_Code
	RETURN