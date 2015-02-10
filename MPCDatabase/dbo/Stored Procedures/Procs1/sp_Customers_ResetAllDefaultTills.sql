CREATE PROCEDURE dbo.sp_Customers_ResetAllDefaultTills
/*
	(
		@parameter1 datatype = default value,
		@parameter2 datatype OUTPUT
	)
*/
AS
	UPDATE tbl_customers SET DefaultTill=0
	RETURN