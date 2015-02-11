CREATE PROCEDURE dbo.sp_Customer_CustomizedFields_GetCustomerFieldsByCustomerID
	(
		@CustomerID int
	)
AS
	select * from tbl_customizedfields 
          left outer join tbl_customizedfieldsdata on tbl_customizedfields.FieldID=tbl_customizedfieldsdata.FieldID 
          where tbl_customizedfieldsdata.customerid=@CustomerID or customerid is null
	RETURN