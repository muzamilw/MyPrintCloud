CREATE PROCEDURE dbo.sp_Customer_Users_CheckCustomerUser
	(
	@username varchar
	)
AS
	Select CustomerID from tbl_customerusers where username=@username

	RETURN