CREATE PROCEDURE dbo.sp_Customer_Users_ReadAll
	
	
AS
	select * from tbl_customerusers
	RETURN