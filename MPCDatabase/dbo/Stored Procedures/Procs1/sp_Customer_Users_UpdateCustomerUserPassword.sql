CREATE PROCEDURE dbo.sp_Customer_Users_UpdateCustomerUserPassword
	(
	@CustomerUserID int,
	@Password varchar(50)
	)
	
	
AS
	Update tbl_customerusers SET Password=@Password where CustomerUserID=@CustomerUserID

	RETURN