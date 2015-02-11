CREATE PROCEDURE [dbo].[usp_GetMinPriceAndDiscount]
@ItemID int = 0  

AS
BEGIN  
		Declare @Price float = dbo.funGetMiniumProductValue(@ItemID)
		select IsDiscounted, @Price as MinPrice
		from	tbl_items_PriceMatrix
		where ItemID = @ItemID and SupplierID Is NULL and
		(PricePaperType1 = @Price or 
		PricePaperType2 = @Price or
		PricePaperType3 = @Price or
		PriceStockType4 = @Price or
		PriceStockType5 = @Price or
		PriceStockType6 = @Price or
		PriceStockType7 = @Price or
		PriceStockType8 = @Price or
		PriceStockType9 = @Price or
		PriceStockType10 = @Price or
		PriceStockType11 = @Price
		)
		
END