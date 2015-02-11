CREATE PROCEDURE [dbo].[usp_InsertShoppingCartItems]
		
		@ProductID		numeric,
		@CustomerID		numeric,
		@ProductName	varchar(50),
		@ProductQty		int,
		@ProductVAT		numeric(18,2),
		@ProductPrice	numeric(18,2),
		@AddonName		varchar(1000),
		@AddonPrice		numeric(18,2),
		@ProductTotalPrice numeric(18,2),
		@isCheckOut		bit



AS
BEGIN
		
		INSERT INTO webShoppingCart
			(ItemID, CustomerID, ProductName, ProductQty, ProductVAT, 
			 productPrice, AddOnsName, AddOnsPrice,ProductTotalPrice, ShoppingDate, isCheckout)
		VALUES
			(@ProductID, @CustomerID, @ProductName, @ProductQty,
			 @ProductVAT, @ProductPrice, @AddonName, @AddonPrice,@ProductTotalPrice, getdate(), @isCheckOut)
END