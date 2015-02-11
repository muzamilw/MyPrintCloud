CREATE PROCEDURE dbo.sp_Customers_WebADmin_UpdateWebAdmin

	(
		@CustomerID int,
		@WebAccessAdminUserName varchar(50),
		@WebAccessAdminPasswordHint varchar(150)
		
	)

AS
	UPDATE tbl_customers SET tbl_customers.WebAccessAdminUserName=@WebAccessAdminUserName,
        tbl_customers.WebAccessAdminPasswordHint=@WebAccessAdminPasswordHint WHERE tbl_customers.CustomerID=@CustomerID

	RETURN