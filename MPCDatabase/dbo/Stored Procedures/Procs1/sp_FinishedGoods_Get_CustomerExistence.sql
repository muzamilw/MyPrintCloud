CREATE PROCEDURE dbo.sp_FinishedGoods_Get_CustomerExistence

	(
		@CustomerID int
	)

AS
	/* SET NOCOUNT ON */
	
	SELECT tbl_itemlibrary_catalogue.CustomerID FROM tbl_itemlibrary_catalogue where tbl_itemlibrary_catalogue.CustomerID=@CustomerID
	
	RETURN