

CREATE PROCEDURE [dbo].[usp_GetRealEstateProducts]
	@ContactCompanyID int
AS
BEGIN

 SELECT i.ProductName, pci.CategoryId, i.ProductCode, i.ThumbnailPath, i.ItemId
FROM dbo.Items AS i INNER JOIN ProductCategoryItem pci ON pci.ItemId = i.ItemId INNER JOIN
 dbo.fnc_GetCorporateCategoriesByCompanyID(@ContactCompanyID) AS cp ON cp.ProductCategoryID = pci.CategoryId 
 AND i.ProductType = 6 AND i.IsPublished = 1 AND i.IsEnabled = 1 AND i.IsArchived = 0

END