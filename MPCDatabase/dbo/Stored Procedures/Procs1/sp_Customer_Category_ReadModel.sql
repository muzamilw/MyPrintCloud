CREATE PROCEDURE dbo.sp_Customer_Category_ReadModel
	(
	@category_id int
	)
	
AS
	SELECT tbl_finishgood_categories.ID,tbl_finishgood_categories.ItemLibrarayGroupName,tbl_finishgood_categories.Image,
        tbl_finishgood_categories.Thumbnail,tbl_finishgood_categories.ContentType,tbl_finishgood_categories.Description1,tbl_finishgood_categories.Description2,
        tbl_finishgood_categories.LockedBy,tbl_finishgood_categories.CustomerID,tbl_finishgood_categories.ParentID,ParentTable.ItemLibrarayGroupName as ParentName
         FROM tbl_finishgood_categories 
         LEFT OUTER JOIN tbl_finishgood_categories ParentTable ON (tbl_finishgood_categories.ID = ParentTable.ParentID) 
         WHERE tbl_finishgood_categories.ID = @category_id
	RETURN