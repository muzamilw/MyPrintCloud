-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetPublicCatalogProductCategories]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   WITH CTE(ProductCategoryID, ParentCategoryID, CategoryName, ContactCompanyID) AS 
			(SELECT     ProductCategoryID, ParentCategoryID, CategoryName,ContactCompanyID FROM 
					(
					SELECT C.ProductCategoryID, C.ParentCategoryID, C.CategoryName, C.ContactCompanyID
					 FROM dbo.tbl_items AS P 
					 INNER JOIN dbo.tbl_ProductCategory AS C ON P.ProductCategoryID = C.ProductCategoryID --AND P.ProductID = 34 
					 group by C.ProductCategoryID, C.ParentCategoryID, C.CategoryName, C.ContactCompanyID
					 ) 
					AS P
			  UNION ALL
					  SELECT     C.ProductCategoryID, C.ParentCategoryID, C.CategoryName, C.ContactCompanyID
					  FROM         CTE AS CTE_2 
					  INNER JOIN dbo.tbl_ProductCategory AS C ON CTE_2.ParentCategoryID = C.ProductCategoryID
			)
									
	SELECT     ProductCategoryID, ISNULL(ParentCategoryID,0) AS ParentCategoryID, CategoryName  , ContactCompanyID
	FROM CTE
 group by ProductCategoryID, ParentCategoryID, CategoryName, ContactCompanyID

END