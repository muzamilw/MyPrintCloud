CREATE PROCEDURE dbo.sp_SectionFlags_FlagID_Used_By_Inventory_Customers
	(
		@flagID int
		--@parameter2 datatype OUTPUT
	)

AS
	 SELECT CustomerID from tbl_customers WHERE FlagID=@flagID
	RETURN