CREATE PROCEDURE dbo.sp_ItemSection_GET_SectionNameBySectionID
(
	@SectionID int
)
AS
	SELECT     ItemSectionID, SectionNo, SectionName,PRessID,GuillotineID
FROM         tbl_item_sections  WHERE tbl_item_sections.ItemSectionID = @SectionID
	RETURN