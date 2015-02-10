CREATE PROCEDURE dbo.sp_PipeLine_Get_MonthlySalesTargetsByAllUsers
	@SalesYear int	
AS
	SELECT tbl_user_sales_targets.UserSalesTargetID,
    tbl_user_sales_targets.Month,tbl_user_sales_targets.Year,
    tbl_user_sales_targets.SalesTarget,
    tbl_user_sales_targets.SalesTargetTypeID,
    tbl_user_sales_targets.SystemUserID,
    tbl_sales_target_types.Description,
    tbl_sales_target_types.Duration 
    FROM tbl_user_sales_targets 
    INNER JOIN tbl_sales_target_types ON (tbl_user_sales_targets.SalesTargetTypeID = tbl_sales_target_types.SalesTargetTypeID) 
    WHERE 
    tbl_user_sales_targets.Year=@SalesYear and tbl_user_sales_targets.SalesTargetTypeID=1 order by tbl_user_sales_targets.Month asc
	RETURN