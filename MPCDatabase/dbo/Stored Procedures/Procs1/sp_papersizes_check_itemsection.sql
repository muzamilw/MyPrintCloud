CREATE PROCEDURE dbo.sp_papersizes_check_itemsection
(
@PaperSizeID int)
AS
	SELECT tbl_item_sections.ItemSectionID FROM tbl_item_sections 
         WHERE ((tbl_item_sections.SectionSizeID = @PaperSizeID) or (tbl_item_sections.ItemSizeID = @PaperSizeID))
		RETURN