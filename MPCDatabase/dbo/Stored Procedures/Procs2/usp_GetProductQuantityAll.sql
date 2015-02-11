--Exec [usp_GetProductQuantityAll]3380
CREATE PROCEDURE [dbo].[usp_GetProductQuantityAll]--6
(
	@ProductID numeric

)
As
BEGIN
				select PriceMatrixID, isnull(Quantity,0) As Quantity,
					ISNULL(Price, 0) As Price, ItemID, 
					ISNULL(PricePaperType1, 0) As PricePaperType1,
					ISNULL(PricePaperType2, 0) As PricePaperType2,
					ISNULL(PricePaperType3, 0) As PricePaperType3,
					ISNULL(IsDiscounted, 0) As isDiscount
				from	tbl_items_PriceMatrix
				where	ItemID = @ProductID

END