CREATE PROCEDURE dbo.sp_Item_Delete_ItemTitles
(
@ID int
)
AS
	delete from tbl_item_titles where ID=@ID
	
RETURN