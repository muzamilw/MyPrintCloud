CREATE PROCEDURE dbo.sp_ItemSection_Get_SectionsBriefInfoByItemID
(
	@ItemID int
)
AS
	SELECT tbl_item_sections.ItemSectionID as ItemSectionID,  tbl_item_sections.SectionName   as SectionName,  tbl_item_sections.IsMainSection as IsMainSection FROM tbl_item_sections where tbl_item_sections.ItemID = @ItemID order by ItemSectionID ASC
	RETURN