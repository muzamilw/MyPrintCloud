CREATE PROCEDURE dbo.sp_Customer_Category_DeleteCategory_isCategoryUsed
	(
	@CategoryID int
	)
AS
SELECT tbl_finishedgoodpricematrix.CategoryID 
      FROM tbl_finishedgoodpricematrix WHERE tbl_finishedgoodpricematrix.CategoryID=@CategoryID
	RETURN