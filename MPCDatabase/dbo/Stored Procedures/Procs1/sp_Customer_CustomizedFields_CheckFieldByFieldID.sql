CREATE PROCEDURE dbo.sp_Customer_CustomizedFields_CheckFieldByFieldID
	(

		@FieldID int
	
		
	)
AS
	select ID from tbl_customizedfieldsdata where FieldID=@FieldID
	RETURN