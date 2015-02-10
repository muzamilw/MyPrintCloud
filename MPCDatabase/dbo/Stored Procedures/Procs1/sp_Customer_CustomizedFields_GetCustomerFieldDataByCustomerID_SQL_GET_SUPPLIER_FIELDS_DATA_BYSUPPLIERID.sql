CREATE PROCEDURE dbo.sp_Customer_CustomizedFields_GetCustomerFieldDataByCustomerID_SQL_GET_SUPPLIER_FIELDS_DATA_BYSUPPLIERID
	(
		@CustomerID int
	)

AS
	select * from tbl_customizedfieldsdata where customerid=@CustomerID and iscustomer=0
	RETURN