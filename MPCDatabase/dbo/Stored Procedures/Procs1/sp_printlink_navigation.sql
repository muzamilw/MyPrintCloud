CREATE PROCEDURE dbo.sp_printlink_navigation
(
@category_id int,
@CatalogueID int,
@ProductID int
)
AS
SET NOCOUNT ON
DECLARE @ParentCatID int,@tempParentID int

SELECT @ParentCatID=ParentID from tbl_finishgood_categories WHERE ID=@category_id

CREATE TABLE #TemporaryTable
(
ID int,
ItemLibrarayGroupName nvarchar(150),
type int
)

INSERT into #TemporaryTable SELECT ID,Title,0 from tbl_finishedgoods_catalogue WHERE ID=@CatalogueID

SET @tempParentID=@category_id
	
	WHILE (@tempParentID <>0)
	BEGIN
		IF @ParentCatID=0
			BEGIN
				SET @tempParentID=0
			END
			
		INSERT into #TemporaryTable SELECT ID,ItemLibrarayGroupName,1 from tbl_finishgood_categories WHERE ID=@category_id 
		
		--set CategoryID for next Iteration
		SET @category_id=@ParentCatID
		SELECT @ParentCatID=ParentID from tbl_finishgood_categories WHERE ID=@category_id
			
			
			
		continue 
	END
INSERT into #TemporaryTable SELECT tbl_finishedgoods.ID,tbl_items.Title,2 from tbl_finishedgoods inner join tbl_items on tbl_items.itemid=tbl_finishedgoods.itemid WHERE tbl_finishedgoods.ItemID=@ProductID

select * from #TemporaryTable

RETURN