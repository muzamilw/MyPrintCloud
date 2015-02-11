
Create PROCEDURE [dbo].[sp_StockInventory_Get_Item_Section]

	(
		@ItemID integer
		--@parameter2 datatype OUTPUT
	)

AS
	SELECT tbl_item_sections.ItemSectionID FROM tbl_item_sections
        WHERE (((tbl_item_sections.Filmid = @ItemID) or 
        (tbl_item_sections.Plateid = @ItemID)) or 
        ((tbl_item_sections.StockItemID1 = @ItemID)))
	RETURN