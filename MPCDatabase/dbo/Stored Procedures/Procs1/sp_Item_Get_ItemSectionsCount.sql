CREATE PROCEDURE dbo.sp_Item_Get_ItemSectionsCount
(
	@ItemID int
)
AS
	select count(tbl_item_sections.ItemSectionID) as SectionCount from tbl_item_sections where ItemID=@ItemID
RETURN