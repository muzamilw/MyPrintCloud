CREATE PROCEDURE dbo.sp_Customer_CustomizedFields_GetFieldDataByFieldID_SQL_GET_PROSPECT_FIELDDATA_BY_FIELDID
	(

		@CustomerID int,
		@FieldID int
		
	)
	
AS
select * from tbl_customizedfieldsdata where fieldid =@FieldID and iscustomer =2 and customerid =@CustomerID
	RETURN