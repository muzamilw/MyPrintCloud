-- =============================================
-- Author:		Muhammad Naveed
-- Create date: 04/12/2012
-- Description:	Get Product subcategory count
-- =============================================

CREATE FUNCTION [dbo].[fn_GetSubCategoryCount]
(
	@CategoryID		int
)
RETURNS float
AS
BEGIN
	-- Declare the return variable here
	DECLARE @subCategoryCount	int
	
	select @subCategoryCount = count(ProductCategoryID)
		 from tbl_ProductCategory
						where ParentCategoryID = @CategoryID
						
	-- Return the result of the function
	RETURN @subCategoryCount

END