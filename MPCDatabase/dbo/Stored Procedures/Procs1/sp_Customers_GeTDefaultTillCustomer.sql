CREATE PROCEDURE dbo.sp_Customers_GeTDefaultTillCustomer

AS
	SELECT * FROM tbl_customers WHERE DefaultTill<>0
	RETURN