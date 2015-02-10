
Create PROCEDURE [dbo].[sp_PipeLine_Get_TargetInprogressAmountByUser]
	@SalesPerson	int,
	@StartDate datetime,
	@EndDate datetime
AS
	SELECT Sum(CASE WHEN tbl_items.jobselectedQty=1 then tbl_items.Qty1NetTotal WHEN tbl_items.jobselectedQty=	2 THEN tbl_items.Qty2NetTotal WHEN tbl_items.jobselectedQty=3 THEN tbl_items.Qty3NetTotal ELSE 0 END ) as QtyNetTotal 
    FROM tbl_items 
    LEFT OUTER JOIN tbl_estimates ON (tbl_items.EstimateID = tbl_estimates.EstimateID) 
    WHERE tbl_estimates.Created_By = @SalesPerson And tbl_items.Status = 3 And 
    tbl_items.jobcreationdatetime between @StartDate and @EndDate
	RETURN