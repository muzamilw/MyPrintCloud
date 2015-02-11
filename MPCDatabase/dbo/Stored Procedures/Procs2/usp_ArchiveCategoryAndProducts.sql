-- =============================================
-- Author:		Muhammad Naveed
-- Create date: December 19, 2013
-- Description:	Archive All Categories, Sub Categories and Products of Given Category
-- =============================================
create PROCEDURE [dbo].[usp_ArchiveCategoryAndProducts]
	@CategoryID	int	
AS
BEGIN
		
	-- Archive All Categories and Sub Categories
	update	tbl_ProductCategory Set isArchived = 1 
			where ProductCategoryID in(select productCategoryID from fnc_GetChildCategories(@CategoryID))
			
	--- Archive All Products Under these Categories
	
	update	tbl_items Set isArchived = 1 
			where ProductCategoryID in(select productCategoryID from fnc_GetChildCategories(@CategoryID))
		
END