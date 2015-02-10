CREATE PROCEDURE dbo.sp_Customer_Users_IsWebUserUsed_MYSQL_CHECK_IN_USER_SHOPINGCART
	(
	@CustomerUserID int
	)
AS
	SELECT tbl_product_printlink_shoppingcart.UserID 
        FROM tbl_product_printlink_shoppingcart WHERE tbl_product_printlink_shoppingcart.UserID=@CustomerUserID
       
	RETURN