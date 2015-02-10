CREATE PROCEDURE dbo.sp_FinishedGoods_Get_PriceMatrixByCustomer

	(
		@CustomerID int,
		@ItemID int,
		@GenCustomerID int
		
	)

AS
	/* SET NOCOUNT ON */
	
	select * from tbl_finishedgoodpricematrix where (CustomerID=@CustomerID or CustomerID=@GenCustomerID) and ItemID=@ItemID
	
	RETURN