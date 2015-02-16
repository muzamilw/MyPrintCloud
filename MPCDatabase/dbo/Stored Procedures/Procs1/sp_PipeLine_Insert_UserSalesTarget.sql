CREATE PROCEDURE dbo.sp_PipeLine_Insert_UserSalesTarget
	@SalesTargetTypeID int ,@SystemUserID int,@SalesMonth int,@SalesYear int,@SalesTarget int
AS
	insert into tbl_user_sales_targets (SalesTargetTypeID,SystemUserID,Month,Year,SalesTarget) VALUES (@SalesTargetTypeID,@SystemUserID,@SalesMonth,@SalesYear,@SalesTarget)
	RETURN