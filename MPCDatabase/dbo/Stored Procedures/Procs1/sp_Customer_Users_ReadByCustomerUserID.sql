CREATE PROCEDURE dbo.sp_Customer_Users_ReadByCustomerUserID
	(
	@CustomerUserID int

	)
	AS
	
	Select * from tbl_customerusers where CustomerUserID=@CustomerUserID

	
	RETURN