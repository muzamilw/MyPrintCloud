﻿CREATE PROCEDURE dbo.sp_Customer_CustomizedFields_GetCustomerFieldByFieldName
	(
		
		@FieldName varchar(20)=null
		
	)
AS
select * from tbl_customizedfields where fieldName=@FieldName
	RETURN