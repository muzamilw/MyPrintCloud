CREATE PROCEDURE dbo.sp_Customer_CustomizedFields_GetFieldDataByFieldID_SQL_GET_SUPPLIER_FIELDDATA_BY_FIELDID
	(

		@CustomerID int,
		@FieldID int
		
	)
	as
select * from tbl_customizedfieldsdata where fieldid =@FieldID and iscustomer =0 and customerid =@CustomerID
	RETURN