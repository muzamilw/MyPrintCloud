CREATE PROCEDURE dbo.sp_Customer_CustomizedFields_insertCustomize
	(

		@FieldName varchar,
		@FieldType int
	
		
	)
AS
	Insert into tbl_customizedfields (FieldName,FieldType) values (@FieldName,@FieldType)
    
	RETURN