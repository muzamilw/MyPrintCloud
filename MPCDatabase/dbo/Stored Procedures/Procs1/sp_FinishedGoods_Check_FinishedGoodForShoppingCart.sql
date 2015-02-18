CREATE PROCEDURE dbo.sp_FinishedGoods_Check_FinishedGoodForShoppingCart

	(
		@FinishedGoodID int
	)

AS
	/* SET NOCOUNT ON */
	
	SELECT ProductID FROM tbl_product_printlink_shoppingcart_detail WHERE (ProductID = @FinishedGoodID)
	
	RETURN