--Exec [[usp_GetProductsSpecialOffers]]
Create PROCEDURE [dbo].[usp_GetProductsSpecialOffers]

As
BEGIN
		select top 9 CategoryName, Description1, Description2, ImagePath, ThumbnailPath 
		from tbl_ProductCategory

END