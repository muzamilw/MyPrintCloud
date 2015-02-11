CREATE PROCEDURE dbo.sp_Customer_CustomizedFields_Admin_UpdateCustomize
	(

		@FieldID int,
		@FieldName varchar(20)=null,
		@FieldType int	
	
		
	)
AS
    
Update tbl_customizedfields set FieldName=@FieldName,FieldType=@FieldType where FieldID=@FieldID
	
RETURN