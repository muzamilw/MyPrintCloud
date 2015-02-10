CREATE PROCEDURE dbo.sp_Item_Add_ItemTitle
(@Title varchar(50))
AS
	insert into tbl_item_titles (ItemTitle) Values (@Title)
RETURN