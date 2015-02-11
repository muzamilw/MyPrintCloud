CREATE PROCEDURE dbo.sp_Customer_CustomizedFields_checkRowByFieldName
	(

		@FieldName varchar(20)=null
	
		
	)
	
AS
Select FieldID from tbl_customizedfields where FieldName = @FieldName
     
	RETURN