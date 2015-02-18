CREATE PROCEDURE dbo.sp_ItemSection_Delete_SectionbyItemSectionID
(
	@ItemSectionID int
)
AS
	Delete from tbl_item_sections WHERE tbl_item_sections.ItemSectionID = @ItemSectionID
	RETURN