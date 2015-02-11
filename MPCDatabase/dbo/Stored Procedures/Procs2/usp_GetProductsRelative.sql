--Exec [usp_GetProductsRelative]6
CREATE PROCEDURE [dbo].[usp_GetProductsRelative]--6
(
	@ProductID numeric

)
As
BEGIN
				select top 3 cr.*, p.ProductName 
				from tbl_items_RelatedItems cr
				Inner join  tbl_items p on p.ItemID = cr.ItemID
				where cr.ItemID = @ProductID

				

END