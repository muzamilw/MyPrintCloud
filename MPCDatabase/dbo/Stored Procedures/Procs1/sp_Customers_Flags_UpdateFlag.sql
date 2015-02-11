CREATE PROCEDURE dbo.sp_Customers_Flags_UpdateFlag
	(
		@FlagID int,
		@CustomerID int
	)

AS
	UPDATE tbl_customers SET FlagID=@FlagID WHERE CustomerID=@CustomerID

	RETURN