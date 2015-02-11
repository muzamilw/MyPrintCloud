CREATE PROCEDURE dbo.sp_Customer_CustomizedFields_GetCustomerFieldDataByCustomerID_SQL_GET_PROSPECT_FIELDS_DATA_BYPROSPECTID
	(
		@CustomerID int
	)
AS
	select * from tbl_customizedfieldsdata where customerid=@CustomerID and iscustomer=2
	RETURN