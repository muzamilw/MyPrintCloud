CREATE PROCEDURE dbo.sp_PipeLine_Delete_UserSalesTarget
	@UserSalesTargetID int
AS
	delete from tbl_user_sales_targets where UserSalesTargetID = @UserSalesTargetID
	RETURN