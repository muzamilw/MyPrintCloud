
-- =============================================
-- Author:		MZ
-- Create date: 
-- Description:	
-- =============================================
CREATE FUNCTION [dbo].[GetTopCategoryID] 
(
	-- Add the parameters for the function here
	@ProductCategoryID int
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result int;
	
	
	WITH ParentCat(ProductCategoryID,parentCategoryid)  
						AS( 
						SELECT ProductCategoryID, parentCategoryid from tbl_ProductCategory child where ProductCategoryID = @ProductCategoryID

						UNION ALL 

						SELECT parent.ProductCategoryID, parent.parentCategoryid
						FROM tbl_ProductCategory parent 
						INNER JOIN ParentCat child ON child.parentCategoryid = parent.ProductCategoryID 
						) 
						
						SELECT @Result = ProductCategoryID
						FROM ParentCat where parentcategoryid is null; 

	

	-- Return the result of the function
	RETURN @Result

END