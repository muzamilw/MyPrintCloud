CREATE PROCEDURE dbo.sp_test 
	
	@AccountNo int 	
	
AS

Select @AccountNo from tbl_chartofaccount

RETURN