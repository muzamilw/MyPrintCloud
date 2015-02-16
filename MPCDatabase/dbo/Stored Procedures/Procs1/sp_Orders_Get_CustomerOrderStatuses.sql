CREATE PROCEDURE dbo.sp_Orders_Get_CustomerOrderStatuses
AS
	SELECT     OrderStatusID, OrderStatus as CustomerOrderStatus
FROM         tbl_product_printlink_shoppingcart_status
	RETURN