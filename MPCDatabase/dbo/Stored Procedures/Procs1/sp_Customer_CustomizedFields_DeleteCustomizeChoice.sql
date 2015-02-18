CREATE PROCEDURE dbo.sp_Customer_CustomizedFields_DeleteCustomizeChoice
	(

		@ID int
	
		
	)
    AS
	
	delete from tbl_customizedfieldsvalues where ID=@ID
	
	RETURN