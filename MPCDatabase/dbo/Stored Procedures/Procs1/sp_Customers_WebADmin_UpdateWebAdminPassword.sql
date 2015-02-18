CREATE PROCEDURE dbo.sp_Customers_WebADmin_UpdateWebAdminPassword

	(
		@CustomerID int,
		@WebAccessAdminPassword varchar(50)
			
	)
AS
	UPDATE tbl_customers SET 
       tbl_customers.WebAccessAdminPassword=@WebAccessAdminPassword WHERE tbl_customers.CustomerID=@CustomerID

	RETURN