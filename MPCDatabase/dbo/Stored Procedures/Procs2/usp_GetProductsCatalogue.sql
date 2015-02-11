CREATE PROCEDURE [dbo].[usp_GetProductsCatalogue]

AS
BEGIN
		select   ProductCategoryID,CategoryName, DisplayOrder,
				(select   CategoryName
				from	tbl_ProductCategory
				where CategoryName = 'Stationery Essentials')As HeadStationary
		from	tbl_ProductCategory
		where  ParentCategoryID = 177
		order by DisplayOrder
		
		select   ProductCategoryID,CategoryName, DisplayOrder,
				(select   CategoryName
				from	tbl_ProductCategory
				where CategoryName = 'Marketing Edge')As HeadMarketing
		from	tbl_ProductCategory
		where  ParentCategoryID = 178
		order by DisplayOrder
		
		select   ProductCategoryID,CategoryName, DisplayOrder,
				(select   CategoryName
				from	tbl_ProductCategory
				where CategoryName = 'At The Office ')As HeadOffice
		from	tbl_ProductCategory
		where  ParentCategoryID = 179
		order by DisplayOrder
		
		select   ProductCategoryID,CategoryName, DisplayOrder,
				(select   CategoryName
				from	tbl_ProductCategory
				where CategoryName = 'Personal Touch ')As HeadPersonal
		from	tbl_ProductCategory
		where  ParentCategoryID = 180
		order by DisplayOrder
		
		
		
END