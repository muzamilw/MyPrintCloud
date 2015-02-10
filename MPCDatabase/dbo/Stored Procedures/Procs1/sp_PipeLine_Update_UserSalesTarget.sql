CREATE PROCEDURE dbo.sp_PipeLine_Update_UserSalesTarget
	@SalesMonth int,@SalesYear int ,@SalesTarget int,@UserSalesTargetID int
AS
	update tbl_user_sales_targets set Month=@SalesMonth,Year=@SalesYear,SalesTarget=@SalesTarget where (UserSalesTargetID=@UserSalesTargetID)
	RETURN