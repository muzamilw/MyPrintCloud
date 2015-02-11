CREATE PROCEDURE dbo.sp_Customer_CustomizedFields_DeleteCustomize
	(

	
		@FieldID int
	
		
	)
AS
Delete from tbl_customizedfields where FieldID=@FieldID
	RETURN