CREATE PROCEDURE dbo.sp_Customer_CustomizedFields_GetCustomerFieldDataByCustomerID_SQL_GET_CUSTOMER_FIELDS_DATA_BYCUSTOMERID
	(
		@CustomerID int
	)

AS
	select * from tbl_customizedfieldsdata 
       inner join tbl_customizedfields on tbl_customizedfields.fieldid=tbl_customizedfieldsdata.fieldid 
       where tbl_customizedfieldsdata.customerid=@CustomerID and tbl_customizedfieldsdata.ISCustomer=1
	RETURN