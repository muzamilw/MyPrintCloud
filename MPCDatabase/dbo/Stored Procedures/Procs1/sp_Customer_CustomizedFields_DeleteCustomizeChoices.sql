CREATE PROCEDURE dbo.sp_Customer_CustomizedFields_DeleteCustomizeChoices
	(

		@FieldID int
	
		
	)
    AS
delete from tbl_customizedfieldsvalues where FieldID=@FieldID
	RETURN